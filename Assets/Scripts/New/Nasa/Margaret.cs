using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Margaret : MonoBehaviour
{
    [SerializeField] Transform myDeskPos;
    NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        agent.SetDestination(myDeskPos.position);
    }
}
