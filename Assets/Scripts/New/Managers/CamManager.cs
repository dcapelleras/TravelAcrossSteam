using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Yarn.Unity;

public class CamManager : MonoBehaviour
{
    public static CamManager instance;

    public List<CinemachineVirtualCamera> cinemachines;

    DialogueRunner dialogueRunner;

    float shakeTimer;
    int camActive;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<int>("camera", MoveToCam);
        dialogueRunner.AddCommandHandler<int, float, float>("shake", ShakeCam);
    }

    public void MoveToCam(int camIndex)
    {
        camActive= camIndex;
        for (int i = 0; i < cinemachines.Count; i++)
        {
            if (i != camIndex)
            {
                cinemachines[i].Priority = 9;
            }
            else
            {
                cinemachines[i].Priority = 11;
            }
        }
    }

    public void ShakeCam(int camIndex, float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin noise = cinemachines[camIndex].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain= intensity;
        shakeTimer = time;
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
        }
        else if (shakeTimer <=0)
        {
            ShakeCam(camActive, 0f, 0f);
        }
    }
}
