using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CableSocket : MonoBehaviour
{
    public int _socketNumber;

    [SerializeField] PlayerCable player;

    [SerializeField] Animator machineAnimator;

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
                player.holdingCable.visualSubstitute.SetActive(true);
                Debug.Log("This cable plugged correctly");
                cablePlugged = player.holdingCable;
                cablePlugged.pluggedSocket = this;
                player.holdingCable.transform.position = positionToPlug.position;
                player.holdingCable.transform.parent = null;
                player.holdingCable.gameObject.SetActive(false);
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
                CamManager.instance.MoveToCam(3);
                DialogueRunner runner = FindObjectOfType<DialogueRunner>();
                runner.Dialogue.Stop();
                runner.StartDialogue("FinishCables");
                player.gameObject.SetActive(false);
                machineAnimator.SetBool("abrir", true);

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