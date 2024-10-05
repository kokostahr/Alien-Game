using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using TMPro;
using UnityEngine.UI;

public class FirstPersonControls : MonoBehaviour
{
    [Header("MOVEMENT SETTINGS")]
    [Space(5)]
    // Public variables to set movement and look speed, and the player camera
    public float moveSpeed; // Speed at which the player moves
    public float sprintSpeed; //Speed at which the player sprints
    public float lookSpeed; // Sensitivity of the camera movement
    public float gravity = -9.81f; // Gravity value
    public float jumpHeight = 1.0f; // Height of the jump
    public float jumpLimit = 2f; //Eish, trying to limit the jumps  (made it accessible in inspector for trial and error)
    public float timesJumped; //Checking how many times the player has jumped
    public Transform playerCamera; // Reference to the player's camera
                                   // Private variables to store input values and the character controller
    private Vector2 moveInput; // Stores the movement input from the player
    private Vector2 lookInput; // Stores the look input from the player
    private float verticalLookRotation = 0f; // Keeps track of vertical camera rotation for clamping
    private Vector3 velocity; // Velocity of the player
    private CharacterController characterController; // Reference to the CharacterController component
    public bool isSprinting = false; //Whether the player is currently sprinting

    [Header("SHOOTING SETTINGS")]
    [Space(5)]
    public GameObject projectilePrefab; // Projectile prefab for shooting
    public Transform firePoint; // Point from which the projectile is fired (Big T, another game object is transformed)
    public float projectileSpeed = 20f; // Speed at which the projectile is fired
    public float pickUpRange = 6f; // Range within which objects can be picked up
    private bool holdingGun = false;
    public int MaxBullets = 10;
    private int currentBullets;
    //Need the UI FOR THE AMMO AND STUFF
    public GameObject ammoUi;  //Game object of the whole GunAmmo UI
    public TextMeshProUGUI ammoCount; //Updating text that indicates how much ammo is left
    public GameObject ammoInstruct; //Text that is displayed when the ammo runs out instructing players to reload their gun
    //DEFINING THE VARIABLES FOR THE BULLET DAMAGE
    public int playerBulletDamage; //The amount of damage the player's bullet will do to the enemies
    private GameObject enemy; //Calling the enemy so that its not confusing.
    public EnemyController enemyController; //Calling the enemy controller so we can access the enemy's health 

    [Header("STABBING SETTINGS")]
    [Space(5)]
    public float stabSpeed = 15f;
    //private bool holdingKnife = false;

    [Header("PICKING UP SETTINGS")]
    [Space(5)]
    public Transform holdPositionLeft; // Position where the picked-up object will be held
    public Transform holdPositionRight; // Position where the picked-up object will be held
    private GameObject heldObject; // Reference to the currently held object

    // Crouch settings
    [Header("CROUCH SETTINGS")]
    [Space(5)]
    public float crouchHeight = 1f; // Height of the player when crouching
    public float standingHeight = 2f; // Height of the player when standing
    public float crouchSpeed = 2.5f; // Speed at which the player moves when crouching
    private bool isCrouching = false; // Whether the player is currently crouching

    [Header("INTERACT SETTINGS")]
    [Space(5)]
    public Material switchMaterial; // Material to apply when switch is activated
    public GameObject[] objectsToChangeColor; // Array of objects to change color
    //UI FOR THE GATE INTERACTION 
    public GameObject gateInteractionText; //Object of the text that we want to turn on and off

    [Header("UI SETTINGS")]
    [Space(5)]
    public GameObject pickUpText; //Text of the pick up interaction
    public TextMeshProUGUI healthCount; //Text that will display the player's current health
    public Slider playerHealthBar; //Self explanatory...player's healthbar
    public int playerTotalHealth = 100;  //Total health the player begins with
    public int currentHealth;  //Their updating health when injured by enemies

    //public float damageAmount = 0.25f; // Reduce the health bar by this amount
    //private float healAmount = 0.5f;// Fill the health bar by this amount

    


    private void Awake()
    {
        // Get and store the CharacterController component attached to this GameObject
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        currentBullets = MaxBullets;
        //ammoCount.text = "Ammo = " + currentBullets.ToString();

        currentHealth = playerTotalHealth;

        playerHealthBar.maxValue = playerTotalHealth; //maximum value that the healthbar will display
        playerHealthBar.value = currentHealth; //The updating currentvalue of the player's health when they are injured and etc...

        //healthCount.text = "Health = " + currentHealth.ToString();

        //As stated above, calling the enemy within start for the bullets to have an effect on the enemy's health.
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    } 

