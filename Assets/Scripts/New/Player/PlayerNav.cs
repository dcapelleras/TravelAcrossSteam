using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

public class PlayerNav : MonoBehaviour //player
{
    #region References
    public NavMeshAgent agent;
    Camera cam;
    DialogueRunner dialogueRunner;
    [SerializeField] Animator anim;
    #endregion
    bool canMove;
    bool gameEnded = false;
    [SerializeField] Transform endgameTransform;

    private void Awake()
    {
        cam = Camera.main;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<int>("Movement", AllowMovement);
        dialogueRunner.AddCommandHandler<int>("FinishGame", EndGame);
    }

    private void Update()
    {
        if (!canMove || gameEnded)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                if ((transform.position.x - hit.point.x) < -0.1f)
                {
                    anim.SetTrigger("Right");
                    //anim turn left
                    //anim walk
                }
                else if ((transform.position.x - hit.point.x) > 0.1f)
                {
                    anim.SetTrigger("Left");
                    //anim turn right
                    //anim walk
                }
                else
                {
                    //anim idle
                }
            }
        }
    }

    public void AllowMovement(int allowed)
    {
        if (allowed== 0)
        {
            canMove= false;
            return;
        }
        canMove = true;
    }

    public void EndGame(int i)
    {
        AllowMovement(0);
        gameEnded = true;
        agent.enabled = false;
        transform.position = endgameTransform.position;
        CamManager.instance.MoveToCam(3);
        RoomManager.instance.ChangeLoadingScreen(true);
    }

    public void TriggerEndGame(int i)
    {
        dialogueRunner.Dialogue.Stop();
        dialogueRunner.StartDialogue("TerminateGame");
    }
}
