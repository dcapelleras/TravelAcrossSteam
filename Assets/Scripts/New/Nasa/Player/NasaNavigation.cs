using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NasaNavigation : MonoBehaviour
{
    NavMeshAgent nav;
    Camera cam;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Animator anim;
    NasaDialogueMovement dialogue;

    public bool canMove = true;

    NasaDoor doorBeingCrossed;

    private void Awake()
    {
        dialogue = GetComponent<NasaDialogueMovement>();
        nav = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    private void Update()
    {

        if (Vector3.Distance(nav.destination, transform.position) < 3f)
        {
            anim.SetFloat("Walk", 0f);
        }
        else
        {
            anim.SetFloat("Walk", 1f);
        }

        if (!canMove)
        {
            return;
        }

        if (doorBeingCrossed != null)
        {
            if (Vector3.Distance(transform.position, doorBeingCrossed.inFront.position) < 3.1f)
            {
                if (doorBeingCrossed.eventIndex != 0)
                {
                    if (doorBeingCrossed.eventIndex == 1)
                    {
                        dialogue.CheckClosedReceptionDoor();
                        doorBeingCrossed.eventIndex= 0;
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            doorBeingCrossed = null;
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out PuzzleDetector puzzle))
                {
                    nav.SetDestination(puzzle.moveToPos.position);
                }
                else if (hit.collider.TryGetComponent(out NasaDoor door))
                {
                    doorBeingCrossed = door;
                    if (door.isLocked)
                    {
                        Debug.Log("the door is locked");
                        nav.SetDestination(door.inFront.position);
                    }
                    else if (!door.isLocked)
                    {
                        nav.SetDestination(door.crossedPos.position);
                        CamManager.instance.MoveToCam(door.doorIndex);
                    }
                }
                else
                {
                    nav.SetDestination(hit.point);
                }

                if ((transform.position.x - hit.point.x) < -0.5f)
                {
                    anim.SetFloat("Walk", 1f);
                    rend.flipX = false;
                    //anim turn left
                    //anim walk
                }
                else if ((transform.position.x - hit.point.x) > 0.5f)
                {
                    anim.SetFloat("Walk", 1f);
                    rend.flipX = true;
                    //anim turn right
                    //anim walk
                }
                else
                {
                    anim.SetFloat("Walk", 0f);
                    //anim idle
                }
            }
        }
    }

    public void MoveToThisDestination(Transform pos)
    {
        nav.SetDestination(pos.position);
        if (Vector3.Distance(nav.destination, transform.position) < 3f)
        {
            anim.SetFloat("Walk", 0f);
        }
        else
        {
            anim.SetFloat("Walk", 1f);
        }
    }
}
