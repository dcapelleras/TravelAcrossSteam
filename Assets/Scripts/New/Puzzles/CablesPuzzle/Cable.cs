using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    //from player probably
    bool holding;
    [SerializeField] Transform mousePosTransform;
    [SerializeField] LayerMask normalLayer;
    [SerializeField] LayerMask ignireRayLayer;

    [SerializeField] PlayerCable player;

    public Vector3 initialPosition;

    public int socketNumber;

    private void OnMouseDown()
    {
        if (player.holdingCable)
        {
            return;
        }
        transform.parent = mousePosTransform;
        gameObject.layer = ignireRayLayer;
        player.holdingCable = this;
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
