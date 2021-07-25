using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class ESC : MonoBehaviour
{

    [Header("Panel")]
    public GameObject ESCPanel;
    public GameObject SoundPanel;
    public GameObject SavePanel;
    public GameObject RestartPanel;

    [Header("Slider")]
    public Slider UiSoundSlider;
    public Slider BgSoundSlider;
    public Slider EfSoundSlider;

    [Header("Sound Manager")]
    public SoundManager soundManager;
    public AudioSource audioSource;

    [Header("Esc Sound")]
    public AudioClip EscClip;

    private float UisoundVolume = 0.5f;
    private float BgSoundVolume = 0.2f;
    private float EfSoundVolume = 0.5f;
    //public int ClickCount;

    void Update()
    {

        EscOnClick();

    }

    //Esc 버튼을 눌렀을 때 나오게 함
    public void EscOnClick()
    {

        int clickCount = 1;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            clickCount++;
            if (!IsInvoking("EscOnClick"))
            {
                soundManager.PlayUISound(EscClip);
                Invoke("EscOnClick", 1.0f);
                ESCPanel.SetActive(true);
                Time.timeScale = 0;

            }
            else if (clickCount % 2 == 0)
            {
                soundManager.PlayUISound(EscClip);
                CancelInvoke("EscOnClick");
                ESCPanel.SetActive(false);
                SoundPanel.SetActive(false);
                SavePanel.SetActive(false);
                RestartPanel.SetActive(false);
                Time.timeScale = 1;
            }
        }


    }



    //계속하기
    public void ContinueGame()
    {
        ESCPanel.SetActive(false);
        Time.timeScale = 1;
    }


    //음향 설정
    public void SoundCheckGame()
    {
        SoundPanel.SetActive(true);
        UisoundVolume = UiSoundSlider.value;
        BgSoundVolume = BgSoundSlider.value;
        EfSoundVolume = EfSoundSlider.value;
    }



    //음향 설정 확인
    public void SoundCheckGame_ok_Y()
    {

        SoundPanel.SetActive(false);

        //PlayerPrefs으로 소리 설정한 것 저장
        BgSoundVolume = BgSoundSlider.value;
        soundManager.BGMVolume = BgSoundVolume;
        audioSource.volume = BgSoundVolume;

        EfSoundVolume = EfSoundSlider.value;
        soundManager.EffectVolume = EfSoundVolume;

        UisoundVolume = UiSoundSlider.value;
        soundManager.UIVolume = UisoundVolume;

    }


    //음향 설정 취소
    public void SoundCheckGame_ok_N()
    {
        BgSoundSlider.value = BgSoundVolume;
        EfSoundSlider.value = EfSoundVolume;
        UiSoundSlider.value = UisoundVolume;
        SoundPanel.SetActive(false);
    }


    //저장 및 종료
    public void SaveExitGame()
    {
        SavePanel.SetActive(true);
    }


    //저장 및 종료 확인
    public void SaveCheckGame_ok_Y()
    {
        SavePanel.SetActive(false);

    }



    //저장 및 종료 취소
    public void SaveCheckGame_ok_N()
    {
        SavePanel.SetActive(false);
    }


    //다시하기
    public void Restart()
    {
        RestartPanel.SetActive(true);

    }

    //다시하기 취소
    public void Restart_ok_N()
    {
        RestartPanel.SetActive(false);

    }



}


