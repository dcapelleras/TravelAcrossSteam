using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<VideoPlayer>().Play();
    }
}
