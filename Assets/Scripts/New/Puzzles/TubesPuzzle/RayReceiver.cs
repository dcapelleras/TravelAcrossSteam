using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayReceiver : MonoBehaviour
{ //for a ending of the puzzle, create a new script with the same receive ray but a different outcome
    public bool connectedToSource;
    public bool connectedToPrevious;
    float timeToCheck = 1f;
    float timer;

    public GameObject previousObject;

    private void Update()
    {
        if (connectedToSource || connectedToPrevious)
        {
            RaycastHit hit1;
            RaycastHit hit2;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1.5f, Color.yellow);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1.5f, Color.yellow);

            if (!connectedToSource) //si no viene de la source
            {
                if (previousObject != null)
                {
                    if (CheckPrevious(previousObject)) //rastrea hasta que encuentre o no la source siguiendo los rayos
                    {
                        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit1, 1.5f)) //si da el ray
                        {
                            if (hit1.transform.TryGetComponent(out RayReceiver receiver)) //encuentra el receiver
                            {
                                receiver.ActivateRay(false, gameObject);
                            }
                        }
                        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit1, 1.5f)) //si da el ray
                        {
                            if (hit1.transform.TryGetComponent(out RayReceiver receiver)) //encuentra el receiver
                            {
                                receiver.ActivateRay(false, gameObject);
                            }
                        }
                    }
                }
            }

           
        }
        if (connectedToPrevious || connectedToSource)
        {
            timer += Time.deltaTime;
            if (timer > timeToCheck)
            {
                connectedToSource= false;
                connectedToPrevious=false;
            }
        }
    }

    bool CheckPrevious(GameObject previous)
    {
        if (previous == null)

        {
            return false;
        }
        if (previous.TryGetComponent(out RayReceiver receiver))
        {
            if (!receiver.connectedToSource)
            {
                CheckPrevious(receiver.previousObject);
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public void ActivateRay(bool fromOrigin, GameObject previous)
    {
        if (fromOrigin)
        {
            connectedToSource = true;
        }
        else
        {
            connectedToPrevious = true;
            previousObject = previous;
        }
    }
}
