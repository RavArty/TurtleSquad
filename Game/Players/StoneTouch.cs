// Stone player management

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoneTouch : MonoBehaviour {


	public float 		offSetDownHit;
	public float 		offSetShadow;
	public float 		offSetActive;
	public LayerMask 	groundCheckLayerMask;
	public Transform 	groundCheckTransform;
	public Transform 	leftCheck;							// detect left hit
	public Transform 	rightCheck;							// detect right hit
		
	public GameObject parent;
	public GameObject stonePlayer;
	public GameObject activeObject;
	public GameObject shadow;

	public float moveForce = 50f;							// fall down velocity
	public bool isActive = false;								// is object acitve or not
	public float axisMovement = 0;	


	//---check collistion with obstacles(not enemy)------------
	private bool 		isGrounded;
	private bool 		isLeftGrounded;
	private bool 		isRightGrounded;
	//---------------------------------------------------------


	private float		t = 0;							// instead of getaxis value
	private Rigidbody2D objRigidBody;
	private bool 		isEndtouch = false;
	private bool 		isAxisMovementBool = false;		// do not move if touched unsuitable object
	private float 		objSize = 0;					// diametr of object
	private float 		addMulti = 1;					// smooth movement for light touch
	private float 		beganTouchTime;					// detect light touch - if touch time less 0,1
	private float 		endTouchTime;					// detect light touch - if touch time less 0,1
	private float 		timeSpeed = 2.0f;					// axisMovement smooth increase
	private bool 		jump = true;
	private bool 		isHitted = false;					// hitted ui element
	private Quaternion  target;
	private Animator 	anim;	
	private bool 		changeAngle = true;

	private int 		screenSide = 0;
	private bool		isRolled = false;

	private GameObject		leftButtonActiveObj;
	private GameObject		leftButtonNotActiveObj;
	private GameObject		rightButtonActiveObj;
	private GameObject		rightButtonNotActiveObj;


//---------------------------------------------------------------------------------------------------------------



	void Awake(){
		
		objSize = GetComponent<CircleCollider2D>().bounds.size.y * 2;
		objRigidBody = GetComponent<Rigidbody2D> ();
		leftButtonActiveObj = GameObject.Find("LeftButton").transform.GetChild(0).gameObject;
		leftButtonNotActiveObj = GameObject.Find("LeftButton").transform.GetChild(1).gameObject;
		rightButtonActiveObj = GameObject.Find("RightButton").transform.GetChild(0).gameObject;
		rightButtonNotActiveObj = GameObject.Find("RightButton").transform.GetChild(1).gameObject;
		anim = stonePlayer.GetComponent<Animator>();
		target = Quaternion.Euler(0, 0, 0);
		screenSide = Screen.width/2;


	}
//---------------------------------------------------------------------------------------------------------------
	void Update(){

		groundCheckTransform.transform.position = new Vector2(transform.position.x, transform.position.y + offSetDownHit);
		leftCheck.transform.position = new Vector2(transform.position.x + offSetDownHit, transform.position.y);
		rightCheck.transform.position = new Vector2(transform.position.x - offSetDownHit, transform.position.y);

		stonePlayer.transform.position = transform.position;
		activeObject.transform.position = new Vector2(transform.position.x, transform.position.y + offSetActive);
		shadow.transform.position = new Vector2(transform.position.x, transform.position.y + offSetShadow);


	}
//---------------------------------------------------------------------------------------------------------------

	void FixedUpdate() {


		isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.2f, groundCheckLayerMask);
		isLeftGrounded = Physics2D.OverlapCircle(leftCheck.position, 0.1f, groundCheckLayerMask);
		isRightGrounded = Physics2D.OverlapCircle(rightCheck.position, 0.1f, groundCheckLayerMask);

		if(!activeObject.activeSelf){
			isActive = false;
		}else{
			isActive = true;
		}

		if(isActive){

			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

			if (Input.touchCount > 0) {

				var touch = Input.GetTouch(Input.touchCount - 1);

				switch (touch.phase) {

				case TouchPhase.Began:
						StopAllCoroutines();

				if(hit.collider != null){

					if(hit.collider.gameObject.CompareTag("ground_tree") || hit.collider.gameObject.CompareTag("blue")
						|| hit.collider.gameObject.CompareTag("stone") || hit.collider.gameObject.CompareTag("green")
						|| hit.collider.gameObject.CompareTag("UIElement")
						|| hit.collider.gameObject.CompareTag("UIElementPause") ){
						axisMovement = 0;
						isAxisMovementBool = true;
					}
				}

					if(hit.collider != null){
						if(hit.collider.gameObject.tag != "ground_tree" && hit.collider.gameObject.tag != "blue"
							&& hit.collider.gameObject.tag != "stone" && hit.collider.gameObject.tag != "green"
							&& hit.collider.gameObject.tag != "UIElement" && hit.collider.gameObject.CompareTag("UIElementPause")){
							if(!isRolled){
								if(isActive){
									anim.SetBool("Roll", true);
									isRolled = true;
								}

							}
						}
					}else if(hit.collider == null){
						if(!isRolled){
							if(isActive){
								anim.SetBool("Roll", true);
								isRolled = true;
							}

						}
					}
				

					isHitted = false;
					if(hit.collider != null){
						if(hit.collider.gameObject.CompareTag("UIElement") || hit.collider.gameObject.CompareTag("UIElementPause")){
							isHitted = true;
						}
					}


					beganTouchTime = Time.time;

					if(touch.position.x > screenSide){
						if(!isAxisMovementBool){
							MoveRight();
						}
					}else if (touch.position.x < screenSide){
						if(!isAxisMovementBool){
							MoveLeft();
						}
					}
					break;


				case TouchPhase.Stationary:

					if(Time.time - beganTouchTime <= 0.1f){
					}else{ addMulti = 1;}

					if(touch.position.x > screenSide){

						if(!isAxisMovementBool){
							if(!isRolled){
									anim.SetBool("Roll", true);
									isRolled = true;
							}
								MoveRight();
						}

					}else if (touch.position.x < screenSide){

						if(!isAxisMovementBool){
							if(!isRolled){
								anim.SetBool("Roll", true);
								isRolled = true;
							}
								MoveLeft();
						}

					}
					if(hit.collider != null){
						if(hit.collider.gameObject.CompareTag("ground_tree") || hit.collider.gameObject.CompareTag("blue")
							|| hit.collider.gameObject.CompareTag("stone") || hit.collider.gameObject.CompareTag("green") 
							|| hit.collider.gameObject.CompareTag("UIElement")
							|| hit.collider.gameObject.CompareTag("UIElementPause")){
							axisMovement = 0;
						}
					}

					break;
					//----------------------------------------------------------------------------------------------------------
				case TouchPhase.Ended:
					endTouchTime = Time.time;


					axisMovement = 0;
					rightButtonActiveObj.SetActive(false);
					rightButtonNotActiveObj.SetActive(true);
					leftButtonActiveObj.SetActive(false);
					leftButtonNotActiveObj.SetActive(true);
					isAxisMovementBool = false;
					t=0;


					if(hit.collider != null){
						if(hit.collider.gameObject == gameObject){
							isEndtouch = true;
						}

					}
					break;

					//-----------------------------------------------------------------------------------------------------------

				}
			}
		}
			if(Input.touchCount == 0){
					StartCoroutine(ChangeAngle());
				axisMovement = 0;
				rightButtonActiveObj.SetActive(false);
				rightButtonNotActiveObj.SetActive(true);
				leftButtonActiveObj.SetActive(false);
				leftButtonNotActiveObj.SetActive(true);
				isAxisMovementBool = false;
				t=0;
			}

			objRigidBody.AddForce(Vector2.down* moveForce*2);

			if(!isGrounded){

				shadow.SetActive(false);

			}


			if(isGrounded){
				shadow.SetActive(true);

				objRigidBody.velocity = new Vector2(axisMovement*2.5f*objSize*addMulti, objRigidBody.velocity.y);
				stonePlayer.transform.RotateAround(Vector3.zero, -axisMovement*Vector3.forward, Mathf.Abs(axisMovement)*650 * Time.deltaTime);
			}
		if(!isActive){
			objRigidBody.AddForce(Vector2.down* moveForce*2);
			if(!isGrounded){

				shadow.SetActive(false);
			}
		}

		if(!isActive && isGrounded){
			shadow.SetActive(true);
		}
	}

