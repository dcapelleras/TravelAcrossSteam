using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    DialogueRunner dialogueRunner;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<int>("WarehouseLights", TurnOnWarehouseLights);
    }

    public void WarehouseDialogue()
    {
        
        dialogueRunner.Dialogue.Stop();
        dialogueRunner.StartDialogue("EnteringRoom");
    }

    public void TurnOnWarehouseLights(int i)
    {
        LightManager.instance.TurnOnWarehouse();
    }
}
