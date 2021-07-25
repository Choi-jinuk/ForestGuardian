using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RkeyLight : MonoBehaviour
{
    public Image image;
    private Color32 targetColor = new Color32(255,255,0,0);

    void Update()
    {
        if(image.color.a < 0.01)
        {
            targetColor = new Color32(255, 255, 0, 255);
        }
        else if(image.color.a > 0.93)
        {
            targetColor = new Color32(255, 255, 0, 0);
        }
        image.color = Color32.Lerp(image.color, targetColor, Time.deltaTime * 3f);
    }
}
