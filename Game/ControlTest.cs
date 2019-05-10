using UnityEngine;
using System.Collections;

public class ControlTest : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	
	
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.
	
	
	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.
	
	private float axisMovement = 0;	
	private float t = 0;	
	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
	}
	
	
	void Update()
	{
		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}
	

	void FixedUpdate ()
	{

		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
		if (Input.touchCount > 0) {
			
			var touch = Input.GetTouch(Input.touchCount - 1);
			
			switch (touch.phase) {
				
			case TouchPhase.Began:
				if(touch.position.x > (Screen.width/3)*2){
				//	axisMovement = 1;
				//	t += Time.smoothDeltaTime;
				//	axisMovement = Mathf.Lerp( 0, 1, t);

				}else if (touch.position.x < Screen.width/3){

				//	t += Time.smoothDeltaTime;
				//	axisMovement = -Mathf.Lerp( 0, 1, t);
					//	axisMovement = -1;
						

					
				}else if (touch.position.x > Screen.width/3 && touch.position.x < (Screen.width/3)*2){
				//	axisMovement = 0;
				}
				//	Debug.Log(EventSystem.current.);
				

				
				break;
				
				
			case TouchPhase.Stationary:
				//------------------------------------------------------------------------------------------------------

				if(touch.position.x > (Screen.width/3)*2){
					
    				t += Time.smoothDeltaTime;
    				axisMovement = Mathf.Lerp( 0, 1, t);

					
				}else if (touch.position.x < Screen.width/3){
					
					t += Time.smoothDeltaTime;
					axisMovement = -Mathf.Lerp( 0, 1, t);

				}else if (touch.position.x > Screen.width/3 && touch.position.x < (Screen.width/3)*2){
					axisMovement = 0;
				}

				
				break;
				//----------------------------------------------------------------------------------------------------------
			case TouchPhase.Ended:

				axisMovement = 0;
				t=0;
				break;
				
			}
			//-----------------------------------------------------------------------------------------------------------
			
		}


		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(axisMovement * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			// ... add a force to the player.
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * axisMovement * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);


	}
	

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	
	public IEnumerator Taunt()
	{
		// Check the random chance of taunting.
		float tauntChance = Random.Range(0f, 100f);
		if(tauntChance > tauntProbability)
		{
			// Wait for tauntDelay number of seconds.
			yield return new WaitForSeconds(tauntDelay);
			
			// If there is no clip currently playing.
			if(!GetComponent<AudioSource>().isPlaying)
			{
				// Choose a random, but different taunt.
				tauntIndex = TauntRandom();
				
				// Play the new taunt.
				GetComponent<AudioSource>().clip = taunts[tauntIndex];
				GetComponent<AudioSource>().Play();
			}
		}
	}
	
	
	int TauntRandom()
	{
		// Choose a random index of the taunts array.
		int i = Random.Range(0, taunts.Length);
		
		// If it's the same as the previous taunt...
		if(i == tauntIndex)
			// ... try another random taunt.
			return TauntRandom();
		else
			// Otherwise return this index.
			return i;
	}
}

