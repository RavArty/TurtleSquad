   using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	[HideInInspector]
	public GameObject remainsGreen;
	public bool isDead = false;
	public float health = 0;
	public float hurtForce = 100;
	public GameObject HealthBar;
	private Vector2 healthScale;
	private SpriteRenderer healthBar;
	private GameObject _remainsGreen;


	void Awake () {
		if(HealthBar != null){
			healthBar = HealthBar.transform.Find("health").GetComponent<SpriteRenderer>();
			healthScale = HealthBar.transform.localScale;
		}

	}
	IEnumerator Attack(GameObject obj){
		for(;;){
			if(obj != null)
				TakeDamage(obj.transform);
			yield return new WaitForSeconds(0.3f);
		}
	}
	void OnTriggerExit2D (Collider2D col){
		if(col.gameObject.tag == "spike"){
			StopAllCoroutines();
		}
	}
	void OnTriggerEnter2D (Collider2D col){
		if(col.gameObject.tag == "core" || col.gameObject.tag == "spike" ) {
			
			if(col.gameObject.tag == "core"){
				col.gameObject.GetComponent<Core>().DestroyObj();
			}
			if(gameObject.GetComponentInParent<GreenTouch>()){
				if(health > 1){
					if(col.gameObject.tag == "spike"){
						StartCoroutine(Attack(col.gameObject));
					}else{
						TakeDamage(col.transform);
					}
				
				}

				else if(!isDead){
					StopAllCoroutines();
					isDead = true;
					gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10*hurtForce);
					gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Patron"; 
					gameObject.layer = LayerMask.NameToLayer("Patron");
					Collider2D[] cols = GetComponents<Collider2D>();
					foreach(Collider2D c in cols){
						c.isTrigger = true;
					}
				}
			}else if(gameObject.GetComponentInParent<BlueTouch>()){
				if(health > 0){
					TakeDamage(col.transform);
				}else{
					if(!isDead){
						isDead = true;
						gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10*hurtForce);
						gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Patron"; 
						gameObject.layer = LayerMask.NameToLayer("Patron");

						Collider2D[] cols = GetComponents<Collider2D>();
						foreach(Collider2D c in cols){
							c.isTrigger = true;
						}
					}
				}
			}
		}
	}

	public void LionAttack(GameObject enemy, GameObject obj){
		if(obj.GetComponentInParent<GreenTouch>()){
			if(health > 1){
				TakeDamage(enemy.transform);
			}else{
				gameObject.GetComponent<GreenTouch>().SetImprovement(0);
			}

			if(gameObject.GetComponent<GreenTouch>().currentImpr == 0 && !isDead){
				isDead = true;
				gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10*hurtForce);
				gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Patron"; 
				gameObject.layer = LayerMask.NameToLayer("Patron");
				Collider2D[] cols = GetComponents<Collider2D>();
				foreach(Collider2D c in cols){
					c.isTrigger = true;
				}
			}
		}else if(obj.GetComponentInParent<BlueTouch>()){
			Debug.Log("blue dead0");
			if(health > 0){
				TakeDamage(enemy.transform);
			}else{
				if(!isDead){
					isDead = true;
					gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10*hurtForce);
					gameObject.transform.parent.gameObject.layer = LayerMask.NameToLayer("Patron");
					gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Patron"; 
					gameObject.layer = LayerMask.NameToLayer("Patron");
					Collider2D[] cols = GetComponents<Collider2D>();
					foreach(Collider2D c in cols){
						c.isTrigger = true;
					}
				}
			}
		}
	}


	void TakeDamage(Transform enemy){
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
		gameObject.GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);
		health -= enemy.GetComponent<Damage>().damage;
		if(health == 0 && gameObject.GetComponent<GreenTouch>().currentImpr != 0){
			gameObject.GetComponent<GreenTouch>().DestroyGreenShell();
			gameObject.GetComponent<GreenTouch>().SetImprovement(0);

		}
		UpdateHealthBar();
	}

	public void UpdateHealthBar(){
		if(healthBar != null){
			healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
			if(health < 0){health = 0;}
			HealthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
			if(HealthBar.transform.localScale.x < 0){
				Vector3 enemyScale = transform.localScale;
				enemyScale.x *= -1;
				HealthBar.transform.localScale = enemyScale;
			}
		}
	}
}
