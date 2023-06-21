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
    bool patrolling;
    [SerializeField] float timeBetweenPatrolChange = 5f;

    [SerializeField] List<Transform> wayPoints= new List<Transform>();

    public bool friendly;


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
        
        if (Vector3.Distance(playerTransform.position, transform.position) < 3.2f)
        {
            if (!friendly)
            {
                CatchPlayer();
            }
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
                nav.SetDestination(goalPos);
            }
        }
    }

    public void DetectPlayer()
    {
        if (!friendly)
        {
            CatchPlayer();
        }
        
    }

    void CatchPlayer()
    {
        nav.isStopped = true;
        nav.ResetPath();
        friendly = true;
        NasaDialogueManager.instance.CatchedDialogue();
    }
}
