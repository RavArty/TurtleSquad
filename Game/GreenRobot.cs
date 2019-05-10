using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GreenRobot : MonoBehaviour {

	private float axisMovement = 0;	
	private float t = 0;
	public bool active = false;	
	public Sprite leftButtonActive;
	public Sprite rightButtonActive;
	public GameObject leftButton;
	public GameObject rightButton;
	private Rigidbody2D objRigidBody;

	private bool facingRight = true; // For determining which way the player is currently facing.
	
	[SerializeField] private float maxSpeed = 10f; // The fastest the player can travel in the x axis.
	[SerializeField] private float jumpForce = 400f; // Amount of force added when the player jumps.	
	
    [SerializeField] private LayerMask whatIsGround; // A mask determining what is ground to the character
	
	private Transform groundCheck; // A position marking where to check if the player is grounded.
	private float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool grounded = false; // Whether or not the player is grounded.
    private Animator anim; // Reference to the player's animator component.
	private bool alive = true;
	private int lastFingerIndex = 0;
	private int lastIndex = 0;
	private bool axisMovementBool = false;
	public bool greenDied = false;




	void Awake() {
		objRigidBody = GetComponent<Rigidbody2D> ();
		groundCheck = transform.Find("GroundCheck");
		anim = GetComponent<Animator>();
	}
	
	void FixedUpdate(){
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool("Ground", grounded);
		// Set the vertical animation
		anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		if(!transform.Find ("ActiveObject").gameObject.activeSelf){
			active = false;
		}
		
		if(transform.Find ("ActiveObject").gameObject.activeSelf){
			active = true;
		}
		//----------------------------------------------------------------------------------------------------------
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
	
	
		//----------------------------------------------------------------------------------------------------------
		// Pass all parameters to the character control script.
		Move(axisMovement);

	}
	public void Move(float move)
	{

		//only control the player if grounded or airControl is turned on
		if (grounded)
		{
		
			// The Speed animator parameter is set to the absolute value of the horizontal input.
			anim.SetFloat("Speed", Mathf.Abs(move));
			
            if(active)
			GetComponent<Rigidbody2D>().velocity = new Vector2(move*maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			
			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && facingRight)
				// ... flip the player.
				Flip();
		}
	}
	public void Die(GameObject core){
		if(alive){
			alive = false;
			if (gameObject.transform == null){
				return;
			}
			anim.SetTrigger("Die");
			greenDied = true;
			StartCoroutine(DestroyObject());
		}
	}
	IEnumerator DestroyObject(){
		yield return new WaitForSeconds(1.5f);
		Destroy(gameObject);
	}
	
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
