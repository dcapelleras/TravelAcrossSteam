using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemPickup : MonoBehaviour //player script
{
    public GameObject pickingObj;
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void PickUp(Item item, GameObject obj)
    {
        InventoryManager_v2.instance.Add(item);
        Destroy(obj);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out ItemController itemController))
                {
                    pickingObj = itemController.gameObject;
                    
                }
                else
                {
                    pickingObj = null;
                }
            }
        }

        if (pickingObj != null)
        {
            if (Vector3.Distance(transform.position, pickingObj.transform.position) < 3f)
            {
                
                PickUp(pickingObj.GetComponent<ItemController>().item, pickingObj);
            }
        }
    }
}
