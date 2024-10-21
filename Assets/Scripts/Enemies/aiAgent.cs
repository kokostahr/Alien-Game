using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class aiAgent : MonoBehaviour
{

   private NavMeshAgent agent;
    [SerializeField] private Transform movePos;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
       agent.SetDestination(movePos.position);    
    }
}
