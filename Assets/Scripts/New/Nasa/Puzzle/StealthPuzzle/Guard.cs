using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Yarn.Unity;

public class Guard : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Animator anim;
    NavMeshAgent nav;
    [SerializeField] float maxXPos;
    [SerializeField] float minXPos;
    [SerializeField] float maxZPos;
    [SerializeField] float minZPos;
    [SerializeField] Transform placeToLookAt;
    Vector3 goalPos;
    float timerToGoBack;
    float timeToGoBack = 4f;
    bool patrolling;
    [SerializeField] float timeBetweenPatrolChange = 5f;

    [SerializeField] List<Transform> wayPoints= new List<Transform>();

    float timeFriendly = 5f;
    float timerWhileFriendly;
    bool friendly;


    private void Awake()
    {
        nav= GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        goalPos = wayPoints[0].position;
    }

    private void OnEnable()
    {
        patrolling = true;
        StartCoroutine(PatrolBetweenPoints());
    }

    private void OnDisable()
    {
        patrolling = false;
        StopCoroutine(PatrolBetweenPoints());
    }

    private void Update()
    {
        if (nav.remainingDistance < 1)
        {
            anim.SetBool("walking", false);
        }
        else
        {
            anim.SetBool("walking", true);
        }
        if (friendly)
        {
            timerWhileFriendly += Time.deltaTime;
            if (timerWhileFriendly >= timeFriendly)
            {
                friendly = false;
            }
        }
        if (Vector3.Distance(playerTransform.position, transform.position) < 3.2f)
        {
            if (!friendly)
            {
                CatchPlayer();
            }

        }
        if (timerToGoBack < timeToGoBack)
        {
            if (Vector3.Distance(transform.position, goalPos) > 12f)
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
            nav.SetDestination(goalPos);
        }
    }

    IEnumerator PatrolBetweenPoints()
    {
        while (patrolling)
        {
            for (int i = 0; i < wayPoints.Count; i++)
            {
                goalPos = wayPoints[i].position;
                yield return new WaitForSeconds(timeBetweenPatrolChange);
            }
        }
    }

    public void DetectPlayer()
    {
        if (!friendly)
        {
            timerToGoBack = 0;
            nav.SetDestination(playerTransform.position);
        }
        
    }

    void CatchPlayer()
    {
        nav.isStopped = true;
        nav.ResetPath();
        friendly = true;
        timerWhileFriendly= 0;
        NasaDialogueManager.instance.CatchedDialogue();
    }
}
