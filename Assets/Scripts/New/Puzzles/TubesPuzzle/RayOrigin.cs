using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class RayOrigin : MonoBehaviour
{

    float timeConnected;
    float maxTimeConnected = 1f;
    public bool connected;
    //to make it work, only activate ray when a piece rotates, then disconnect all
    void Update()
    {
        ActivateRay();
        if (connected)
        {
            timeConnected += Time.deltaTime;
            if (timeConnected >= maxTimeConnected )
            {
                connected = false;
                timeConnected= 0;
            }
        }
    }

    public void ActivateRay()
    {
        if (Input.GetMouseButtonDown(0))
        {
            connected= true;
            RaycastHit hit;//origin of the ray, always shoots
            Debug.DrawRay(transform.position, transform.TransformDirection(UnityEngine.Vector3.up) * 1f, Color.yellow);
            if (Physics.Raycast(transform.position, transform.TransformDirection(UnityEngine.Vector3.up), out hit, 1f)) //direction to start the ray
            {
                if (hit.transform.TryGetComponent(out RayReceiver receiver))
                {
                    receiver.ActivateRay();
                }
            }
        }
    }
}
