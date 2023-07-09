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

    [SerializeField] AudioSource applauseSource;
    [SerializeField] AudioSource myManSource;

    private void Awake()
    {
        runner = FindObjectOfType<DialogueRunner>();
        runner.AddCommandHandler("startVideo", StartVideo);
        runner.AddCommandHandler("applauses", Applauses);
    }

    public void StartVideo()
    {
        videoOn= true;
        videoList[0].SetActive(true);
        StartCoroutine(LastButtonAppear());
    }

    public void Applauses()
    {
        applauseSource.Play();
        StartCoroutine(DelayApplauses());
    }

    IEnumerator DelayApplauses()
    {
        yield return new WaitForSeconds(0.5f);
        myManSource.Play();
    }

    public IEnumerator LastButtonAppear()
    {
        yield return new WaitForSeconds(40);
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
