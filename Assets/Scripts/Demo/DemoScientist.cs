using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DemoScientist : MonoBehaviour
{
    public List<GameObject> listOfObjects = new List<GameObject>();
    public string requiredObject;
    public Transform moveToPosition;
    DialogueRunner dialogueRunner;
    public List<GameObject> listOfSprites= new List<GameObject>();

    public GameObject portal;

    public int missionIndex = 0;

    bool missionFinished = false;

    private void Awake()
    {
        requiredObject = listOfObjects[missionIndex].name;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    public void CheckObject(List<string> objs)
    {
        if (missionFinished)
        {
            dialogueRunner.Dialogue.Stop();
            dialogueRunner.StartDialogue("AlreadyFinishedESP");
            return;
        }
        for (int i = 0;i < objs.Count; i++) //per cada un dels items al inventory
        {
            if (objs[i] == requiredObject) //un dels objectes es el requerit
            {
                listOfSprites[i].SetActive(false);
                if (missionIndex < listOfObjects.Count -1) //si el mission index es mes petit que la llist of objects
                {
                    missionIndex++;
                    requiredObject = listOfObjects[missionIndex].name;
                    switch (missionIndex)
                    {
                        case 1:
                            dialogueRunner.Dialogue.Stop();
                            dialogueRunner.StartDialogue("Mission2ESP");
                            break;
                        case 2:
                            dialogueRunner.Dialogue.Stop();
                            dialogueRunner.StartDialogue("Mission3ESP");
                            break;
                        case 3:
                            dialogueRunner.Dialogue.Stop();
                            dialogueRunner.StartDialogue("Mission4ESP");
                            break;
                        case 4:
                            dialogueRunner.Dialogue.Stop();
                            dialogueRunner.StartDialogue("Mission5ESP");
                            break;
                        case 5:
                            dialogueRunner.Dialogue.Stop();
                            dialogueRunner.StartDialogue("Mission6ESP");
                            break;
                    }
                    return;
                }
                dialogueRunner.Dialogue.Stop();
                dialogueRunner.StartDialogue("FinalESP");
                missionFinished= true;
                portal.SetActive(true);
                return;
            }
        }
        Debug.Log("Item required not found");
    }
}
