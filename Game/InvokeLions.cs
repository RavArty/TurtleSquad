using UnityEngine;
using System.Collections;

public class InvokeLions : MonoBehaviour {

	private bool detected = false;
	public GameObject spawner;


	void OnTriggerEnter2D(Collider2D other){
		if((other.tag == "blue" || other.tag == "green")  && !detected){
			detected = true;
		Debug.Log("spawn");
			spawner.GetComponent<Spawner>().InvokeLions();

		}	
	}
}
