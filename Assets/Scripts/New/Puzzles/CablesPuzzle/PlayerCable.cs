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
                newPos.y += 0.25f;
            }
            mousePosTransform.position = newPos;
        }
    }
}