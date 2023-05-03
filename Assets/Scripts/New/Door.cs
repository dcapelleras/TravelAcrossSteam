using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform spawnPosition;

    public float raydistance;
    public int doorIndex; //has to be the same as the camera to move towards

    [SerializeField] Transform player;

    bool doorOpen;

    private void Update()
    {
        Debug.DrawRay(transform.position, (player.position - transform.position).normalized * raydistance, Color.white);
        if (Vector3.Distance(transform.position, player.position) < raydistance && !doorOpen)
        {
            transform.GetComponentInParent<Animator>().SetTrigger("Open");
            doorOpen = true;
        }
        else if (Vector3.Distance(transform.position, player.position) > raydistance && doorOpen)
        {
            transform.GetComponentInParent<Animator>().SetTrigger("Close");
            doorOpen = false;
        }
    }
}