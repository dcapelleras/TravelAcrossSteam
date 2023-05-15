using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Guard : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    NavMeshAgent nav;
    [SerializeField] float maxXPos;
    [SerializeField] float minXPos;
    [SerializeField] float maxZPos;
    [SerializeField] float minZPos;
    [SerializeField] Transform placeToLookAt;
    Vector3 initialPos;
    float timerToGoBack;
    float timeToGoBack = 4f;

    private void Awake()
    {
        nav= GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        initialPos = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) < 1f)
        {
            Debug.Log("Player catched");
        }
        if (timerToGoBack < timeToGoBack)
        {
            if (Vector3.Distance(transform.position, initialPos) > 12f)
            {
                timerToGoBack += Time.deltaTime;
            }
            else
            {
                timerToGoBack = 0f;
            }
        }

        if (timerToGoBack >= timeToGoBack && nav.remainingDistance < 0.2f)
        {
            Quaternion newRotation = Quaternion.LookRotation(placeToLookAt.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 1);
        }
        if (transform.position.x > maxXPos || transform.position.x < minXPos || transform.position.z > maxZPos || transform.position.z < minZPos || (timerToGoBack >= timeToGoBack)) 
        {
            Debug.Log("going back to initial pos");
            nav.SetDestination(initialPos);
        }
        Debug.Log("distance between transform pos and initial pos"+Vector3.Distance(transform.position, initialPos));
    }

    public void DetectPlayer()
    {
        timerToGoBack = 0;
        nav.SetDestination(playerTransform.position);
    }
}