//---------------------------------------------------------------------------------------------------------------


	void MoveRight(){
		t += Time.smoothDeltaTime*timeSpeed;
		axisMovement = Mathf.Lerp( 0, 1, t);

		if (!isHitted){
			rightButtonActiveObj.SetActive(true);
			rightButtonNotActiveObj.SetActive(false);
		}
	}

//---------------------------------------------------------------------------------------------------------------
	void MoveLeft(){
		t += Time.smoothDeltaTime*timeSpeed;
		axisMovement = -Mathf.Lerp( 0, 1, t);

		if (!isHitted){
			leftButtonActiveObj.SetActive(true);
			leftButtonNotActiveObj.SetActive(false);
		}
	}
//---------------------------------------------------------------------------------------------------------------
	IEnumerator RotateMe(Vector3 byAngles, float inTime)
	{
		Quaternion fromAngle = stonePlayer.transform.rotation ;
		Quaternion toAngle = Quaternion.Euler(stonePlayer.transform.eulerAngles + byAngles) ;
		Vector3 pos = new Vector3(transform.position.x + axisMovement*0.5f, transform.position.y,transform.position.z);
		for(float t = 0f ; t < 1f ; t += Time.deltaTime/inTime)
		{
			transform.position = Vector3.Lerp(transform.position, pos, t);
			stonePlayer.transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t) ;
			yield return null ;
		}
	}

//---------------------------------------------------------------------------------------------------------------

	IEnumerator ChangeAngle(){
		yield return new WaitForSeconds(2.0f);
		isRolled = false;
		StartCoroutine(ChangeAngle2());
	}
//---------------------------------------------------------------------------------------------------------------

	IEnumerator ChangeAngle2(){
		
			for(float t = 0; t < 1; t+=Time.deltaTime/0.5f){
				stonePlayer.transform.rotation = Quaternion.Slerp(stonePlayer.transform.rotation, target, t);
			yield return null;
		}
		anim.SetBool("Roll", false);

	}

//---------------------------------------------------------------------------------------------------------------
	public void DestroyParent(){
		Destroy(parent);
	}

}
