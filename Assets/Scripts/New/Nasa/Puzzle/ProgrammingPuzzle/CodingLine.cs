using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CodingLine : MonoBehaviour
{
    [SerializeField] CodingManager codingManager;

    public Action selectedAction;

    public int lineIndex;

    public void MarkSpot(int actionIndex)
    {
        switch (actionIndex)
        {
            case 0:
                selectedAction = Action.forward;
                break;
            case 1:
                selectedAction = Action.left; 
                break;
            case 2:
                selectedAction = Action.right;
                break;
            default:
                selectedAction = Action.none;
                break;
        }

        codingManager.UpdateCodeList(lineIndex, selectedAction);
    }
}
