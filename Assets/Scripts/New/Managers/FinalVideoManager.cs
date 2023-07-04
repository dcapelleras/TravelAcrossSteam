using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Yarn.Unity;

public class FinalVideoManager : MonoBehaviour
{
    DialogueRunner runner;
    [SerializeField] List<GameObject> videoList;
    [SerializeField] GameObject backgroundImage;

    int currentDiapositiveIndex = 0;

    bool videoOn = false;

    private void Awake()
    {
        runner = FindObjectOfType<DialogueRunner>();
        runner.AddCommandHandler("startVideo", StartVideo);
    }

    public void StartVideo()
    {
        backgroundImage.SetActive(true);
        videoOn= true;
        videoList[0].SetActive(true);
    }

    private void Update()
    {
        if (videoOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                videoList[videoList.Count].SetActive(true);
            }
            if (Input.anyKeyDown)
            {
                GoToNextDiapositive();
            }
        }
    }

    public void GoToNextDiapositive()
    {
        if ((currentDiapositiveIndex +1) >= videoList.Count)
        {
            return;
        }
        videoList[currentDiapositiveIndex].SetActive(false);
        currentDiapositiveIndex++;
        videoList[currentDiapositiveIndex].SetActive(true);
    }
}
