using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranspWallCam : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out TransparentWall wall))
        {
            wall.BlockVision();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out TransparentWall wall))
        {
            wall.UnblockVision();
        }
    }
}
