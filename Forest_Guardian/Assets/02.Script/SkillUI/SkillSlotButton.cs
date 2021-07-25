using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotButton : MonoBehaviour
{
    private Vector3 targetScale;
    private Vector3 temp;
    public RectTransform thisRect;
    void Update()
    {

        temp = thisRect.localScale;
        if (temp.sqrMagnitude > 4f)
        {
            targetScale = new Vector3(1f, 1f, 1f);
        }
        if (temp.sqrMagnitude < 3.2f)
        {
            targetScale = new Vector3(1.2f, 1.2f, 1.2f);
        }
        temp = Vector3.Lerp(temp, targetScale, Time.deltaTime * 3f);
        thisRect.localScale = temp;
    }
}
