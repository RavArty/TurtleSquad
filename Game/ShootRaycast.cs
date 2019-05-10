using UnityEngine;
using System.Collections;

public class ShootRaycast : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate () {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
	
	}
}
