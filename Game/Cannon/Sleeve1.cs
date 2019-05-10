using UnityEngine;
using System.Collections;

public class Sleeve1 : MonoBehaviour {

	[SerializeField] private LayerMask ShootLayer; // A mask determining what is ground to the character
	public Sprite laser;
	public GameObject trunk;
	public GameObject trunk_par;
	public float length = 10.0f;
	private float nextFire;
	public float fireRate;

	Animator fire;

	void Awake(){
		fire = trunk.GetComponent<Animator> ();
	}
	
	void FixedUpdate(){
		RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, length, ShootLayer);
		if(hit.collider != null){
			if((hit.collider.CompareTag("green") || hit.collider.CompareTag("blue")) && (Time.time > nextFire) ){
			if(!hit.collider.transform.GetComponent<Health>().isDead){
				nextFire = Time.time + fireRate;
				fire.SetTrigger("Fire");
			}
		}
		}


	}


}