    private void OnEnable() //initialises and enables input actions. It listens for player input to handle, referring to the generated C# script for the action map
    {
        // Create a new instance of the input actions
        var playerInput = new Controls();

        // Enable the input actions
        playerInput.Player.Enable();

        // Subscribe to the movement input events
        //A lamder expression is a short way of representing longer code. Checks if the input has been done, and performed or cancelled. 
        playerInput.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed. CTX in this line refers to the contex, refers to the key bindings. 
        playerInput.Player.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

        // Subscribe to the look input events
        playerInput.Player.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>(); // Update lookInput when look input is performed
        playerInput.Player.LookAround.canceled += ctx => lookInput = Vector2.zero; // Reset lookInput when look input is canceled

        // Subscribe to the jump input event
        playerInput.Player.Jump.performed += ctx => Jump(); // Call the Jump method when jump input is performed

        // Subscribe to the shoot input event
        playerInput.Player.Shoot.performed += ctx => Shoot(); // Call the Shoot method when shoot input is performed

        // Subscribe to the pick-up input event
        playerInput.Player.PickUp.performed += ctx => PickUpObject(); // Call the PickUpObject method when pick-up input is performed

        // Subscribe to the crouch input event
        playerInput.Player.Crouch.performed += ctx => ToggleCrouch(); // Call the ToggleCrouch method when crouch input is performed

        // Subscribe to the sprint input event
        playerInput.Player.Sprint.performed += ctx => Sprint(); // Call the ToggleSprint method when sprint input is performed

        // Subscribe to the interact input event
        playerInput.Player.Interact.performed += ctx => Interact(); //Interact with switch


    }
    private void Update()
    {
        // Call Move and LookAround methods every frame to handle player movement and camera rotation
        Move();
        LookAround();
        ApplyGravity();
        CheckForInteractionTrigger();

        //Update the text value for the ammo and the health every frame
        ammoCount.text = "Ammo = " + currentBullets.ToString();
        healthCount.text = "Health = " + currentHealth.ToString();

        playerHealthBar.value = currentHealth;

        if (currentHealth < 1)
        {
            //need to play "you died scene"
            Debug.Log("You died");

        }
    }
    public void Move()
    {
        // Create a movement vector based on the input
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        // Transform direction from local to world space.
        // Local space refers to its self space whereas world space refers to the gloabal space within the game world. eg a moon orbits around the earth - Local Space. Moon orbits around the sun - world space.
        move = transform.TransformDirection(move);


        //Adjust speed if crouching
        float currentSpeed;

        if(isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        //Adjust speed when sprinting
        if (isSprinting)
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }


        // Move the character controller based on the movement vector and speed
        characterController.Move(move * currentSpeed * Time.deltaTime);
    }
    public void LookAround()
    {
        // Get horizontal and vertical look inputs and adjust based on sensitivity
        float LookX = lookInput.x * lookSpeed;
        float LookY = lookInput.y * lookSpeed;

        // Horizontal rotation: Rotate the player object around the y-axis
        //difference between transform and Transform- Transform (with caps) allows us to refer to another gameObjects Transform.
        //transform (baby t) refers to the transform of the object the current script is attached to.
        transform.Rotate(0, LookX, 0);

        // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
        //Restricting the Up and Down looking to 90 degrees. 
        //Mathf.clamp takes in 3 arguments: the thing you want to clamp, the min value, the max value. 
        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        // Apply the clamped vertical rotation to the player camera, because the player is looking through the camera view, not the player object. 
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
    }
    public void ApplyGravity()
    {
        //Checking if the player is on the ground, so that they remain on the ground. 
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f; // Small value to keep the player grounded
        }

