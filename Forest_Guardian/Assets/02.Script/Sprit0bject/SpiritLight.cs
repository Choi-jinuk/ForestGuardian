using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritLight : MonoBehaviour
{
    private Vector3 targetScale;
    void Update()
    {
        if (this.transform.localScale.sqrMagnitude > 250f)
        {
            targetScale = new Vector3(5f, 5f, 5f);
        }
        if (this.transform.localScale.sqrMagnitude < 90f)
        {
            targetScale = new Vector3(10f, 10f, 10f);
        }
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, targetScale, Time.deltaTime);
    }
}
