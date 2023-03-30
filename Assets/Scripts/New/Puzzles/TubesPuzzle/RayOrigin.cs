using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class RayOrigin : MonoBehaviour
{

    void Update()
    {
        RaycastHit hit;//origin of the ray, always shoots
        Debug.DrawRay(transform.position, transform.TransformDirection(UnityEngine.Vector3.up) * 1.5f, Color.yellow);
        if (Physics.Raycast(transform.position, transform.TransformDirection(UnityEngine.Vector3.up), out hit, 1.5f)) //direction to start the ray
        {
            if (hit.transform.TryGetComponent(out RayReceiver receiver))
            {
                receiver.ActivateRay();
            }
        }
    }
}
