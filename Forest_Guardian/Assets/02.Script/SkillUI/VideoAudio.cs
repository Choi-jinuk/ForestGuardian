using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoAudio : MonoBehaviour
{
    public AudioSource effectUiAudio;
    public VideoPlayer video;
    void Start()
    {
        video.SetTargetAudioSource(0, effectUiAudio);
    }
}
