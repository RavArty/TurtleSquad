// Blue player management

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BlueTouch : MonoBehaviour {

	public GameObject 	Helps;
	public float 		offSetDownHit;
	public Vector2 		offSetArrow;
	public float 		offSetShadow;
	public float 		offSetActive;
	public LayerMask 	groundCheckLayerMask;
	public Transform 	groundCheckTransform;
	public Transform 	leftCheck;					// detect left hit
	public Transform 	rightCheck;					// detect right hit


	public GameObject	parent;
	public GameObject 	arrowObj;					// arrow object
	public GameObject 	arrowObjShoot;				// shoot object inside arrow
	public GameObject 	activeObject;
	public GameObject 	ImprAnim;
	public GameObject 	shadow;
	public float	  	shootSpeed;
	public float 		moveForce = 50f;			// fall down velocity
	public bool 		isActive = false;			// is object acitve or not
	public float 		axisMovement = 0;	

	public Improvements[] impr;						// IAP improvements

	[HideInInspector]
	public int currentImpr = 0;

	//---check collistion with obstacles(not enemy)------------
	private bool 		isGrounded;
	private bool 		isLeftGrounded;
	private bool 		isRightGrounded;
	//---------------------------------------------------------

	private float 		t = 0;						// instead of getaxis value
	private Rigidbody2D objRigidBody;
	private GameObject 	shell;
	private bool 		isEndtouch = false;
	private bool 		isAxisMovementBool = false;	// do not move if touched unsuitable object
	private float 		objSize = 0;						// diametr of object
	private float 		addMulti = 1;						// smooth movement for light touch
	private float 		touchTime;						// detect light touch - if touch time less 0,2
	private float	 	timeSpeed = 0.5f;					// axisMovement smooth increase
	private float 		nextFire;
	private GameObject	ArrowSpawned;
	private GameObject 	arrow;
	private AudioSource	audio;

	private GameObject		leftButtonActiveObj;
	private GameObject		leftButtonNotActiveObj;
	private GameObject		rightButtonActiveObj;
	private GameObject		rightButtonNotActiveObj;
	private GameObject		centerButtonActiveObj;
	private GameObject		centerButtonNotActiveObj;

	public int 			UpRight;
	public int 			UpLeft;
	public int 			DownRight;
	public int 			DownLeft;
	public int 			Up;
	public int 			Down;
	public int 			Right;
	public int 			Left;

	private float 		beganTouchTime;					// detect light touch - if touch time less 0,1
	private bool 		isHitted = false;
	private int 		screenSideLeft;
	private int 		screenSideRight;
	private int 		blue_touch;					//shot direction


/*-----------------------------
 * arrow position
 * blue_touch = 11		//up right
 * blue_touch = 12		//up left
 * blue_touch = 21		//down right
 * blue_touch = 22		//down left
 * blue_touch = 3		//up
 * blue_touch = 4		//down
 * blue_touch = 5		//right
 * blue_touch = 6		//left
-----------------------------*/



//---------------------------------------------------------------------------------------------------------------
	void Awake(){
		
		audio = GetComponent<AudioSource>();
		objSize = GetComponent<CircleCollider2D>().bounds.size.y * 2;
		objRigidBody = GetComponent<Rigidbody2D> ();
		leftButtonActiveObj = GameObject.Find("LeftButton").transform.GetChild(0).gameObject;
		leftButtonNotActiveObj = GameObject.Find("LeftButton").transform.GetChild(1).gameObject;
		rightButtonActiveObj = GameObject.Find("RightButton").transform.GetChild(0).gameObject;
		rightButtonNotActiveObj = GameObject.Find("RightButton").transform.GetChild(1).gameObject;
		centerButtonActiveObj = GameObject.Find("CenterButton").transform.GetChild(0).gameObject;
		centerButtonNotActiveObj = GameObject.Find("CenterButton").transform.GetChild(1).gameObject;
		screenSideLeft = Screen.width/3;
		screenSideRight = (Screen.width/3)*2;

// Init position
		if (UpRight == 1){
			blue_touch = 11;
			//	arrow_obj.transform.rotation = Quaternion.Lerp(arrow_obj.transform.rotation, Quaternion.Euler(0,0,-45), Time.deltaTime*10);
			impr[currentImpr].imprObj.transform.rotation = Quaternion.Euler(0,0,33);
		}else if (DownRight == 1){
			blue_touch = 21;
			//	arrow_obj.transform.rotation = Quaternion.Lerp(arrow_obj.transform.rotation, Quaternion.Euler(0,0,-135), Time.deltaTime*10);
			impr[currentImpr].imprObj.transform.rotation = Quaternion.Euler(0,0,-147);
		}else if(Up == 1){
			blue_touch = 3;
		}else if(Down == 1){
			blue_touch = 4;
		}else if(Right == 1){
			blue_touch = 5;
		}else if(Left == 1){
			blue_touch = 6;
		}


	}

//---------------------------------------------------------------------------------------------------------------
	public void ImproveBlue(){
		
		if(currentImpr == 1){

			ImprAnim.SetActive(true);
			ImprAnim.GetComponent<Animator>().SetTrigger("Appear");

		}
	}

//---------------------------------------------------------------------------------------------------------------

	public void SetImprBlue(){
		
		Destroy(arrowObj);
		arrowObj = Instantiate(impr[currentImpr].imprObj, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		arrowObj.transform.SetParent(parent.transform);
		arrowObjShoot = arrowObj.transform.GetChild(0).gameObject;
		arrowObjShoot.GetComponent<InstantiateArrow>().GetParentObj(gameObject);

	}
//---------------------------------------------------------------------------------------------------------------
	public void SetImprovement(int imprChoice){
		currentImpr = imprChoice;
		ImproveBlue();
	}
//---------------------------------------------------------------------------------------------------------------

	void Update(){
		
//		Profiler.BeginSample("BlueTouch offsets update()");

		groundCheckTransform.transform.position = new Vector2(transform.position.x, transform.position.y + offSetDownHit);
		leftCheck.transform.position = new Vector2(transform.position.x + offSetDownHit, transform.position.y);
		rightCheck.transform.position = new Vector2(transform.position.x - offSetDownHit, transform.position.y);

		if(arrowObj != null){
			arrowObj.transform.position = transform.position;
		}

		activeObject.transform.position = new Vector2(transform.position.x, transform.position.y + offSetActive);
		shadow.transform.position = new Vector2(transform.position.x, transform.position.y + offSetShadow);

//		Profiler.EndSample();
	}

//---------------------------------------------------------------------------------------------------------------

	void FixedUpdate() {

//		Profiler.BeginSample("BlueTouch check colls  fixedupdate()");

		isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.2f, groundCheckLayerMask);
		isLeftGrounded = Physics2D.OverlapCircle(leftCheck.position, 0.1f, groundCheckLayerMask);
		isRightGrounded = Physics2D.OverlapCircle(rightCheck.position, 0.1f, groundCheckLayerMask);

//		Profiler.EndSample();


		if(!activeObject.activeSelf){
			isActive = false;
		}else{
			isActive = true;
		}



//		Profiler.BeginSample("BlueTouch raycast fixedupdate()");

		if(isActive){
			
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
		if (Input.touchCount > 0) {

			var touch = Input.GetTouch(Input.touchCount - 1);

			switch (touch.phase) {

			case TouchPhase.Began:

				if(hit.collider != null){

					if(hit.collider.gameObject.CompareTag("ground_tree") || hit.collider.gameObject.CompareTag("blue")
						|| hit.collider.gameObject.CompareTag("stone") || hit.collider.gameObject.CompareTag("green")
						|| hit.collider.gameObject.CompareTag("UIElement")
						|| hit.collider.gameObject.CompareTag("UIElementPause")){
						axisMovement = 0;
						isAxisMovementBool = true;
					}
				}


				isHitted = false;
				if(hit.collider != null){
						if(hit.collider.gameObject.CompareTag("UIElement")){
						isHitted = true;
					}
				}


			//	beganTouchTime = Time.time;

				if(touch.position.x > screenSideRight){
				//	if(isActive){
					if(Helps != null && !Hints.instance.isAbleToMove){
						// do nothing
					}else{
						if(!isAxisMovementBool){
							MoveRight();
						}
					}
				}else if (touch.position.x < screenSideLeft){
				//	if(isActive){
					if(Helps != null && !Hints.instance.isAbleToMove){
						// do nothing
					}else{
						if(!isAxisMovementBool){
							MoveLeft();
						}
					}
				}else if  (touch.position.x > screenSideLeft && touch.position.x < screenSideRight){
						if(isActive){
							if(hit.collider != null){
								if(hit.collider.gameObject.CompareTag("ground_tree") || hit.collider.gameObject.CompareTag("blue")
									|| hit.collider.gameObject.CompareTag("stone")|| hit.collider.gameObject.CompareTag("green") 
									|| hit.collider.gameObject.CompareTag("UIElement")
									|| hit.collider.gameObject.CompareTag("UIElementPause")){
									axisMovement = 0;
								}else{
									if(!impr[currentImpr].machineGun){
										Shoot();
									}
								}
							}else{
								if(!impr[currentImpr].machineGun){
									Shoot();
								}
							}
						}
					}

				
				break;


			case TouchPhase.Stationary:

				if(touch.position.x > screenSideRight){

					if(!isAxisMovementBool){

					//	if(active){
						if(Helps != null && !Hints.instance.isAbleToMove){
							// do nothing
						}else{
							MoveRight();
						}
					}

				}else if (touch.position.x < screenSideLeft){

					if(!isAxisMovementBool){
					//	if(isActive){
						if(Helps != null && !Hints.instance.isAbleToMove){
							// do nothing
						}else{
							MoveLeft();
						}
					}

				}else if (touch.position.x > screenSideLeft && touch.position.x < screenSideRight){
				//	if(isActive){
						if(hit.collider != null){
							if(hit.collider.gameObject.CompareTag("ground_tree") || hit.collider.gameObject.CompareTag("blue")
								|| hit.collider.gameObject.CompareTag("stone")|| hit.collider.gameObject.CompareTag("green") 
								|| hit.collider.gameObject.CompareTag("UIElement") || hit.collider.gameObject.CompareTag("UIElementPause")){
								axisMovement = 0;
							}else{
								if(impr[currentImpr].machineGun){
									Shoot();
								}
							}
						}else{
							if(impr[currentImpr].machineGun){
								Shoot();
							}
						}
				//	}
				}


				if(hit.collider != null){
						if(hit.collider.gameObject.CompareTag("ground_tree") || hit.collider.gameObject.CompareTag("blue")
							|| hit.collider.gameObject.CompareTag("stone")|| hit.collider.gameObject.CompareTag("green") 
							|| hit.collider.gameObject.CompareTag("UIElement") || hit.collider.gameObject.CompareTag("UIElementPause") ){
						axisMovement = 0;
					}
				}

				//if(isActive){RotateArrow();}
				RotateArrow();
				break;
				//----------------------------------------------------------------------------------------------------------
			case TouchPhase.Ended:
					
				rightButtonActiveObj.SetActive(false);
				rightButtonNotActiveObj.SetActive(true);
				leftButtonActiveObj.SetActive(false);
				leftButtonNotActiveObj.SetActive(true);
				centerButtonActiveObj.SetActive(false);
				centerButtonNotActiveObj.SetActive(true);
				axisMovement = 0;
				isAxisMovementBool = false;
				t=0;
				if(isActive){
					//		iTween.FadeTo(greenPlayer, iTween.Hash("alpha",1,"time",0.1f));
					//		iTween.FadeTo(greenBall, iTween.Hash("alpha",0,"time",0.1f));
				}
				if(hit.collider != null){
					if(hit.collider.gameObject.tag == "ground_tree" || hit.collider.gameObject.tag == "blue"
						|| hit.collider.gameObject.tag == "stone"|| hit.collider.gameObject.tag == "green" || hit.collider.gameObject.tag == "UIElement"
							|| hit.collider.gameObject.CompareTag("UIElementPause")){
						axisMovement = 0;
					}else{
					//	if(!impr[currentImpr].machineGun){
					//		Shoot();
					//	}
					}
				}
				if(hit.collider != null){
					if(hit.collider.gameObject == gameObject){
						isEndtouch = true;
					//	isActive = true;

					}

				}
				break;

			
			
			//-----------------------------------------------------------------------------------------------------------
			}
		}

		if (Input.touchCount == 0){
				
			axisMovement = 0;
			
			rightButtonActiveObj.SetActive(false);
			rightButtonNotActiveObj.SetActive(true);
			leftButtonActiveObj.SetActive(false);
			leftButtonNotActiveObj.SetActive(true);
			centerButtonActiveObj.SetActive(false);
			centerButtonNotActiveObj.SetActive(true);

			isAxisMovementBool = false;
			t=0;

		}

		objRigidBody.AddForce(Vector2.down* moveForce*2);
			if(!isGrounded){

			shadow.SetActive(false);
		}


			if(isGrounded && !gameObject.GetComponent<Health>().isDead){
				shadow.SetActive(true);
				objRigidBody.velocity = new Vector2(axisMovement*2*objSize*addMulti, objRigidBody.velocity.y);
		
			}
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
//		Profiler.EndSample();
	}
//---------------------------------------------------------------------------------------------------------------
	void MoveRight(){
		
		t += Time.smoothDeltaTime/timeSpeed;
		axisMovement = Mathf.Lerp( 0, 1, t);

		if( !isHitted){
			
			rightButtonActiveObj.SetActive(true);
			rightButtonNotActiveObj.SetActive(false);
		}
	}
//---------------------------------------------------------------------------------------------------------------
	void MoveLeft(){
		
		t += Time.smoothDeltaTime/timeSpeed;
		axisMovement = -Mathf.Lerp( 0, 1, t);

		if (!isHitted){
			
			leftButtonActiveObj.SetActive(true);
			leftButtonNotActiveObj.SetActive(false);
		}
	}

//---------------------------------------------------------------------------------------------------------------


	void RotateArrow(){
		if(axisMovement > 0){

			if(UpRight == 1){
				blue_touch = 11;
			}else if(DownLeft == 1){
				blue_touch = 22;
			}else if(Up == 1){
				blue_touch = 3;
			}else if(Down == 1){
				blue_touch = 4;
			}else if(Right == 1){
				blue_touch = 5;
			}
		}else if(axisMovement < 0){

			if(UpLeft == 1){
				blue_touch = 12;
			}else if(DownRight == 1){
				blue_touch = 21;
			}else if(Up == 1){
				blue_touch = 3;
			}else if(Down == 1){
				blue_touch = 4;
			}else if(Left == 1){
				blue_touch = 6;
			}
		}
	}

//---------------------------------------------------------------------------------------------------------------
	public void Shoot(){

//		Profiler.BeginSample("BlueTouch Shoot()");

		if(Time.time > nextFire){
	
			centerButtonActiveObj.SetActive(true);
			centerButtonNotActiveObj.SetActive(false);

			isEndtouch = false;
			nextFire = Time.time + impr[currentImpr].fireRate;
		
			if(!gameObject.GetComponent<Health>().isDead){
				arrowObjShoot.GetComponent<Animator>().SetTrigger("Arrow");
				if(KeepDataOnPlayMode.instance.isSoundOn){
					audio.Play();
				}
			//	MusicSound.instance.audioSources[2].clip = blue_fire;
			//	MusicSound.instance.audioSources[2].Play();
			//	AudioSource.PlayClipAtPoint (blue_fire, Vector3.zero, MusicSound.instance.audioSources [2].volume);
			}

		}
//		Profiler.EndSample();
	}
//---------------------------------------------------------------------------------------------------------------

	public void InstantiateArrow(){

		ArrowSpawned = ObjectPool.current.GetObject(impr[currentImpr].arrow);
		ArrowSpawned.transform.position = arrowObjShoot.transform.Find("Spawn").position;
		ArrowSpawned.transform.rotation = arrowObj.transform.rotation;
		ArrowSpawned.SetActive(true);
		ArrowSpawned.transform.GetComponent<Rigidbody2D>().AddForce (arrowObj.transform.up * shootSpeed);

		
	}
//---------------------------------------------------------------------------------------------------------------

	void LateUpdate(){
//		Profiler.BeginSample("BlueTouch lateupdate()");
	
		if(arrowObj != null){

			if(blue_touch == 11){
			
				arrowObj.transform.rotation = Quaternion.Lerp(arrowObj.transform.rotation, Quaternion.Euler(0,0,33), Time.deltaTime*10);
			
			}
			else if(blue_touch == 12){
			
				arrowObj.transform.rotation = Quaternion.Lerp(arrowObj.transform.rotation, Quaternion.Euler(0,0,-33), Time.deltaTime*10);
			
			}
			else if(blue_touch == 21){
			
				arrowObj.transform.rotation = Quaternion.Lerp(arrowObj.transform.rotation, Quaternion.Euler(0,0,-147), Time.deltaTime*10);
			
			}
			else if(blue_touch == 22){
	
				arrowObj.transform.rotation = Quaternion.Lerp(arrowObj.transform.rotation, Quaternion.Euler(0,0,147), Time.deltaTime*10);
			
			}
			else if(blue_touch == 3){
				arrowObj.transform.rotation = Quaternion.Euler(0,0,0);
		
			}
			else if(blue_touch == 4){
				arrowObj.transform.rotation = Quaternion.Euler(0,0,180);
			}
			else if(blue_touch == 5){
				arrowObj.transform.rotation = Quaternion.Lerp(arrowObj.transform.rotation, Quaternion.Euler(0,0,90), Time.deltaTime*10);
			
			}
			else if(blue_touch == 6){
				arrowObj.transform.rotation = Quaternion.Lerp(arrowObj.transform.rotation, Quaternion.Euler(0,0,-90), Time.deltaTime*10);
			
			}
	

		}


	}
//---------------------------------------------------------------------------------------------------------------
	public void DestroyParent(){
		Destroy(parent);
	}

}
