using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cinemachine;
    public Cinemachine.CinemachineVirtualCamera cinemachine2;

    Camera cam;

    Vector3 moveToVector;
    Vector3 goalPosition;

    Rigidbody rb;

    float moveSpeed = 20f;

    public GameObject interactingObject;
    public GameObject secondInteracting;
    bool firstOrSecond;

    GameObject pickingObject;

    private void Awake()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        GetInput();
        Move();
        
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
            moveToVector = Vector3.zero;
            RaycastHit hit;
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider.CompareTag("Pickable"))
                {

                    goalPosition = hit.collider.transform.position;
                    moveToVector = goalPosition;
                    goalPosition.y = 1f;
                    pickingObject = hit.collider.gameObject;
                    if (pickingObject.TryGetComponent(out Pickable pickable))
                    {
                        pickable.pickedUp = true;
                    }
                }
                else if (hit.collider.CompareTag("Interactable"))
                {
                    /*if (pickingObject.TryGetComponent(out Interactable interactable))
                    {
                        goalPosition = interactable.gotoPosition.position;
                        moveToVector = goalPosition;
                    }
                    moveToVector.y = 1f;*/
                }
                else
                {
                    goalPosition = hit.point;
                    moveToVector = hit.point;
                    moveToVector.y = 1f;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (firstOrSecond)
            {
                cinemachine2.Priority = 13;
                firstOrSecond = false;
            }
            else
            {
                firstOrSecond   = true;
                cinemachine2.Priority = 11;
            }
        }
    }


    void Move()
    {
        rb.MovePosition(goalPosition);
    }

    void Interact()
    {
        if (interactingObject != null)
        {
            if (firstOrSecond)
            {
                firstOrSecond = false;
                cinemachine.Follow = interactingObject.transform;
                cinemachine.LookAt = interactingObject.transform;
                cinemachine.Priority = 12;
            }
            else
            {
                firstOrSecond= true;
                cinemachine.Follow = secondInteracting.transform;
                cinemachine.LookAt = secondInteracting.transform;
                cinemachine.Priority = 12;
            }
        }
    }
}
