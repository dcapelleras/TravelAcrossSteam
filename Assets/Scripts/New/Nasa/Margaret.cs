using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Margaret : MonoBehaviour
{
    [SerializeField] Transform myDeskPos;
    [SerializeField] Transform secondPos;
    NavMeshAgent agent;
    [SerializeField] Animator anim;
    bool isIdle = false;
    bool isSitting;
    bool canCheckAgent;
    [SerializeField] GameObject chair;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
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
                    if (agent.remainingDistance < 0.3f)
                    {
                        agent.SetDestination(secondPos.position);
                    }
                }
            }
        }
    }

    public void SitDown()
    {
        canCheckAgent= false;
        isSitting = true;
        anim.SetBool("sit", true);
        chair.SetActive(true);
    }
}
