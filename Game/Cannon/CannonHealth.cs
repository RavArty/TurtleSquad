using UnityEngine;
using System.Collections;

public class CannonHealth : MonoBehaviour {
	
	public float health = 5f;					// The player's health.
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public float damageAmount = 20f;			// The amount of damage to take when enemies touch the player
	public GameObject parent;
	public GameObject HealthBar;
	public GameObject 	ui_points;
	private AudioSource audio;
	private SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private Animator anim;						// Reference to the Animator on the player
	
	
	void Awake ()
	{
		audio = GetComponent<AudioSource>();
		healthBar = HealthBar.transform.Find("health").GetComponent<SpriteRenderer>();
		healthScale = HealthBar.transform.localScale;
	}

	
	void OnTriggerEnter2D (Collider2D col)
	{
		
		if(col.gameObject.tag == "arrow")
		{
			ObjectPool.current.PoolObject (col.gameObject);

			
			// ... and if the player still has health...
			if(health > 0f)
			{
				// ... take damage and reset the lastHitTime.
				if(KeepDataOnPlayMode.instance.isSoundOn){
					audio.Play();
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
		Debug.Log ("touched");
		health -= enemy.GetComponent<Damage>().damage;
		UpdateHealthBar();
		CheckHealth(enemy);

	}
	
	
	public void UpdateHealthBar ()
	{
		
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
		if(health < 0){health = 0;}
		HealthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
		if(HealthBar.transform.localScale.x < 0){
			Vector3 enemyScale = transform.localScale;
			enemyScale.x *= -1;
			HealthBar.transform.localScale = enemyScale;
		}
	}

	void CheckHealth (Transform col)
	{
		if(health == 0f){
			// Find all of the colliders on the gameobject and set them all to be triggers.
			Collider2D[] cols = GetComponents<Collider2D>();
			foreach(Collider2D c in cols)
			{
				c.isTrigger = true;
			}

			Vector2 scorePos;
			scorePos = transform.position;
			scorePos.y += 2.0f;
			Instantiate(ui_points, scorePos, Quaternion.identity);
			GameObject.FindObjectOfType<CoinScore> ().CollectCoin (10);
			parent.gameObject.GetComponent<CannonCollision>().DestroyCannon(col.gameObject);
		}
	}
}
