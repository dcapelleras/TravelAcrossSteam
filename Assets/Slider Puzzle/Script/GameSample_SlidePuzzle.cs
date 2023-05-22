using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSample_SlidePuzzle : MonoBehaviour
{
    public GameObject completeObject;
    public SlidePuzzle slidePuzzle;

    public AudioSource audioSource;
    public AudioClip completeSound;

    void Start ()
    {
        completeObject.SetActive(false);

        slidePuzzle.onComplete += puzzleComplete;
    }

    private void puzzleComplete()
    {
        NasaDialogueManager.instance.FinishSlidingPuzzle();

        slidePuzzle.slideActive = false;

        completeObject.SetActive(true);
        audioSource.PlayOneShot(completeSound);

        gameObject.SetActive(false);
    }

    public void PlayAgain ()
    {
        slidePuzzle.slideActive = true;

        completeObject.SetActive(false);
        slidePuzzle.Setup();
    }
}
