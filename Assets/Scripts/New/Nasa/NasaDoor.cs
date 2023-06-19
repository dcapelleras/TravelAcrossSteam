using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NasaDoor : MonoBehaviour
{
    public Transform crossedPos;
    public Transform inFront;

    [SerializeField] Transform doorVisualToOpen;

    public float raydistance;
    public int doorIndex; //has to be the same as the camera to move towards

    [SerializeField] Transform player;

    public bool isLocked = true;

    public int eventIndex;

    public bool changesAxisView;

    [SerializeField] Vector3 openPosition;
    [SerializeField] Quaternion openRotation;

    public int indexForGuards;

    public void UnlockDoor()
    {
        isLocked= false;
        doorVisualToOpen.position = openPosition;
        doorVisualToOpen.rotation = openRotation;
    }
}
