using UnityEngine;
using System.Collections;

public class DestroyDragShot : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        Invoke("DestroyIt", 0.4f);
    }

    void DestroyIt()
    {
        Destroy(gameObject);
    }


}
