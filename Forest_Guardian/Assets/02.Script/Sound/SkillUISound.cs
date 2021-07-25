using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class SkillUISound : MonoBehaviour
{

    public AudioClip SkillUiSound;

    private Button button { get { return GetComponent<Button>(); } }
    public SoundManager audioSource;


    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        button.onClick.AddListener(() => PlaySound());
    }

    void PlaySound()
    {
        audioSource.PlayUISound(SkillUiSound);
    }
}
