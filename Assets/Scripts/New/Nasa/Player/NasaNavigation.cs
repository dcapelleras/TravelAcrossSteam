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
    public SpriteRenderer rend;
    [SerializeField] Animator anim;
    NasaDialogueMovement dialogue;

    public bool canMove = true;

    NasaDoor doorBeingCrossed;

    public EventClickable pickingObject;

    public bool hasBossKeys;

    public bool hasFolderWithDocs;

    public bool invertedAxis;


    private void Awake()
    {
        dialogue = GetComponent<NasaDialogueMovement>();
        nav = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    private void Update()
    {
        if (transform.rotation != cam.transform.rotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, cam.transform.rotation, 2f);
        }

        if (Vector3.Distance(nav.destination, transform.position) < 3f)
        {
            anim.SetFloat("Walk", 0f);
        }
        else
        {
            anim.SetFloat("Walk", 1f);
        }

        if (invertedAxis)
        {
            if ((transform.position.z - nav.destination.z) < -0.5f) //walk to front
            {
                rend.flipX = false;
                //anim turn left
                //anim walk
            }
            else if ((transform.position.z - nav.destination.z) > 0.5f) //walk to back
            {
                rend.flipX = true;
                //anim turn right
                //anim walk
            }
        }
        else
        {
            if ((transform.position.x - nav.destination.x) < -0.5f) //walk to left
            {
                rend.flipX = true;
                //anim turn left
                //anim walk
            }
            else if ((transform.position.x - nav.destination.x) > 0.5f) //walk to right
            {
                rend.flipX = false;
                //anim turn right
                //anim walk
            }
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
                    }
                }
                else
                {
                    nav.SetDestination(hit.point);
                }

                if (invertedAxis)
                {
                    if ((transform.position.z - hit.point.z) < -0.5f) //walk to front
                    {
                        anim.SetFloat("Walk", 1f);
                        rend.flipX = false;
                        //anim turn left
                        //anim walk
                    }
                    else if ((transform.position.z - hit.point.z) > 0.5f) //walk to back
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
                else
                {
                    if ((transform.position.x - hit.point.x) < -0.5f) //walk to left
                    {
                        anim.SetFloat("Walk", 1f);
                        rend.flipX = true;
                        //anim turn left
                        //anim walk
                    }
                    else if ((transform.position.x - hit.point.x) > 0.5f) //walk to right
                    {
                        anim.SetFloat("Walk", 1f);
                        rend.flipX = false;
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
