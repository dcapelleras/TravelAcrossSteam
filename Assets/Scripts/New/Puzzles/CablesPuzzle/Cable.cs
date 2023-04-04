using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    
    [SerializeField] Transform mousePosTransform;
    [SerializeField] LayerMask normalLayer;
    [SerializeField] LayerMask ignoreRayLayer;

    [SerializeField] PlayerCable player;

    public Vector3 initialPosition;

    public int socketNumber;

    public CableSocket pluggedSocket;

    private void OnMouseDown()
    {
        if (player.holdingCable)
        {
            return;
        }
        transform.parent = mousePosTransform;
        gameObject.layer = ignoreRayLayer;
        player.holdingCable = this;
        if (pluggedSocket != null )
        {
            pluggedSocket.cablePlugged = null;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            transform.parent = null;
            gameObject.layer = normalLayer;
            player.holdingCable = null;
        }
    }
}
