using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

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
        dialogueRunner.AddCommandHandler<int>("TeleportToNasa", StartTeleport);
        dialogueRunner.AddCommandHandler<int>("OpenTutorial", OpenThisTutorial);
    }

    public void OpenThisTutorial(int i)
    {
        GameManager.instance.ShowTutorial(i);
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

    public void PrepareTeleport()
    {
        dialogueRunner.Dialogue.Stop();
        dialogueRunner.StartDialogue("StartingTeleport");
    }

    public void StartTeleport(int i)
    {
        SceneManager.LoadScene(2);
    }
}