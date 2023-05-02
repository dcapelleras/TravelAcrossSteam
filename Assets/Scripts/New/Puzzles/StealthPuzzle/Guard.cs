using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    NavMeshAgent nav;

    private void Awake()
    {
        nav= GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) < 1f)
        {
            Debug.Log("Player catched");
        }
    }

    public void DetectPlayer()
    {
        Debug.Log("Player detected");
        nav.SetDestination(playerTransform.position);
    }
}
