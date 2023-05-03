using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCable : MonoBehaviour
{
    [SerializeField] Transform mousePosTransform;
    Vector3 newPos;
    public Cable holdingCable;
    [SerializeField] Transform startingPosition;

    private void OnEnable()
    {
        newPos = startingPosition.position;
    }

    private void Update()
    {

        //activate the player when needed


        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            newPos.x = hit.point.x;
            newPos.y = hit.point.y;
            if (hit.collider.TryGetComponent(out Cable cable))
            {
                newPos = hit.point;
                newPos.z = cable.transform.position.z;
                newPos.y += 0.5f;
            }
            mousePosTransform.position = newPos;
            Debug.DrawRay(Camera.main.transform.position, hit.point * hit.distance, Color.yellow, 2f);
        }
    }


    /*
    public GameObject holdingCable;
    [SerializeField] Vector3 offset = new Vector3(0, 1, -1);
    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            transform.position = hit.point;
            if (Input.GetMouseButton(0))
            {
                if (hit.collider.CompareTag("Cable"))
                {
                    holdingCable = hit.collider.gameObject;
                    holdingCable.transform.SetParent(transform);
                }
                if (holdingCable != null && holdingCable.GetComponent<Cable>().inPlace)
                {
                    holdingCable.transform.SetParent(null);
                    holdingCable = null;
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                holdingCable.transform.SetParent(null); 
                holdingCable = null;
            }
        }
        
    }*/
}