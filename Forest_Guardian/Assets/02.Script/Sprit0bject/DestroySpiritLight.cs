using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpiritLight : MonoBehaviour
{
    public GameObject SpiritObject;
    private Vector3 targetScale = new Vector3(0f, 0f, 0f);

    private void OnEnable()
    {
        this.GetComponent<SpiritLight>().enabled = false;
        this.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
        Invoke("setActiveFalse", 3f);
    }

    private void Update()
    {
        this.transform.localScale = Vector3.Lerp(this.transform.localScale, targetScale, Time.deltaTime);
    }

    private void setActiveFalse()
    {
        this.enabled = false;
        this.GetComponent<SpiritLight>().enabled = true;
        this.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
        this.transform.parent.gameObject.transform.parent.GetComponent<RespawnSpirit>().respawnSpirit();
        SpiritObject.SetActive(false);
    }
}
