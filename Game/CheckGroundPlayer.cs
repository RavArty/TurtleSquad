using UnityEngine;
using System.Collections;

public class CheckGroundPlayer : MonoBehaviour {

	public int ground = 1;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("ground_stone") || other.CompareTag("ground_tree")){
			ground = 1;
		}
	}

		void OnTriggerExit2D(Collider2D other) {
			if(other.CompareTag("ground_stone") || other.CompareTag("ground_tree")){
				ground = 0;
			}
	}
}
