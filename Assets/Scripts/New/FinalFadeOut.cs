using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class FinalFadeOut : MonoBehaviour
{
    DialogueRunner runner;

    private void Awake()
    {
        runner = FindObjectOfType<DialogueRunner>();
        runner.AddCommandHandler("fadeOut", FadeOut);
    }

    public void FadeOut()
    {

    }
}
