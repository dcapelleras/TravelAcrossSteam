using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoor : MonoBehaviour
{
    public Door crossingDoor;
    PlayerNav nav;

    private void Awake()
    {
        nav= GetComponent<PlayerNav>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out Door door))
                {
                    crossingDoor = door;
                }
                else
                {
                    crossingDoor = null;
                }
            }
            
        }

        if (crossingDoor != null)
        {
            if (Vector3.Distance(crossingDoor.transform.position, transform.position) < 3f)
            {
                CrossDoor();
                RoomManager.instance.ChangeLoadingScreen(false);
            }
        }
    }

    void CrossDoor()
    {
        CamManager.instance.MoveToCam(crossingDoor.doorIndex);
        Debug.Log("Crossing door");
        //cam manager move camera and show loading screen
        //transform.position = crossingDoor.spawnPosition.position; //has to be the place of the connecting door
        nav.agent.SetDestination(crossingDoor.spawnPosition.position);
        crossingDoor = null;

    }
}
