using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTakeDown : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyIt", 0.6f);
    }

    void DestroyIt()
    {
        Destroy(this.gameObject);
    }
}