        //If the player is in the air, gravity should gradually pull them back to the ground
        velocity.y += gravity * Time.deltaTime; // Apply gravity to the velocity
        characterController.Move(velocity * Time.deltaTime); // Apply the velocity to the character
    }
    public void Jump()
    {
        if (characterController.isGrounded) //JUMP when character is already on the ground
        {
            // Calculate the jump velocity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            timesJumped = 0;
        }
        else if (!characterController.isGrounded && timesJumped < jumpLimit) 
        {
            // Calculate the double jump velocity. Jump when player is already in the air
            timesJumped++;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

    }

    public void Shoot()
    {
        if (holdingGun == true && currentBullets > 0)                                 
        {
            Debug.Log("Shoot called");

            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

            // Get the Rigidbody component of the projectile and set its velocity
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = firePoint.forward * projectileSpeed;          //BulletSpawnPoint is the firepoint
                                                                        // Destroy the projectile after 3 seconds
            Destroy(projectile, 3f);

            currentBullets--;  

            if (currentBullets == 0) //when the bullets run out--
            {
                //--display the instruction to reload gun
                ammoInstruct.SetActive(true);
            }
        }

    }

    public void ReloadGun() //function that will reload the gun when the reload button is pressed.
    {
        if (currentBullets == 0)
        {
            //hide the instruction text after some seconds
            ammoInstruct.SetActive(false);

            //need code that says when the player presses the button, then refill the bullets
            currentBullets = MaxBullets;
        }
    }

    private void OnTriggerEnter(Collider other) //Function that will decrease the enemy's health when the bullet interacts with their colliders
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("KILL 'EM");
            if (enemy !=null)
            {
                enemyController.emycurrentHealth -= playerBulletDamage;
            }
        }
    }

    public void PickUpObject()
    {
        // Check if we are already holding an object
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            heldObject.transform.parent = null;
            holdingGun = false;
            //Hide the UI STUFF for the gun
            ammoUi.SetActive(false);

        }

        // Perform a raycast from the camera's position forward
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Debugging: Draw the ray in the Scene view
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.red, 2f);


        if (Physics.Raycast(ray, out hit, pickUpRange))
        {

            //Once the raycast hits, pickUp text should display
            //if (hit.collider.CompareTag("PickUp") || hit.collider.CompareTag("Knife") || hit.collider.CompareTag("Gun"))
            //{
            //    pickUpText.SetActive(true);
            //}

            // Check if the hit object has the tag "PickUp"
            if (hit.collider.CompareTag("PickUp"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the LEFT hold position
                heldObject.transform.position = holdPositionLeft.position;
                heldObject.transform.rotation = holdPositionLeft.rotation;
                heldObject.transform.parent = holdPositionLeft;

                //Make sure the pickuptext disappears after the object has been picked up
                //pickUpText.SetActive(false);
            }

            if (hit.collider.CompareTag("Knife"))
            {
                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the RIGHT hold position
                heldObject.transform.position = holdPositionRight.position;
                heldObject.transform.rotation = holdPositionRight.rotation;
                heldObject.transform.parent = holdPositionRight;

                //Make sure the pickuptext disappears after the object has been picked up
                //pickUpText.SetActive(false);
            }
           
            if (hit.collider.CompareTag("Gun"))
            {
                //Display the UI STUFF for the gun
                ammoUi.SetActive(true);

                // Pick up the object
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics

                // Attach the object to the RIGHT hold position
                heldObject.transform.position = holdPositionRight.position;
                heldObject.transform.rotation = holdPositionRight.rotation;
                heldObject.transform.parent = holdPositionRight;

                //So that the mf shooting can work
                holdingGun = true;

                //Make sure the pickuptext disappears after the object has been picked up
                //pickUpText.SetActive(false);
            }    
        }
    }

    public void ToggleCrouch()
    {
        if (isCrouching)
        {
            // Stand up
            characterController.height = standingHeight;
            isCrouching = false;
        }
        else
        {
            // Crouch down
            characterController.height = crouchHeight;
            isCrouching = true;
        }
    }

    public void Sprint()
    {
        //Need a movement vector, to find the input
        Vector3 run = new Vector3(moveInput.x, 0, moveInput.y);
        run = transform.TransformDirection(run);


        if (isSprinting)
        {
            //Speed Up
            isSprinting = false;
        }
        else
        {
            //Slow Down
            isSprinting = true;
        }

        // Move the character controller based on the movement vector and speed
        characterController.Move(run * sprintSpeed * Time.deltaTime);

    }

    public void Interact()
    {
        // Perform a raycast to detect the lightswitch
        // Can use to change gameObjects in the scene. Here we're switching materials, but we can use it to switch objects. 
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            //Display the interaction text to guide players on what to press
            //gateInteractionText.SetActive(true);

            if (hit.collider.CompareTag("Switch")) // Assuming the switch has this tag
            {
                // Change the material color of the objects in the array
                foreach (GameObject obj in objectsToChangeColor)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = switchMaterial.color; // Set the color to match the switch material color
                    }
                }
            }
            else if (hit.collider.CompareTag("Door")) // Check if the object is a gate
            {
                // Start moving the door upwards
                StartCoroutine(RaiseDoor(hit.collider.gameObject));

                //Hide interaction text after the stuff has been pressed
                //gateInteractionText.SetActive(false)
            }
            else if (hit.collider.CompareTag("Gate")) // Check if the object is a gate
            {
                // Start moving the door upwards
                StartCoroutine(OpenGate(hit.collider.gameObject));

                //Hide interaction text after the stuff has been pressed
                //gateInteractionText.SetActive(false)
            }

            ;
        }
    }

    private IEnumerator RaiseDoor(GameObject door)
    {
        float openAmount = 5f; // The total distance the door will be opened
        float openSpeed = 2f; // The speed at which the door will be opened
        Vector3 startPosition = door.transform.position; // Store the initial position of the door
        Vector3 endPosition = startPosition + Vector3.right * openAmount; //Calculate the final position of the door after opening
                                                                        //use left/right and position.x to move it left and right rather than up and down.

        // Continue raising the door until it reaches the target height
        while (door.transform.position.x < endPosition.x)
        {
            // Move the door towards the target position at the specified speed
            door.transform.position =
            Vector3.MoveTowards(door.transform.position, endPosition, openSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame before continuing the loop

            //Need code that says after a certain amount of time the door should return to startPosition. 
            //WaitForSeconds(5f)
            //door.transform.position =
            //Vector3.MoveTowards(endPosition, door.transform.position, openSpeed * Time.deltaTime);
            //yield return null; // Wait until the next frame before continuing the loop
        }
    }

    private IEnumerator OpenGate(GameObject door)
    {
        float openAmount = 20f; // The total distance the door will be opened
        float openSpeed = 3f; // The speed at which the door will be opened
        Vector3 startPosition = door.transform.position; // Store the initial position of the door
        Vector3 endPosition = startPosition + Vector3.right * openAmount; //Calculate the final position of the door after opening
                                                                          //use left/right and position.x to move it left and right rather than up and down.

        // Continue raising the door until it reaches the target height
        while (door.transform.position.x < endPosition.x)
        {
            // Move the door towards the target position at the specified speed
            door.transform.position =
            Vector3.MoveTowards(door.transform.position, endPosition, openSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame before continuing the loop

            //Need code that says after a certain amount of time the door should return to startPosition. 
            //WaitForSeconds(5f)
            //door.transform.position =
            //Vector3.MoveTowards(endPosition, door.transform.position, openSpeed * Time.deltaTime);
            //yield return null; // Wait until the next frame before continuing the loop
        }
    }

    //Going to modify this accordingly to include triggers for V/O, Object triggers
    private void CheckForInteractionTrigger()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        // Perform raycast to detect objects
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the object has the "PickUp" tag
            if (hit.collider.CompareTag("PickUp") || hit.collider.CompareTag("Knife") || hit.collider.CompareTag("Gun"))
            {
                // Display the pick-up text
                pickUpText.gameObject.SetActive(true); // DISPLAY THE TEXT
                //pickUpText.text = hit.collider.gameObject.name; //this DISPLAYs THE actual NAME OF THE GAMEOBJECT
                Debug.Log("ACTIVATED PICKUPTEXT");
            }
            else
            {
                // Hide the pick-up text if not looking at a "PickUp" object
                pickUpText.gameObject.SetActive(false);
            }
        }
        else
        {
            // Hide the text if not looking at any object
            pickUpText.gameObject.SetActive(false);
        }

        // Perform raycast to detect the gate
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            // Check if the object has the "Gate" or "Door" tag
            if (hit.collider.CompareTag("Gate") || hit.collider.CompareTag("Door"))
            {
                // Display the gate interaction text
                gateInteractionText.SetActive(true);
                //pickUpText.text = hit.collider.gameObject.name; 
                Debug.Log("ACTIVATED OPENGATE");
            }
            else
            {
                // Hide the gateopening text if not looking at a "Gate" or "Door" object
                gateInteractionText.SetActive(false);
            }
        }
        else
        {
            // Hide the text if not looking at any object
            gateInteractionText.SetActive(false);
        }
    }


   
}