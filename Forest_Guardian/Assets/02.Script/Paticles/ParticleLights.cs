using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLights : MonoBehaviour
{
    public GameObject ParticleSpirit;
    public GameObject Rkey;
    private enum targetName { Big, Small };
    targetName target_Name = targetName.Big;
    public float smoothTime = 0.5f;

    void Update()
    {
        if (target_Name == targetName.Big)
        {
            BIG();
        }
        else
        {
            SMALL();
        }

    }

    void BIG()
    {

        this.transform.localScale = new Vector3(this.transform.localScale.x + Time.deltaTime * smoothTime,
        this.transform.localScale.y + Time.deltaTime * smoothTime,
        this.transform.localScale.z);
        if (this.transform.localScale.sqrMagnitude >= 2.5f)
        {
            target_Name = targetName.Small;
        }
    }

    void SMALL()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x - Time.deltaTime * smoothTime,
        this.transform.localScale.y - Time.deltaTime * smoothTime,
        this.transform.localScale.z);
        if (this.transform.localScale.sqrMagnitude <= 1.1f)
        {
            target_Name = targetName.Big;
            ParticleSpirit.SetActive(false);
            Rkey.SetActive(false);
        }
    }
}
