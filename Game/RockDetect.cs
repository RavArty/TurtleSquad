using UnityEngine;
using System.Collections;

public class RockDetect : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {
		
		
		if (coll.transform.tag == "arrow"){
			Destroy(coll);
			Destroy(transform.gameObject);
		}
		
	}
}
