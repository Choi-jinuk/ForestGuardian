using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPV_Change : MonoBehaviour
{
    public GameObject PP;
    public bool PPset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PP.SetActive(PPset);
        }
    }
}
