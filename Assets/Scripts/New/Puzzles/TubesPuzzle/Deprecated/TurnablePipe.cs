using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnablePipe : MonoBehaviour
{
    
    [SerializeField] PipesManager pipeManager;
    int currentRotation = 0;
    public bool complete = false;

    private void Update()
    {
        RaycastHit hitUp;
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hitUp, 1.5f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hitUp.distance, Color.yellow);
            complete = false;
            return;
        }
        RaycastHit hitDown;
        if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitDown, 1.5f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hitDown.distance, Color.yellow);
            complete = false;
            return;
        }
        if (!complete)
        {
            complete = true;
            pipeManager.CheckPuzzle();
        }
    }

    private void OnMouseDown()
    {
        if (currentRotation == 0)
        {
            currentRotation = 1;
            transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            currentRotation = 0;
        }
    }
}
