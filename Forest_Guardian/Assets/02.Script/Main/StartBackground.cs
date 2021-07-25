using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartBackground : MonoBehaviour
{
    public Image startBackground;
    private Color tempColor;

    private void Start()
    {
        tempColor = startBackground.color;
        StartCoroutine(FadeInBackground());
    }
    IEnumerator FadeInBackground()
    {
        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime * 0.4f;
            startBackground.color = tempColor;
            yield return new WaitForEndOfFrame();
        }
        this.gameObject.SetActive(false);
    }
}
