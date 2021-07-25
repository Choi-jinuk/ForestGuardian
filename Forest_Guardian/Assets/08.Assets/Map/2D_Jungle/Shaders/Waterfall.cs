using UnityEngine;
using System.Collections;

public class Waterfall : MonoBehaviour 
{

	public float speed = 0;

    private void Start()
    {
        this.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Background5";
    }

    void Update () 
	{
	
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (0,Time.time * speed);
	}
}
