using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public bool inPlace;
    public string tagRequired;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagRequired))
        {
            inPlace = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagRequired))
        {
            inPlace = false;
        }
    }
}
