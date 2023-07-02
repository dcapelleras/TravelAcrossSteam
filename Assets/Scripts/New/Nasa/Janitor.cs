using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Janitor : MonoBehaviour
{
    [SerializeField] Transform doorAssistTransform;
    [SerializeField]NasaDoor receptionDoor;
    NavMeshAgent agent;
    [SerializeField] Transform awayPosition;
    //[SerializeField] SpriteRenderer rend;
    [SerializeField] Animator anim;
    float timer = 0;
    bool canUnlockDoor = false;
    Curious curious;

    private void Awake()
    {
        agent= GetComponent<NavMeshAgent>();
        curious = GetComponent<Curious>();
    }

    public void GoAssistDoor()
    {
        curious.curiousIndex = -1;
        agent.SetDestination(doorAssistTransform.position);

    }

    private void Update()
    {
        if (receptionDoor.isLocked)
        {
            if (Vector3.Distance(transform.position, doorAssistTransform.position) < 1f && canUnlockDoor)
            {
                anim.SetBool("walking", false);
                timer += Time.deltaTime;
                if (timer > 2f)
                {
                    receptionDoor.UnlockDoor();
                    GoAway();
                    NasaDialogueManager.instance.JanitorOpenedOffices();
                }
            }
        }
        if (agent.remainingDistance < 1f)
        {
            anim.SetBool("walking", false);
        }
        else
        {
            anim.SetBool("walking", true);
        }
    }

    public void UnlockDoor()
    {
        canUnlockDoor = true;
    }

    void GoAway()
    {
        agent.SetDestination(awayPosition.position);
        //rend.flipX = true;
        curious.curiousIndex = 1;
    }
}
