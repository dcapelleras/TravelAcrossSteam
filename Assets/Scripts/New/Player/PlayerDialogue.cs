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
        if (dialogueRunner.IsDialogueRunning || RoomManager.instance.changingRoom)
        {
            player.AllowMovement(0);
            return;
        }
        player.AllowMovement(1);
    }
}
