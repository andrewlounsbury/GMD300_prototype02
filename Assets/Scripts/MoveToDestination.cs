using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveToDestination : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform Destination; 


    // Update is called once per frame
    void Update()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = Destination.position; 
    }
}
