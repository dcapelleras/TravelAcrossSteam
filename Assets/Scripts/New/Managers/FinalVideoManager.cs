using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class FinalVideoManager : MonoBehaviour
{
    DialogueRunner runner;
    [SerializeField] List<GameObject> videoList;

    int currentDiapositiveIndex = 0;

    bool videoOn = false;

    private void Awake()
    {
        runner = FindObjectOfType<DialogueRunner>();
        runner.AddCommandHandler("startVideo", StartVideo);
    }

    public void StartVideo()
    {
        videoOn= true;
        videoList[0].SetActive(true);
    }

    private void Update()
    {
        if (videoOn)
        {
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
