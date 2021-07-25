using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLoad : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public Image coverScreen;
    private Color tempColor;
    private void Start()
    {
        tempColor = coverScreen.color;
    }

    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(StopBackground());
        }
    }

    IEnumerator StopBackground()
    {
        while(backgroundMusic.volume > 0f && tempColor.a < 1f)
        {
            backgroundMusic.volume -= Time.deltaTime;
            tempColor.a += Time.deltaTime;
            coverScreen.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("Loading");
    }
}
