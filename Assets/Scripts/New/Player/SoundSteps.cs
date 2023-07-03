using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSteps : MonoBehaviour
{
    [SerializeField] List<AudioClip> clips= new List<AudioClip>();
    [SerializeField] AudioSource source;

    public void MakeSoundStepSound()
    {
        int randomIndex = Random.Range(0,clips.Count);
        source.PlayOneShot(clips[randomIndex]);
    }
}
