using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MovementMoba : MonoBehaviour
{
    Camera cam;
    GameObject catchedObj;
    float maxRayDistance = 500f;
    [SerializeField] GameObject spherePointer;
    bool moving;
    Vector3 clickedPoint;
    Vector3 goalPosition;
    float speed = 10f;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {

        MoveWithMouseClick();
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

    private void RaycastDetectObject() //at update
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, maxRayDistance))
            {
                if (hit.collider.CompareTag("Interactable"))
                {
                    catchedObj = hit.collider.gameObject;
                    Instantiate(spherePointer, hit.point, Quaternion.identity);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (catchedObj != null)
            {
                Debug.Log(catchedObj.name);
            }
        }
    }
}
