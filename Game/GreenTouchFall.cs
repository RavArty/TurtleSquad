using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GreenTouchFall : MonoBehaviour {


	public float offSet;
	private bool grounded;


	public LayerMask groundCheckLayerMask;
	public LayerMask sideCheckLayerMask;
	public Transform groundCheckTransform;
	public Transform leftCheckTransform;
	public Transform rightCheckTransform;

	public Text coinsLabel;
	public Text distanceLabel;
	public AudioClip coinPickUp;
	public GameObject parent;					//parent object of green ball
	public GameObject obj;						//green ball
	public GameObject destroyPrefab;
	public GameObject greenPlayer; 
	public TimerCountdown	  timer;
	public float maxSpeed = 2f;
	public float moveForce = 50f;	
	public float torqueForce = 3.5f;	

	public float axisMovement = 0;		
	private bool isAxisMovementBool = false;		// do not move if touched unsuitable object
	public bool directionChosen = false;


	//---check collistion with obstacles(not enemy)------------
	private bool downGrounded = false;
	private bool leftGrounded = false;
	private bool rightGrounded = false;
	//---------------------------------------------------------
	
	private uint coins = 0;
	private GameObject remains_obj;
	private float t = 0;
	private Rigidbody2D objRigidBody;
	private bool endtouch = false;
	private float startDistY = 0;
	private float stepDist = 0;
	private bool Kinematic = false;
	private bool leftWallTouch = false;
	private bool rightWallTouch = false;
	private bool move = true;
	private int	screenSide;					// to determine touch position

	[HideInInspector]
	public int distance;

	private GameObject		leftButtonActiveObj;
	private GameObject		leftButtonNotActiveObj;
	private GameObject		rightButtonActiveObj;
	private GameObject		rightButtonNotActiveObj;



	
//---------------------------------------------------------------------------------------------------------------	
	void Awake(){
		objRigidBody = obj.GetComponent<Rigidbody2D> ();
		startDistY = obj.transform.position.y;
		stepDist = obj.GetComponent<CircleCollider2D>().bounds.size.y * 2;

		leftButtonActiveObj = GameObject.Find("LeftButton").transform.GetChild(0).gameObject;
		leftButtonNotActiveObj = GameObject.Find("LeftButton").transform.GetChild(1).gameObject;
		rightButtonActiveObj = GameObject.Find("RightButton").transform.GetChild(0).gameObject;
		rightButtonNotActiveObj = GameObject.Find("RightButton").transform.GetChild(1).gameObject;
		screenSide = Screen.width/2;
	}
//---------------------------------------------------------------------------------------------------------------
	void Start(){
		Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
		StartCoroutine(DistanceCount());
	}
//---------------------------------------------------------------------------------------------------------------
	IEnumerator DistanceCount() {
		for(;;) {
			//distance count
			distance = (int)((startDistY - obj.transform.position.y)/stepDist);
	//		distanceLabel.text = "Distance : " + distance.ToString();
			yield return new WaitForSeconds(.3f);
		}
	}

//---------------------------------------------------------------------------------------------------------------

	void Update(){
		groundCheckTransform.transform.position = new Vector2(transform.position.x, transform.position.y + offSet);
		leftCheckTransform.transform.position = new Vector2(transform.position.x + offSet, transform.position.y);
		rightCheckTransform.transform.position = new Vector2(transform.position.x - offSet, transform.position.y);
		greenPlayer.transform.position = transform.position;
	}

//---------------------------------------------------------------------------------------------------------------
	IEnumerator DestroyParent() {
		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1));
		Time.timeScale = 1;
		Destroy(parent);
	}
//---------------------------------------------------------------------------------------------------------------
	void OnTriggerEnter2D (Collider2D collider){

		if (collider.gameObject.CompareTag("Coins")){

			if(KeepDataOnPlayMode.instance.isSoundOn){
				GetComponent<AudioSource> ().Play();
			}
		}
		if (collider.gameObject.CompareTag("Line") || collider.gameObject.CompareTag("spike")){
			Time.timeScale = 0;
			timer.Stop();
			greenPlayer.GetComponent<SpriteRenderer>().sprite = null;

			GameObject destroyEffect = Instantiate (destroyPrefab, transform.position, Quaternion.identity) as GameObject;
			StartCoroutine(DestroyParent());
		}

		if (collider.transform.tag ==  "ground_stone" || collider.transform.tag ==  "ground_tree" ){
			Kinematic = true;
		}else {
			Kinematic = false;
		}
	}
