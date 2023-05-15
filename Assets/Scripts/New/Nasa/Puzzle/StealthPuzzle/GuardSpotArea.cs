using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpotArea : MonoBehaviour
{
    [SerializeField] Guard guard;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Detecting player");
            guard.DetectPlayer();
        }
    }
}
