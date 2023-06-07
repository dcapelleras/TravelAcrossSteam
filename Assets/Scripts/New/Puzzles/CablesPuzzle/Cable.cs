using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public GameObject visualSubstitute;
    [SerializeField] Transform mousePosTransform;
    [SerializeField] LayerMask normalLayer;
    [SerializeField] LayerMask ignoreRayLayer;

    [SerializeField] PlayerCable player;

    Vector3 initialPosition;

    public int socketNumber;

    public CableSocket pluggedSocket;

    private void Awake()
    {
        initialPosition= transform.position;
    }

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
            if (player.holdingCable == this)
            {
                transform.position = initialPosition;
            }
            StartCoroutine(HoldingNull());
        }
    }

    IEnumerator HoldingNull()
    {
        yield return new WaitForSeconds(0.1f);
        player.holdingCable = null;
    }
}
