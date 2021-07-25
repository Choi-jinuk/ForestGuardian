using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [Header("BGM")]
    public AudioClip ForestMusic;
    public AudioClip WaterMusic;
    public AudioClip StoneMusic;
    public AudioClip FireMusic;
    public AudioClip LightMusic;

    [Header("Effect Sound")]
    public AudioClip AbsorbSpirit;

    [Header("AudioSource")]
    public AudioSource BGM;
    public AudioSource EffectUI;

    [Header("Audio Volume")]
    [Range(0f, 1f)]
    public float BGMVolume = 1f;
    [Range(0f, 1f)]
    public float EffectVolume = 0.8f;
    [Range(0f, 1f)]
    public float UIVolume = 0.8f;

    private Coroutine co;

    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        co = StartCoroutine(FadeINOUTBGM(ForestMusic));
    }
    public void PlayBGM(int BGM)
    {
        StopCoroutine(co);
        switch (BGM)
        {
            case 0:
                StartBGM(ForestMusic);
                break;
            case 1:
                StartBGM(WaterMusic);
                break;
            case 2:
                StartBGM(StoneMusic);
                break;
            case 3:
                StartBGM(FireMusic);
                break;
            case 4:
                StartBGM(LightMusic);
                break;
        }
    }

    private void StartBGM(AudioClip clip)
    {
        co = StartCoroutine(FadeINOUTBGM(clip));
    }
    IEnumerator FadeINOUTBGM(AudioClip clip)
    {
        while (BGM.volume > 0)
        {
            BGM.volume = BGM.volume - (Time.deltaTime / 1.5f);
            yield return new WaitForEndOfFrame();
        }
        BGM.volume = 0;
        ChangeBGM(clip);
        while (BGM.volume < BGMVolume)
        {
            BGM.volume = BGM.volume + (Time.deltaTime / 1.5f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

    void ChangeBGM(AudioClip clip)
    {
        BGM.clip = clip;
        BGM.loop = true;
        BGM.Play();
    }

    public void PlayEffect(AudioClip clip)
    {
        EffectUI.PlayOneShot(clip, EffectVolume);
    }

    public void PlayUISound(AudioClip clip)
    {
        EffectUI.PlayOneShot(clip, UIVolume);
    }
}