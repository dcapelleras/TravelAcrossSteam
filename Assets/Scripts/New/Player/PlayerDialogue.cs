using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class PlayerDialogue : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    PlayerNav player;

    private void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        player = GetComponent<PlayerNav>();
    }

    private void Update()
    {
        if (player == null || dialogueRunner == null)
        {
            return;
        }
        if (dialogueRunner.IsDialogueRunning || RoomManager.instance.changingRoom) //could instead block movement at the beginning of the dialogue and reset it at the end
        {
            player.AllowMovement(0);
            return;
        }
    }
}