using UnityEngine;
using System.Collections;

public class DestroyCloud : MonoBehaviour {

	// Use this for initialization
	void Start(){

		Invoke("DestroyIt",0.8f);
	}

	void DestroyIt(){
		Destroy(gameObject);}
    

}
