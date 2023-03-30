using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CamCinemachine : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera cinemachine;
    public Cinemachine.CinemachineVirtualCamera cinemachine2;
    public Cinemachine.CinemachineVirtualCamera cinemachinePuzzle;

    public Transform firstCam1Focus;
    public Transform firstCam2Focus;

    public Transform puzzle;

    Camera cam;
    DialogueRunner dialogueRunner;

    [SerializeField] float roomZoom = -40f;
    float objectZoom = -20f;

    private void Awake()
    {
        cam = Camera.main;
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<int>("camera", CineChange);
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CineChange(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CineChange(2);
        }*/
    }

    public void CineChange(int camNum)
    {
        if (camNum == 1)
        {
            cinemachine.LookAt = firstCam1Focus;
            //cinemachine.Follow = firstCam1Focus;
            cinemachine.Priority = 11;
            cinemachine2.Priority = 10;
            cinemachine.gameObject.transform.position = new Vector3(cinemachine.gameObject.transform.position.x, cinemachine.gameObject.transform.position.y, roomZoom);
        }
        else if (camNum == 2)
        {
            cinemachine2.LookAt = firstCam2Focus;
            //cinemachine2.Follow = firstCam2Focus;
            cinemachine.Priority = 10;
            cinemachine2.Priority = 11;
            cinemachine2.gameObject.transform.position = new Vector3(cinemachine2.gameObject.transform.position.x, cinemachine2.gameObject.transform.position.y, roomZoom);
        }
    }

    public void ZoomOnPuzzle()
    {
        cinemachinePuzzle.Priority = 12;
        //GameManager.instance.OpenInventory();
    }

    public void ZoomBackToRoom()
    {
        cinemachinePuzzle.Priority = 9;
        //GameManager.instance.CloseInventory();
    }
}
