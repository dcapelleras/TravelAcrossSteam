using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineFollow : MonoBehaviour
{
    public int movableAxis; //0=x, 1=z

    public bool isActive;

    NasaNavigation player;

    Vector3 pos;

    [SerializeField] float zOffset;

    private void Awake()
    {
        player = FindObjectOfType<NasaNavigation>();
        pos = transform.position;
    }

    private void LateUpdate()
    {
        if (isActive)
        {
            if (movableAxis== 0)
            {
                pos.x = player.transform.position.x;
                
            }
            else
            {
                pos.z = player.transform.position.z + zOffset;
            }
            transform.position = pos;
        }
    }
}
