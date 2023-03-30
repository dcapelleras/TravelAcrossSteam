using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressablePlate : MonoBehaviour
{
    //If something with weight is in contact, animate it down and activate bool "activated"

    //if something with weight stops being in contact animate it up and deactivate bool "activated"

    public bool activated = false;

    //Animator anim; (set in awake)

    [SerializeField] ContraptionWithPressurePlates contraption; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Weigth")
        {
            
            activated = true;
            contraption.CheckActivation();
            //anim.setTrigger("activate"); (no loop, no time exit)
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Weigth")
        {
            activated = false;
            //anim.setTrigger("deactivate"); (no loop, no time exit)
        }
    }
}
