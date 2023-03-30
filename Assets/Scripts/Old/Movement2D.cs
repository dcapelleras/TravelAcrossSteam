using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    bool moving;
    Vector3 goalPosition;
    [SerializeField] float speed = 10f;
    Camera cam;
    Vector3 clickedPoint;
    bool interacting;
    GameObject interactingObject;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        MoveWithMouseClick();

        if (interacting && transform.position == goalPosition)
        {
            interacting = false;
            if (interactingObject.TryGetComponent(out InteractablePuzzle interactable))
            {
                Debug.Log("interacting");
                //interactable.Interact();
            }
        }
    }

    void MoveWithMouseClick()//at update, better with baked navmesh, but for now...
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, goalPosition, speed * Time.deltaTime);
            if (transform.position == goalPosition)
            {
                moving = false;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Walkable")
                {
                    clickedPoint = hit.point;
                    goalPosition = new Vector3(clickedPoint.x, clickedPoint.y + 1f, clickedPoint.z);
                    //float distance = clickedPoint.magnitude - transform.position.magnitude;
                    moving = true;
                }
                else if (hit.collider.tag == "Interactable")
                {
                    Debug.Log("clicked an interactable");
                    clickedPoint = hit.point;
                    goalPosition = hit.collider.GetComponent<InteractablePuzzle>().moveToPosition.position;
 
                    moving = true;
                    interacting = true;
                    interactingObject = hit.collider.gameObject;
                }
            }
        }
    }

    void Interact()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag != "Walkable")
                {
                    return;
                }
                clickedPoint = hit.point;
                goalPosition = new Vector3(clickedPoint.x, clickedPoint.y + 1f, clickedPoint.z);
                //float distance = clickedPoint.magnitude - transform.position.magnitude;
                moving = true;
            }
        }
    }
}
