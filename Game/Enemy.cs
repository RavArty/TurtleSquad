using UnityEngine;

using System.Collections;

public class Enemy : MonoBehaviour
{

	public LayerMask 	groundCheckLayerMask;
	public Transform 	groundCheckTransform;
	public Transform 	groundCheckTransformFront;
	public GameObject shadow;
	public float moveSpeed = 2f;		// The speed the enemy moves at.
	public int HP = 2;					// How many times the enemy can be hit before it dies.

	public float damage = 10;

	
	private Animator anim;
	private AudioSource[] audio;
	private bool attack = false;
	private SpriteRenderer ren;			// Reference to the sprite renderer.
	private Transform underCheck;
	private Transform underCheckBack;

	[HideInInspector]
	public bool dead = false;			// Whether or not the enemy is dead.

	private bool doNotFlip = false;
	private float position;
	[HideInInspector]
	public Vector3 startPosition;
	private float size;
	public bool stopWalking = false;
	private bool grounded;
	private bool groundedFront;
	private Transform center;
	public bool appear = false;
	private Rigidbody2D objRigidBody;

	
	
	void Awake()
	{
		audio = GetComponents<AudioSource>();
		anim = GetComponent<Animator> ();
		ren = GetComponent<SpriteRenderer>();
		objRigidBody = GetComponent<Rigidbody2D> ();

		size = gameObject.GetComponent<CircleCollider2D>().bounds.size.x*2;
	//	startPosition = gameObject.transform.position;

		position = transform.position.x;

		
	}

	
	
	void FixedUpdate ()
	{

		grounded = Physics2D.OverlapCircle(groundCheckTransform.position, 0.2f, groundCheckLayerMask);
		groundedFront = Physics2D.OverlapCircle(groundCheckTransformFront.position, 0.2f, groundCheckLayerMask);

		if(grounded || groundedFront){
			if(!stopWalking){
				anim.SetBool("Walk", true);
				shadow.SetActive(true);
			}
		}else{
				anim.SetBool("Walk", false);
				shadow.SetActive(false);
				objRigidBody.AddForce(Vector2.down* 50);
			
		
		}

		//-----------------------------------------------------------------------------------------------------------------------------		

		if(!dead && (grounded || groundedFront) && transform.GetComponent<Rigidbody2D>().velocity.y > -2 && !attack && !appear && !stopWalking){
			
			transform.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, transform.localScale.y * moveSpeed/2);	
		
		}

	}

	IEnumerator TransformEnemy(){
		yield return new WaitForSeconds(1.5f);
		gameObject.transform.position = startPosition;
		stopWalking = true;
		appear = true;
		anim.SetTrigger("Appear");
		StartCoroutine(FireAfterAppearance());
	}
	IEnumerator FireAfterAppearance(){
		yield return new WaitForSeconds(0.5f);
		AfterAppearance();
	}


	IEnumerator DestroyGreen(GameObject obj)
	{	
		yield return new WaitForSeconds(0.8f);
		attack = false;
		if(!obj.GetComponent<Health>().isDead){
			obj.gameObject.GetComponent<Health>().LionAttack(transform.gameObject, obj);
		}
	}
	
	IEnumerator DestroyBlue(GameObject obj)
	{	
		yield return new WaitForSeconds(0.8f);
		attack = false;
		if(!obj.GetComponent<Health>().isDead){
			obj.gameObject.GetComponent<Health>().LionAttack(transform.gameObject, obj);
		}
	}
	
	public void Death()
	{

		stopWalking = true;
		attack = false;
		ObjectPool.current.PoolObject (gameObject);
		gameObject.GetComponent<LionEffect>().CreateLionsEffect();
	
		dead = true;
	}
	
	
	public void Flip()
	{ 
		if(!dead){
			position = transform.position.x;
			if(!doNotFlip){
			Vector3 enemyScale = transform.localScale;
			enemyScale.x *= -1;
			transform.localScale = enemyScale;
			}
		}
	}
	public void TranformTo(){
		StartCoroutine(TransformEnemy());
	}
	void LionRelocation(){
		GameObject[] allCoins = GameObject.FindGameObjectsWithTag("CoinsTrigger");
		GameObject[] coinsObj;
		int j = 0;
		for(int i = 0; i < allCoins.Length; i++){
			if(!allCoins[i].GetComponent<BlockOccupation>().occupied){
				j += 1;
			}
		}

		coinsObj = new GameObject[j];
		int k = 0;
		for(int i = 0; i < allCoins.Length; i++){
			if(!allCoins[i].GetComponent<BlockOccupation>().occupied){
				coinsObj[k] = allCoins[i];
				k += 1;
			}
		}
		int random = Random.Range(0, coinsObj.Length);
		gameObject.transform.position = coinsObj[random].transform.position;
	}

	public void FrontCheck(GameObject obj){
		if((obj.tag == "Obstacle" || obj.tag == "cannon" || obj.tag == "gates" || obj.tag == "stone" || obj.tag == "spike" || obj.tag == "stoneObstacle" )){ // && !transform.GetComponent<Collider2D>().isTrigger){
			if(Mathf.Abs(transform.position.x - position) > size/8){
				Flip ();
			}else{
				stopWalking = true;
			}
		}else if(obj.tag == "green" && !attack && !dead && !obj.gameObject.GetComponent<Health>().isDead){
			
			if(KeepDataOnPlayMode.instance.isSoundOn){
				audio[0].Play();
			}
			attack = true;
			anim.SetTrigger("Attack");
			anim.SetBool("Walk", false);
			stopWalking = true;
			if(obj == null){
				return;
			}

			StartCoroutine(DestroyGreen(obj));
		}else if (obj.tag == "blue" && !attack && !dead && !obj.gameObject.GetComponent<Health>().isDead){
			if(KeepDataOnPlayMode.instance.isSoundOn){
				audio[0].Play();
			}
			attack = true;
			anim.SetTrigger("Attack");
			anim.SetBool("Walk", false);
			stopWalking = true;
			if(obj == null){
				return;
			}
			StartCoroutine(DestroyBlue(obj.gameObject));
		}

	}

	public void BackCheck(GameObject obj){
		if(obj.tag == "green" && !attack && !dead && !obj.gameObject.GetComponent<Health>().isDead){

			Flip();
		}else if (obj.tag == "blue" && !attack && !dead && !obj.gameObject.GetComponent<Health>().isDead){
			Flip();
		}
	}

	public void BackCheckStone(GameObject obj){
		if(obj.tag == "stone" && !dead){
			Flip();
	}
	}
	public void AfterAppearance(){
		appear = false;
		stopWalking = false;
	}
}
