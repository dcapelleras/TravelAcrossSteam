using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePuzzle : MonoBehaviour
{
    //que si interactuas con el objeto adecuado se resuelva el puzzle
    [SerializeField] GameObject requiredObject;
    bool resolved = false;
    public Transform moveToPosition;

    public void Interact(string objName)
    {
        if (objName == requiredObject.name)
        {
            Debug.Log("puzzle resolved");
            resolved= true;
        }
        else
        {
            Debug.Log("wrong object, puzzle not resolved");
        }
    }


}
