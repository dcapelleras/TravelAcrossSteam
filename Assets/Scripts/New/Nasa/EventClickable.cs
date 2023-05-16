using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventClickable : MonoBehaviour
{
    public int alreadyExecuted;
    public Transform placeToPick;
    public float distanceToPick;
    public virtual void ExecuteAction()
    {
        alreadyExecuted++;
    }
}
