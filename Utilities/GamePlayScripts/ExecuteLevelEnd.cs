using UnityEngine;
using System.Collections;

public class ExecuteLevelEnd : MonoBehaviour {

	public GameObject LevelEndIntro;

	void OnTriggerEnter2D (Collider2D other) {
		
		if (other.gameObject.tag == "green"){
			if(!other.gameObject.GetComponent<Health>().isDead){
				LevelEndIntro.SetActive(true);
			}

		}
	}
}
