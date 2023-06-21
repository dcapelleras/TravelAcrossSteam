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

    [SerializeField] Vector3 limitPos;

    public int camIndex;

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
            switch (camIndex)
            {
                case 0:
                    pos.x = Mathf.Clamp(pos.x, -10f, 35f);
                    break;
                case 1:
                    pos.z = Mathf.Clamp(pos.z, -30f, 1000f);
                    break; 
                case 2:
                    pos.z = Mathf.Clamp(pos.z, 170f, 230f);
                    break;
                case 3:
                    pos.z = Mathf.Clamp(pos.z, 285f, 315f);
                    break;
                case 4:

                    break;
            }
            transform.position = pos;
        }
    }
}
