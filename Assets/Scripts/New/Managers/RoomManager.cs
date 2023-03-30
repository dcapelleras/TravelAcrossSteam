using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class RoomManager : MonoBehaviour
{
    public static RoomManager instance;

    [SerializeField] GameObject loadingScreen;

    DialogueRunner dialogueRunner;
    
    public float loadingScreenTime = 5f;

    bool firstTimeEnteringRoom = true;

    public bool changingRoom = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }

    public void ChangeLoadingScreen(bool isEndgame)
    {
        StartCoroutine(UnloadLoadingScreen(isEndgame));
        return; //activate or deactivate loading screen
        changingRoom = true;
        loadingScreen.SetActive(true);
    }

    IEnumerator UnloadLoadingScreen(bool isEndgame)
    {
        yield return new WaitForSeconds(1f); //yield return new WaitForSeconds(loadingScreenTime); 
        changingRoom =false;
        loadingScreen.SetActive(false);
        if (isEndgame)
        {
            //dialogueRunner.Dialogue.Stop();
            //dialogueRunner.StartDialogue("EndGame");
        }
        else if (firstTimeEnteringRoom)
        {
            firstTimeEnteringRoom = false;
            dialogueRunner.Dialogue.Stop();
            dialogueRunner.StartDialogue("EnteringRoom");
        }
    }
}
