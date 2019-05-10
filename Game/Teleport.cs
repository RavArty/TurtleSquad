using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public GameObject hole2;
	[HideInInspector]
	public bool pointer = false;

	void OnTriggerEnter2D(Collider2D other) {
//		if(!hole2.GetComponent<Teleport2>().pointer){ 
			other.transform.position = hole2.transform.position;
			pointer = true;
//		}
	}

	void OnTriggerExit2D(Collider2D other){
		pointer = false;
//		hole2.GetComponent<Teleport2>().pointer = true;
	}
}
