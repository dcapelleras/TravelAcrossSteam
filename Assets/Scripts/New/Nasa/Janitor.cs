using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Janitor : MonoBehaviour
{
    [SerializeField] Transform doorAssistTransform;
    [SerializeField]NasaDoor receptionDoor;
    NavMeshAgent agent;
    [SerializeField] Transform awayPosition;
    float timer = 0;

    private void Awake()
    {
        agent= GetComponent<NavMeshAgent>();
    }

    public void GoAssistDoor()
    {
        agent.SetDestination(doorAssistTransform.position);
    }

    private void Update()
    {
        if (receptionDoor.isLocked)
        {
            if (Vector3.Distance(transform.position, doorAssistTransform.position) < 1f)
            {
                timer += Time.deltaTime;
                if (timer > 2f)
                {
                    receptionDoor.isLocked = false;
                    GoAway();
                    NasaDialogueManager.instance.JanitorOpenedOffices();
                }
            }
        }
    }

    void GoAway()
    {
        agent.SetDestination(awayPosition.position);
    }
}
