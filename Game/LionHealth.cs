using UnityEngine;
using System.Collections;

public class LionHealth : MonoBehaviour {
	/// <summary>
	/// scale - 309
	/// </summary>

	public float health = 100f;					// The player's health.
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public float damageAmount = 20f;			// The amount of damage to take when enemies touch the player
	public GameObject parent;
	public GameObject HealthBar;

	public GameObject 	ui_points;
	[HideInInspector]
	public bool isDead = false;
	private AudioSource[] audio;
	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
				
	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private Animator anim;						// Reference to the Animator on the player
	
	
	void Awake ()
	{
		audio = GetComponents<AudioSource>();
		anim = GetComponent<Animator> ();
		// Setting up references.
		if(HealthBar != null){
			healthBar = HealthBar.transform.Find("health").GetComponent<SpriteRenderer>();
			healthScale = HealthBar.transform.localScale;
		}
	}
	
	
	void OnTriggerEnter2D (Collider2D col)
	{
		
		if(col.gameObject.tag == "arrow" && !gameObject.GetComponent<Enemy>().appear)
		{
		//	Destroy(col.gameObject);
			ObjectPool.current.PoolObject (col.gameObject);
			
			// ... and if the player still has health...
			if(health > 0f)
			{
				// ... take damage and reset the lastHitTime.
				if(KeepDataOnPlayMode.instance.isSoundOn){
					audio[1].Play();
				}
			    TakeDamage(col.transform); 
				
			}
			// If the player doesn't have health, do some stuff, let him fall into the river to reload the level.
			else
			{
				CheckHealth(col.transform);

				
			}
		}
		
	}
	
	
	void TakeDamage (Transform enemy)
	{
		
		
		// Reduce the player's health by 10.
		health -= enemy.GetComponent<Damage>().damage;
	//	Debug.Log("lion health: " + health);
		// Update what the health bar looks like.
		UpdateHealthBar();
		CheckHealth(enemy);
		
	}
	
	
	public void UpdateHealthBar ()
	{
		
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
		
		
		// Set the scale of the health bar to be proportional to the player's health.
		if(health < 0){health = 0;}
		HealthBar.transform.localScale = new Vector3((healthScale.x * health * 0.01f), 309, 1);
		if(HealthBar.transform.localScale.x < 0){
			Vector3 enemyScale = transform.localScale;
			enemyScale.x *= -1;
			HealthBar.transform.localScale = enemyScale;
		}
	//	healthBar.transform.position = new Vector2(healthBar.transform.position.x)
		
	}

	void CheckHealth (Transform col)
	{

		if(health == 0f && !isDead){

			isDead = true;
			
			Vector2 scorePos;
			scorePos = transform.position;
			scorePos.y += 2.0f;
			Instantiate(ui_points, scorePos, Quaternion.identity);
			GameObject.FindObjectOfType<CoinScore> ().CollectCoin (10);

			parent.gameObject.GetComponent<Enemy>().Death();
		}
	}
	
}
