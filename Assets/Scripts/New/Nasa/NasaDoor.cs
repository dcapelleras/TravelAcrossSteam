using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NasaDoor : MonoBehaviour
{
    public Transform crossedPos;
    public Transform inFront;

    public float raydistance;
    public int doorIndex; //has to be the same as the camera to move towards

    [SerializeField] Transform player;

    public bool isLocked = true;
    bool doorOpen;

    public int eventIndex;

    public bool changesAxisView;

    private void Update()
    {
        if (isLocked)
        {
            return;
        }
        Debug.DrawRay(transform.position, (player.position - transform.position).normalized * raydistance, Color.white);
        if (Vector3.Distance(transform.position, player.position) < raydistance && !doorOpen)
        {
            transform.GetComponent<Animator>().SetTrigger("Open");
            doorOpen = true;
        }
        else if (Vector3.Distance(transform.position, player.position) > raydistance && doorOpen)
        {
            transform.GetComponent<Animator>().SetTrigger("Close");
            doorOpen = false;
        }
    }


}
