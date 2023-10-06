using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentPatrol : MonoBehaviour
{
    public Transform[] PatrolPoints;
    private NavMeshAgent agent;
    private int currentIndex = 0; 
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(PatrolPoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentIndex = (currentIndex + 1) % PatrolPoints.Length;
        }
    }
}
