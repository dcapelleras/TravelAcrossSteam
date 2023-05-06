using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerBase : MonoBehaviour
{
    //in case needs to be repeated, can override the bool disabling
    public int alreadyExecuted;
    public virtual void ExecuteTrigger()
    {
        alreadyExecuted++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ExecuteTrigger();
        }
    }
}
