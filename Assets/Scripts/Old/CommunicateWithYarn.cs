using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class CommunicateWithYarn : MonoBehaviour
{
    DialogueRunner dialogueRunner;
    [SerializeField] GameObject charPrefab;
    private Dictionary<string, GameObject> characters = new Dictionary<string, GameObject>();
    //public List<Transform> locations = new List<Transform>();
    //public List<Transform> camLocations = new List<Transform>();
    public Vector3 camOffset;
    public Vector3 playerOffset;
    GameObject playerInstance;

    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<int>("counter", GetNumber);
        dialogueRunner.AddCommandHandler<string, string>("spawnChar", MoveCharacter);
        characters.Add("Player", charPrefab);
        dialogueRunner.AddCommandHandler<string>("camera", MoveCamera);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            dialogueRunner.Dialogue.Stop(); //to change node in mid running you first stop it
            dialogueRunner.StartDialogue("SecondConversation"); //to start a new dialogue script
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            dialogueRunner.Dialogue.Stop(); //to change node in mid running you first stop it
            dialogueRunner.StartDialogue("Start"); //to start a new dialogue script
        }
    }

    private void MoveCamera(string locationName) //in yarn script: <<camera Corridor>> !!HAVE TO CREATE DICTIONARY OF <string, Transform>
    {
        Vector3 location = GameObject.Find(locationName).transform.position;
        location += camOffset;
        cam.transform.position = location;
    }

    void GetNumber(int i)
    {
        Debug.Log(i);
    }

    void MoveCharacter(string characterName, string locationName)
    {
        GameObject character = characters[characterName];
        Vector3 location = GameObject.Find(locationName).transform.position;
        location += playerOffset;
        if (playerInstance == null)
        {
            playerInstance = Instantiate(character);
        }
        playerInstance.transform.position = location;
    }

    [YarnCommand("something")] //HAS TO BE PUBLIC! HAS TO AFFECT AN OBJECT! MUST CALL IT USING NAME OF GAMEOBJECT
    public void DoSomething(string thing)
    {
        Debug.Log("doing " + thing);
    }



    //to play a specific text node: 
    /*if (player presses SPACE)
    then find the nearest NPC
    get that NPC's dialogue node name
    call DialogueRunner.StartDialogue() with the NPC's dialogue node
    disable player movement*/
}
