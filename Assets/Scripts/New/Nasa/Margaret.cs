using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Margaret : MonoBehaviour
{
    [SerializeField] Transform myDeskPos;
    [SerializeField] Transform secondPos;
    [SerializeField] Transform thirdPos;
    NavMeshAgent agent;
    [SerializeField] Animator anim;
    bool isIdle = false;
    bool isSitting;
    bool canCheckAgent;
    bool passedSecond;
    [SerializeField] GameObject chair;
    Curious curious;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        curious = GetComponent<Curious>();
    }

    private async void OnEnable()
    {
        SetDestinationToTable();
        await Task.Delay(1000);
        canCheckAgent= true;
    }

    void SetDestinationToTable()
    {
        agent.SetDestination(myDeskPos.position);
    }

    private void Update()
    {
        if (canCheckAgent)
        {
            if (!isSitting)
            {
                if (!isIdle)
                {
                    if (agent.remainingDistance < 0.1f)
                    {
                        isIdle = true;
                        anim.SetBool("idle", true);
                    }
                    if (agent.remainingDistance < 0.5f && !passedSecond)
                    {
                        passedSecond = true;
                        agent.SetDestination(secondPos.position);
                    }
                    if (agent.remainingDistance < 0.3f && passedSecond)
                    {
                        agent.SetDestination(thirdPos.position);
                    }
                }
            }
        }
    }

    public void SitDown()
    {
        canCheckAgent= false;
        isSitting = true;
        //anim.SetBool("sit", true);
        //chair.SetActive(true);
        curious.curiousIndex = 3;
    }
}
