using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTextLight : MonoBehaviour
{
    private Text text;
    private Color tempColor;
    private float targetFloat;
    void Start()
    {
        text = this.GetComponent<Text>();
        tempColor = text.color;
    }

    void Update()
    {
        if(tempColor.a < 0.01f)
        {
            targetFloat = 1f;
        }
        if(tempColor.a > 0.99f)
        {
            targetFloat = -1f;
        }
        tempColor.a += Time.deltaTime * targetFloat;
        text.color = tempColor;
    }
}
