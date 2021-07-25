using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnSpirit : MonoBehaviour
{
    public GameObject Spirit;
    public void respawnSpirit()
    {
        Invoke("respawn", 30f);
    }

    private void respawn()
    {
        Spirit.SetActive(true);
    }
}
