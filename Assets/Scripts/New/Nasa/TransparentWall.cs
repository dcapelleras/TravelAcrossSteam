using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : MonoBehaviour
{
    Renderer rend;
    [SerializeField]Material normalMaterial;
    [SerializeField]Material invisMaterial;

    bool playerInFront;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInFront= true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInFront = false;
        }
    }

    private void Update()
    {
        if (playerInFront)
        {
            rend.material= invisMaterial;
        }
        else
        {
            rend.material= normalMaterial;
        }
    }
}
