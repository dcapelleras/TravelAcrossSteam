using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NasaNavigation : MonoBehaviour
{
    NavMeshAgent nav;
    Camera cam;
    //public SpriteRenderer rend;
    [SerializeField] Animator anim;
    NasaDialogueMovement dialogue;

    public bool canMove = true;

    NasaDoor doorBeingCrossed;

    public EventClickable pickingObject;

    public bool hasBossKeys;

    public bool hasFolderWithDocs;

    public bool invertedAxis;

    public bool isExterior;

    public int placeToGoBackWhenCaught;

    [SerializeField] AudioSource secondaryAudio;

    bool walkingSoundOn;

    private void Awake()
    {
        dialogue = GetComponent<NasaDialogueMovement>();
        nav = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    private void Update()
    {
        float dist = 3f;
        if (isExterior)
        {
            dist = 1.35f;
        }
        if (Vector3.Distance(nav.destination, transform.position) < dist && walkingSoundOn)
        {
            walkingSoundOn = false;
            anim.SetFloat("Walk", 0f);
            secondaryAudio.Stop();
        }
        if (GameManager.instance.menuOpen)
        {
            walkingSoundOn = false;
            secondaryAudio.Stop();
        }
        else if (!walkingSoundOn && Vector3.Distance(nav.destination, transform.position) > dist)
        {
            walkingSoundOn= true;
            anim.SetFloat("Walk", 1f);
            secondaryAudio.Play();
        }
            if (!canMove)
        {
            return;
        }

        if (doorBeingCrossed != null)
        {
            if (Vector3.Distance(transform.position, doorBeingCrossed.inFront.position) < 4.1f)
            {
                if (doorBeingCrossed.eventIndex != 0)
                {
                    if (doorBeingCrossed.eventIndex == 1)
                    {
                        dialogue.CheckClosedReceptionDoor();
                        doorBeingCrossed.eventIndex= 0;
                    }
                    else if (doorBeingCrossed.eventIndex == 3)
                    {
                        NasaDialogueManager.instance.OpenWarehouse();
                        doorBeingCrossed.eventIndex = 0;
                    }
                }
            }
        }
        if (pickingObject != null)
        {
            if (Vector3.Distance(transform.position, pickingObject.transform.position) < pickingObject.distanceToPick)
            {
                pickingObject.ExecuteAction();
                pickingObject = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            doorBeingCrossed = null;
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    return;
                }

                if (hit.collider.TryGetComponent(out EventClickable clickable))
                {
                    pickingObject = clickable;
                    MoveToThisDestination(clickable.placeToPick);
                }
                else
                {
                    pickingObject = null;
                }
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
                        if (door.changesAxisView)
                        {
                            invertedAxis = !invertedAxis;
                        }
                        CamManager.instance.MoveToCam(doorBeingCrossed.doorIndex);
                        nav.SetDestination(door.crossedPos.position);
                        if (door.indexForGuards != 10)
                        {
                            placeToGoBackWhenCaught = door.indexForGuards;
                        }
                    }
                }
                else
                {
                    nav.SetDestination(hit.point);
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
