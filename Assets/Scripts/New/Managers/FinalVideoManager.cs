using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;
using Yarn.Unity;

public class FinalVideoManager : MonoBehaviour
{
    DialogueRunner runner;
    [SerializeField] List<GameObject> videoList;
    [SerializeField] GameObject lastButton;
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
        LastButtonAppear();
    }

    public async void LastButtonAppear()
    {
        await Task.Delay(60000);
        lastButton.SetActive(true);
    }

    /*
    private void Update()
    {
        
        if (videoOn)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                videoList[videoList.Count -1].SetActive(true);
            }
            if (Input.anyKeyDown)
            {
                GoToNextDiapositive();
            }
        }
}
    */

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
