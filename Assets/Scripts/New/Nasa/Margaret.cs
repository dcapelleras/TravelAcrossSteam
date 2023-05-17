using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Margaret : MonoBehaviour
{
    [SerializeField] Transform myDeskPos;
    NavMeshAgent agent;
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        agent.SetDestination(myDeskPos.position);
    }

    private void Update()
    {
        if (transform.rotation != cam.transform.rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, cam.transform.rotation, 2f);
        }
    }
}
