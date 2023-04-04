using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableSocket : MonoBehaviour
{
    public int _socketNumber;

    [SerializeField] PlayerCable player;

    [SerializeField] Transform positionToPlug;

    public List<CableSocket> sockets;

    public bool completed;

    public Cable cablePlugged;

    [SerializeField] Material highlightMat;

    private void OnMouseDown()
    {
        if (cablePlugged != null)
        {
            return;
        }
        if (player.holdingCable != null)
        {
            if (player.holdingCable.socketNumber == _socketNumber)
            {
                //fix cable in the socket and set it to complete
                Debug.Log("This cable plugged correctly");
                cablePlugged = player.holdingCable;
                cablePlugged.pluggedSocket = this;
                player.holdingCable.transform.position = positionToPlug.position;
                player.holdingCable.transform.parent = null;
                player.holdingCable = null;
                completed = true;
                transform.GetComponent<Renderer>().material = highlightMat;

                for (int i = 0; i < sockets.Count; i++)
                {
                    if (!sockets[i].completed)
                    {
                        return;
                    }
                }
                Debug.Log("Congrats, all cables in place!!!!!");
            }
            else
            {
                cablePlugged = player.holdingCable;
                cablePlugged.pluggedSocket = this;
                player.holdingCable.transform.position = positionToPlug.position;
                player.holdingCable.transform.parent = null;
                player.holdingCable = null;
                Debug.Log("Plugged in the wrong socket");
            }
        }
    }
}