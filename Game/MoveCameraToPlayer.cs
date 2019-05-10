using UnityEngine;
using System.Collections;

public class MoveCameraToPlayer : MonoBehaviour {

	public GameObject player;

	private bool first = false;
	private bool second = false;
	private Vector3 initPos;
	private Vector3 lastPos;
	public float smooth = 100.01f;
	private float LerpTime=0.0f;
	private float Speed;

	// Use this for initialization
	void Start () {
		initPos = transform.position;
		lastPos = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z - 10);
		Speed = Vector3.Distance (lastPos, initPos) / 1f;
	//	Debug.Log("speed: " + Speed);
	}
	

	void Update ()
	{
		float moveHorizontal = Input.GetAxis ("Jump");
		if(moveHorizontal == 1){
			 first = true;
		}
		float fire = Input.GetAxis ("Fire1");
		if(fire == 1){
			second = true;
		}
	//	Debug.Log("moveHorizontal: " + moveHorizontal);
		if(first){
			transform.position = Vector3.Lerp(transform.position, lastPos, Speed * Time.deltaTime);
			transform.GetComponent<Camera>().orthographicSize = Mathf.Lerp(transform.GetComponent<Camera>().orthographicSize, 6.5f, Speed * Time.deltaTime);
		

		}
		if(second){
			first = false;
			transform.position = Vector3.Lerp(transform.position, initPos, Speed * Time.deltaTime);
			transform.GetComponent<Camera>().orthographicSize = Mathf.Lerp(transform.GetComponent<Camera>().orthographicSize, 17.6f, Speed * Time.deltaTime);


		}
	}

	IEnumerator LerpByBttn(){

		while (LerpTime < 1){
			LerpTime += Time.deltaTime*smooth;
			transform.position = Vector2.Lerp(transform.position, lastPos, LerpTime);
			transform.GetComponent<Camera>().orthographicSize = Mathf.Lerp(transform.GetComponent<Camera>().orthographicSize, 6.5f, LerpTime);

			yield return null;
		}

			

		}


}
