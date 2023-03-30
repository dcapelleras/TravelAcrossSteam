using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //make the player able to interact with interactable puzzles if holding the right object
    GameObject holdingObject;
    GameObject closestInteractale;
    float minInteractDistance = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (holdingObject) //requires a script to pick up objects
            {
                if (FindClosestInteractable() == null)
                {
                    Debug.Log("Not interactable found");
                    return;
                }
                if (FindClosestInteractable().TryGetComponent(out InteractablePuzzle puzzle))
                {
                    puzzle.Interact(holdingObject.name);
                    Debug.Log("interacting with " + FindClosestInteractable().name);
                }
                else
                {
                    Debug.Log("no component found");
                }
            }
        }
    }

    GameObject FindClosestInteractable()
    {
        List<GameObject> interactables = new List<GameObject>();
        interactables.AddRange(GameObject.FindGameObjectsWithTag("Interactable"));
        GameObject closest = null;
        float distance = minInteractDistance;
        for (int i = 0; i < interactables.Count; i++)
        {
            if ((interactables[i].transform.position - transform.position).magnitude < distance)
            {
                closest= interactables[i];
            }
        }
        return closest;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                holdingObject = other.gameObject;
                Debug.Log("Holding " + holdingObject.name);
            }
        }
    }
}
