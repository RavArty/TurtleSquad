using UnityEngine;
using System.Collections;

public class TestGreenDeleteAfter : MonoBehaviour {

	public GameObject green;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		green.transform.RotateAround(Vector3.zero, Vector3.forward, 600 * Time.deltaTime);

	}
}
