// Detect if there is smth on the block

using UnityEngine;
using System.Collections;

public class BlockOccupation : MonoBehaviour {

	public bool occupied = false;

	void OnTriggerEnter2D() {
		occupied = true;

	}

	void OnCollisionEnter2D(){
		occupied = true;
	}
}
