using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    public Anima2D.SpriteMeshInstance[] texture;

    string layerName = "MainBackground2";
    public void Spawn()
    {
        for (var i = 0; i < texture.Length; i++)
        {
            texture[i].sortingLayerName = layerName;
        }
    }
}
