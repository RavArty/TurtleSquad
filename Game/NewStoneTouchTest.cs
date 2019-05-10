using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewStoneTouchTest : MonoBehaviour {

	public Vector2 		offSet;
	public Vector2 		offSetDownHit;
	public float 		offSetShadow;
	public Transform angleCheck;
	public Transform groundCheck;
	public Transform colliders;
	public GameObject shadow;
	public bool active = true;
	private float touchTime;
	private float t = 0;	
	private bool axisMovementBool = false;
	public float axisMovement = 0;	
	private float timeSpeed = 2;
	private bool endtouch = false;
	private float objSize = 0;	
	private Rigidbody2D objRigidBody;
	public float moveForce = 50f;	
	private float addMulti = 1;		
	private Animator anim;	
	private bool changleAngle = true;
	private float angle;
	private Quaternion target;

	public GameObject player;


	void Awake(){
		objSize = GetComponent<CircleCollider2D>().bounds.size.y * 2;
		objRigidBody = GetComponent<Rigidbody2D> ();
		anim = player.GetComponent<Animator>();
		 target = Quaternion.Euler(0, 0, 0);

	}

	void Update(){
		groundCheck.transform.position = new Vector2(transform.position.x + offSetDownHit.x, transform.position.y + offSetDownHit.y);
	//	player.transform.position = new Vector2(transform.position.x + offSet.x, transform.position.y + offSet.y);
		shadow.transform.position = new Vector2(transform.position.x, transform.position.y + offSetShadow);
		player.transform.position = transform.position;
		colliders.position = transform.position;
	}
	void FixedUpdate() {


		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
		if (Input.touchCount > 0) {

			var touch = Input.GetTouch(Input.touchCount - 1);

			switch (touch.phase) {

			case TouchPhase.Began:
				StopAllCoroutines();
			
				if(changleAngle){
					anim.SetTrigger("Roll");
				}
				touchTime = Time.time;
				if(touch.position.x > (Screen.width/2)){
					if(active){
						t += Time.smoothDeltaTime*timeSpeed;
						axisMovement = Mathf.Lerp( 0, 1, t);
					}
				}else if (touch.position.x < Screen.width/2){

					if(active){
						t += Time.smoothDeltaTime*timeSpeed;
						axisMovement = -Mathf.Lerp( 0, 1, t);

					}
				}
				if(hit.collider != null){

					if(hit.collider.gameObject.tag == "ground_tree" || hit.collider.gameObject.tag == "blue"
						|| hit.collider.gameObject.tag == "stone" || hit.collider.gameObject.tag == "green"
						|| hit.collider.gameObject.tag == "UIElement" ){
						axisMovement = 0;
						axisMovementBool = true;
					}
				}

				break;


			case TouchPhase.Stationary:
				//------------------------------------------------------------------------------------------------------

				if(touch.position.x > (Screen.width/2)){

				//	player.GetComponent<Animator>().SetTrigger("Roll");
					if(!axisMovementBool){

						if(active){
							t += Time.smoothDeltaTime*timeSpeed;
							axisMovement = Mathf.Lerp( 0, 1, t);
					
						}
					}

				}else if (touch.position.x < Screen.width/2){

					if(!axisMovementBool){


						if(active){

							t += Time.smoothDeltaTime*timeSpeed;
							axisMovement = -Mathf.Lerp( 0, 1, t);

						}
					}


				}
				if(hit.collider != null){
					if(hit.collider.gameObject.tag == "ground_tree" || hit.collider.gameObject.tag == "blue"
						|| hit.collider.gameObject.tag == "stone"|| hit.collider.gameObject.tag == "green" || hit.collider.gameObject.tag == "UIElement"){
						axisMovement = 0;
					}
				}

				break;
				//----------------------------------------------------------------------------------------------------------
			case TouchPhase.Ended:
		            int i = 0;
					i += 1;
			
					changleAngle = false;
					StartCoroutine(ChangeAngle( ));
			
				axisMovement = 0;
				axisMovementBool = false;
				t=0;
				if(active){
					//		iTween.FadeTo(greenPlayer, iTween.Hash("alpha",1,"time",0.1f));
					//		iTween.FadeTo(greenBall, iTween.Hash("alpha",0,"time",0.1f));
				}

				if(hit.collider != null){
					if(hit.collider.gameObject == gameObject){
						endtouch = true;
						active = true;

					}

				}

				break;

			}
			//-----------------------------------------------------------------------------------------------------------

		}
		if(t == 70){
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, target, Time.deltaTime * 4);
			objRigidBody.velocity = new Vector2(axisMovement*2*objSize*addMulti, objRigidBody.velocity.y);
			player.transform.RotateAround(Vector3.zero, -axisMovement*Vector3.forward, Mathf.Abs(axisMovement)*600 * Time.deltaTime);
	}
	IEnumerator ChangeAngle(){
		yield return new WaitForSeconds(2.0f);

		StartCoroutine(ChangeAngle2());
	}

	IEnumerator ChangeAngle2(){
    for(float t = 0; t < 1; t+=Time.deltaTime/0.5f){
			player.transform.rotation = Quaternion.Slerp(player.transform.rotation, target, t);

		yield return null;
	}
		anim.SetTrigger("Unroll");
	//	colliders.gameObject.SetActive(true);
		changleAngle = true;

}
}