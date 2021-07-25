using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeBackgroundSceen : MonoBehaviour
{
    public GameObject startBackgroundObejct;
    public Image startBackground;
    private Color tempColor;

    public void changeBackSceenFadeOut()
    {
        StartCoroutine(changeSceenFadeOut());
    }

    public void changeBackSceenFadeIn()
    {
        StartCoroutine(changeSceenFadeIn());
    }


    IEnumerator changeSceenFadeOut()
    {
        while (tempColor.a < 0.99f)
        {
            tempColor.a += Time.deltaTime * 3f;
            startBackground.color = tempColor;
            yield return new WaitForEndOfFrame();
        }

    }

    IEnumerator changeSceenFadeIn()
    {
        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime * 3f;
            startBackground.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
        startBackgroundObejct.SetActive(false);
    }
}
