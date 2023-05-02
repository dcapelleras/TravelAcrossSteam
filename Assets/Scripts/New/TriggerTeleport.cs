using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerTeleport : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.instance.PrepareTeleport();
            gameObject.SetActive(false);
        }
    }
}
