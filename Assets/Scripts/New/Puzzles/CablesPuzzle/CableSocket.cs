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

    private void OnMouseDown()
    {
        if (player.holdingCable != null)
        {
            if (player.holdingCable.socketNumber == _socketNumber)
            {
                //fix cable in the socket and set it to complete
                //**disabling scripts doesnt work, considering instantiating a prefab and disabling the objects
                Debug.Log("This fixed correctly");
                player.holdingCable.transform.position = positionToPlug.position;
                player.holdingCable.transform.parent= null;
                player.holdingCable= null;
                completed = true;

                for (int i = 0; i < sockets.Count; i++)
                {
                    if (!sockets[i].completed)
                    {
                        return;
                    }
                }
                Debug.Log("Congrats, all cables in place!!!!!");
            }
        }
    }
}