//---------------------------------------------------------------------------------------------------------------

	void OnTriggerExit2D (Collider2D collider){
		if (collider.transform.tag ==  "ground_stone" || collider.transform.tag ==  "ground_tree" ){
			Kinematic = false;
			
		}	
	}
//---------------------------------------------------------------------------------------------------------------

	void CollectCoin(Collider2D coinCollider){
		coins++;
		coinsLabel.text = ": " + coins.ToString();
		coinCollider.gameObject.SetActive(false);
	}
//---------------------------------------------------------------------------------------------------------------

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.2f, groundCheckLayerMask);
		leftWallTouch = Physics2D.OverlapCircle(leftCheckTransform.position, 0.2f, sideCheckLayerMask);
		rightWallTouch = Physics2D.OverlapCircle(rightCheckTransform.position, 0.2f, sideCheckLayerMask);
		
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
		if (Input.touchCount > 0) {

			var touch = Input.GetTouch(Input.touchCount - 1);
			
			switch (touch.phase) {
			case TouchPhase.Began:


			//------------------------------------------------------------------------------------------------------
				directionChosen = true;
				if(touch.position.x > (Screen.width/2)){
					
					axisMovement = 1;
					rightButtonActiveObj.SetActive(true);
					rightButtonNotActiveObj.SetActive(false);
					
				}else if (touch.position.x < Screen.width/2){
					axisMovement = -1;
					leftButtonActiveObj.SetActive(true);
					leftButtonNotActiveObj.SetActive(false);
					}

				if(hit.collider != null){

					if(hit.collider.gameObject.CompareTag("UIElement") || hit.collider.gameObject.CompareTag("UIElementPause") ){
						axisMovement = 0;
						isAxisMovementBool = true;
					}
				}

				break;

			case TouchPhase.Stationary:
			//------------------------------------------------------------------------------------------------------
				directionChosen = true;
				if(touch.position.x > (Screen.width/2)){
					t += Time.smoothDeltaTime*4;

					axisMovement = 1;
					rightButtonActiveObj.SetActive(true);
					rightButtonNotActiveObj.SetActive(false);

				}else if (touch.position.x < Screen.width/2){
					axisMovement = -1;
					leftButtonActiveObj.SetActive(true);
					leftButtonNotActiveObj.SetActive(false);
				}

				if(hit.collider != null){

					if(hit.collider.gameObject.CompareTag("UIElement") || hit.collider.gameObject.CompareTag("UIElementPause") ){
						axisMovement = 0;
						isAxisMovementBool = true;
					}
				}
				break;
				//----------------------------------------------------------------------------------------------------------
			case TouchPhase.Ended:
				t=0;
				if(hit.collider != null){
					if(hit.collider.gameObject == obj){
						endtouch = true;
						directionChosen = false;
						rightButtonActiveObj.SetActive(false);
						rightButtonNotActiveObj.SetActive(true);
						leftButtonActiveObj.SetActive(false);
						leftButtonNotActiveObj.SetActive(true);
					}
					
				}
				break;
				
			}
			//----------------------------------------------------------------------------------------------------------
		}
		if(Input.touchCount == 0){
			rightButtonActiveObj.SetActive(false);
			rightButtonNotActiveObj.SetActive(true);
			leftButtonActiveObj.SetActive(false);
			leftButtonNotActiveObj.SetActive(true);
		}

		if((rightWallTouch || leftWallTouch) ){
			move = false;
			greenPlayer.transform.rotation = Quaternion.identity;
		}
		if(!rightWallTouch && !leftWallTouch){
			move = true;
		}

		if(!grounded){

			Vector3 movement = new Vector3(axisMovement*0.2f*stepDist, -50, 0);
			objRigidBody.velocity = movement;
		}
		if((grounded && !rightWallTouch) || (grounded && !leftWallTouch)){
			Vector3 movement = new Vector3(axisMovement*4.0f*stepDist, -2, 0);
			objRigidBody.velocity = movement;
			greenPlayer.transform.RotateAround(Vector3.zero, -axisMovement*Vector3.forward, 2.5f*360 * Time.deltaTime);
		}

	}
//----------------------------------------------------------------------------------------------------------
	public static class CoroutineUtil
	{
		public static IEnumerator WaitForRealSeconds(float time)
		{
			float start = Time.realtimeSinceStartup;
			while (Time.realtimeSinceStartup < start + time)
			{

				yield return null;
			}
		}
	}
}

