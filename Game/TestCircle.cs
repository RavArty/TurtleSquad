using UnityEngine;
using System.Collections;

public class TestCircle : MonoBehaviour {

	public GameObject shell2;
	public GameObject shell3;
	private float axisMovement;

	public float maxSpeed = 10f; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		shell2.transform.position = transform.position;
		shell3.transform.position = transform.position;
	}

	void FixedUpdate ()
	{
		 axisMovement = Input.GetAxis ("Horizontal");


			GetComponent<Rigidbody2D>().AddForce(Vector2.right * axisMovement * maxSpeed);

	}
}
