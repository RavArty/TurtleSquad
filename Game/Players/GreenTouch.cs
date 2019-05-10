// Green player management

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GreenTouch : MonoBehaviour {

	public static GreenTouch instance;


	public float 		offSetDownHit;
	public float 		offSetShadow;
	public float 		offSetActive;
	public LayerMask 	groundCheckLayerMask;
	public Transform 	groundCheckTransform;
	public Transform	angleCheck;
	public Transform 	leftCheck;					//detect left hit
	public Transform 	rightCheck;				//detect right hit

	public GameObject greenHealth;
	public GameObject healthBar;
	public GameObject parent;
	public GameObject greenPlayer;
	public GameObject ImprAnim;
	public GameObject activeObject;
	public GameObject shadow;
	public GameObject remains;

	public float       moveForce = 50f;				//fall down velocity
	public bool        isActive = false;					//is object acitve or not
	public float       axisMovement = 0;	

	public AudioClip 	destroyGreenShell;
	public Improvements[] impr;					//IAP improvements

	[HideInInspector]
	public int currentImpr = 0;

	
	//---check collistion with obstacles(not enemy)------------
	private bool 		isGrounded;
	private bool 		isLeftGrounded;
	private bool 		isRightGrounded;
	//---------------------------------------------------------

	private float			t = 0;						//instead of getaxis value
	private Rigidbody2D		objRigidBody;
	private GameObject		shell;
	private bool			isEndtouch = false;
	private bool			isAxisMovementBool = false;	// do not move if touched unsuitable object
	private float			objSize = 0;				// diametr of object
	private float			addMulti = 1;				// smooth movement for light touch
	private float			beganTouchTime;				// detect light touch - if touch time less 0,1
	private float			endTouchTime;				// detect light touch - if touch time less 0,1
	private float			timeSpeed = 2.0f;				// axisMovement smooth increase
	private bool			isHitted = false;				// hitted ui element
	private int				screenSide;					// to determine touch position
	private GameObject		remains_obj;
	private Vector3			initpos;
	private bool			disableGates = false;

	private GameObject		leftButtonActiveObj;
	private GameObject		leftButtonNotActiveObj;
	private GameObject		rightButtonActiveObj;
	private GameObject		rightButtonNotActiveObj;


	 

//---------------------------------------------------------------------------------------------------------------
	void Awake(){

		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}

		initpos = gameObject.transform.position;
		objSize = GetComponent<CircleCollider2D>().bounds.size.y * 2;
		objRigidBody = GetComponent<Rigidbody2D> ();
		leftButtonActiveObj = GameObject.Find("LeftButton").transform.GetChild(0).gameObject;
		leftButtonNotActiveObj = GameObject.Find("LeftButton").transform.GetChild(1).gameObject;
		rightButtonActiveObj = GameObject.Find("RightButton").transform.GetChild(0).gameObject;
		rightButtonNotActiveObj = GameObject.Find("RightButton").transform.GetChild(1).gameObject;
		screenSide = Screen.width/2;
	}
//---------------------------------------------------------------------------------------------------------------

	public void GreenArmor(){

		ImprAnim.SetActive(true);
		ImprAnim.GetComponent<Animator>().SetTrigger("Appear");

		if(impr[currentImpr].healthBar){
			FadeObject.instance.FadeIn(greenHealth, 0.5f);
			StartCoroutine(MakeHealthGreen());
		}
	
	}
//---------------------------------------------------------------------------------------------------------------
	IEnumerator MakeHealthGreen(){
		
		for(float t = 0; t < 1; t += Time.deltaTime/0.5f) {
			healthBar.transform.Find("health").GetComponent<SpriteRenderer>().material.color = Color.Lerp(Color.white, Color.green, t);
			yield return null;
		}
		healthBar.transform.Find("health").GetComponent<SpriteRenderer>().material.color = Color.green;
	}
//---------------------------------------------------------------------------------------------------------------
	public void SetImprovement(int imprChoice){
		currentImpr = imprChoice;
		gameObject.GetComponent<Health>().health = impr[currentImpr].health;
		if(currentImpr == 0){
			gameObject.GetComponent<SpriteRenderer>().sprite = impr[currentImpr].imprObj.GetComponent<SpriteRenderer>().sprite;
		}else{
			GreenArmor();
		}
	}
//---------------------------------------------------------------------------------------------------------------

	void Update(){

//		Profiler.BeginSample("GreenTouch offsets update()");

		groundCheckTransform.transform.position = new Vector2(transform.position.x, transform.position.y + offSetDownHit);
		leftCheck.transform.position = new Vector2(transform.position.x + offSetDownHit, transform.position.y);
		rightCheck.transform.position = new Vector2(transform.position.x - offSetDownHit, transform.position.y);

		greenPlayer.transform.position = transform.position;
		activeObject.transform.position = new Vector2(transform.position.x, transform.position.y + offSetActive);
		shadow.transform.position = new Vector2(transform.position.x, transform.position.y + offSetShadow);

//		Profiler.EndSample();
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

				if(hit.collider != null){

					if(hit.collider.gameObject.CompareTag("ground_tree") || hit.collider.gameObject.CompareTag("blue")
						|| hit.collider.gameObject.CompareTag("stone") || hit.collider.gameObject.CompareTag("green")
							|| hit.collider.gameObject.CompareTag("UIElement")
							|| hit.collider.gameObject.CompareTag("UIElementPause") ){
						axisMovement = 0;
						isAxisMovementBool = true;
					}
				}

				if(!disableGates){
					disableGates = true;
						FadeObjectUnscaled.instance.FadeOut(GameObject.Find("EnterGate").gameObject, 0.5f);

				}

				isHitted = false;
				if(hit.collider != null){
						if(hit.collider.gameObject.CompareTag("UIElement")){
						isHitted = true;
					}
				}

				
				 beganTouchTime = Time.time;
		
				if(touch.position.x > screenSide){
			
					if(!isAxisMovementBool){
	
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
					}

				}else if (touch.position.x < screenSide){

					if(!isAxisMovementBool){
			
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
				
			
				break;

              //-----------------------------------------------------------------------------------------------------------

			}
		}
		if(Input.touchCount == 0){
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
	
		
			if(isGrounded && !gameObject.GetComponent<Health>().isDead){
				shadow.SetActive(true);

			objRigidBody.velocity = new Vector2(axisMovement*2.5f*objSize*addMulti, objRigidBody.velocity.y);
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


	public void DestroyGreenShell(){
		remains_obj = Instantiate(remains, transform.position, transform.rotation) as GameObject;

		foreach (Transform child in remains_obj.transform){
		
			var dir = child.localPosition; //- remains.transform.position;

			float calc = 1 - (dir.magnitude / 10);
			if(calc <= 0){
				calc = 0;
			}
			MusicSound.instance.audioSources[2].clip = destroyGreenShell;
			MusicSound.instance.audioSources[2].Play();
			child.GetComponent<Rigidbody2D>().AddForce(dir.normalized * calc * 800);
			GameObject.FindObjectOfType<FadeObject> ().FadeOut (child.transform.gameObject, 1.0f);

		}
		remains_obj.GetComponent<DestroyRemains>()._DestroyRemains();
	}

//---------------------------------------------------------------------------------------------------------------

	public void DestroyParent(){
		Destroy(parent);
	}

}
