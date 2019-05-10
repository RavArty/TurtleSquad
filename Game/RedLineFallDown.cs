using UnityEngine;
using System.Collections;

public class RedLineFallDown : MonoBehaviour {

	public GameObject cameraObject;
	private float screenHeightInPoints;
	public float speed = 2.0f;
	public GreenTouchFall green;

	void Start(){
		screenHeightInPoints = Camera.main.orthographicSize/2;
	
	}


	void OnTriggerEnter2D (Collider2D col) {
	//	Debug.Log("red line2: " + col.gameObject.tag);
		if(col.CompareTag("Coins")) {
			ObjectPool.current.PoolObject (col.gameObject);

		}

	}


	// Update is called once per frame
	void FixedUpdate () {
		MoveRedLine();
	}

	void MoveRedLine(){
		if(transform.position.y > cameraObject.transform.position.y + screenHeightInPoints * 2){
		//	rigidBody.MovePosition(new Vector2 (cameraObject.transform.position.x, cameraObject.transform.position.y + screenHeightInPoints));
			transform.position = new Vector2 (cameraObject.transform.position.x, cameraObject.transform.position.y + screenHeightInPoints * 2);
		}
		if(green != null){
			int dist = green.distance / 10;
			if(dist == 0){dist = 1;}
			if(dist > 5){dist = 4;}
			transform.Translate(Vector2.down * speed * dist * Time.deltaTime);
		}
	}
}
