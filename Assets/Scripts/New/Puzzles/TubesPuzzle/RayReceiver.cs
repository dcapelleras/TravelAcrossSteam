using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class RayReceiver : MonoBehaviour
{ //for a ending of the puzzle, create a new script with the same receive ray but a different outcome
    public bool connected;
    [SerializeField] bool isGoal = false;

    [SerializeField] RayOrigin origin;
    [SerializeField] GameObject nextPuzzle;
    [SerializeField] GameObject nextPuzzleVisual;

    private void Update()
    {
        if (!origin.connected)
        {
            connected = false;
            return;
        }
        if (connected)
        {
            if (isGoal)
            {
                //trigger end of puzzle
                //text saying: it looks complete, but still doesn't work...
                //camera moves to the plugs
                //text saying: ohh, that's why, let me plug them in
                //puzzle
                //camera to shelf
                //lights or shake 
                //text saying: now what's happening?
                //teleports and camera moves, endgame


                nextPuzzle.SetActive(true);
                if (nextPuzzleVisual!= null)
                {
                    nextPuzzleVisual.SetActive(false);
                }
                //DialogueRunner runner = FindObjectOfType<DialogueRunner>();
                //runner.Dialogue.Stop();
                //runner.StartDialogue("PlugCable");
                this.enabled = false;
                CamManager.instance.MoveToCam(4); //create new cam and assign the index correctly
                


                Debug.Log("Congratulations, all connected");
                this.enabled= false;
                return;
            }

            RaycastHit hit1;
            RaycastHit hit2;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * 1f, Color.yellow);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1f, Color.yellow);

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit1, 1f)) //si da el ray
            {
                if (hit1.transform.TryGetComponent(out RayReceiver receiver)) //encuentra el receiver
                {
                    receiver.ActivateRay();
                }
            }
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit2, 1f)) //si da el ray
            {
                if (hit2.transform.TryGetComponent(out RayReceiver receiver)) //encuentra el receiver
                {
                    receiver.ActivateRay();
                }

            }
        }
    }

    public void ActivateRay()
    {
        connected = true;
    }
}
