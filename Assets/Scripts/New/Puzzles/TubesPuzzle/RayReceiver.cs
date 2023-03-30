using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayReceiver : MonoBehaviour
{ //for a ending of the puzzle, create a new script with the same receive ray but a different outcome
    bool rayReceived;
    float checkTimer;
    float timeToCheck = 1f;

    private void Update()
    {
        if (rayReceived)
        {
            RaycastHit hit1;
            RaycastHit hit2;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1.5f, Color.yellow);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1.5f, Color.yellow);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit1, 1.5f)) 
            {
                if (hit1.transform.TryGetComponent(out RayReceiver receiver) )
                {
                    receiver.ActivateRay();
                }
            }
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit2, 1.5f))
            {
                if (hit2.transform.TryGetComponent(out RayReceiver receiver))
                {
                    receiver.ActivateRay();
                }
            }
        }

        if (checkTimer > 0f)
        {
            checkTimer -= Time.deltaTime;
            if (checkTimer <= 0f)
            {
                rayReceived= false;//deactivate ray
                Debug.Log("deactivating ray");
            }
        }
    }

    public void ActivateRay()
    {
        checkTimer = timeToCheck;
        if (!rayReceived)
        {
            Debug.Log("activating ray");
            rayReceived = true;
        }
    }
}
