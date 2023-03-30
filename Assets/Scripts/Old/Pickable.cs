using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public bool pickedUp;

    private void OnTriggerEnter(Collider other)
    {
        if (pickedUp)
        {
            gameObject.SetActive(false);
        }
    }
}
