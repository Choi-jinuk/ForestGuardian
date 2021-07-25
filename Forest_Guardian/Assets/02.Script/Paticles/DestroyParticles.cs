using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    private ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        Invoke("ParticleStop", 1f);
    }
    void ParticleStop()
    {
        ps.Stop();
    }
 
}
