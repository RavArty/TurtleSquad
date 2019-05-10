using UnityEngine;
using System.Collections;

public class SleeveLion : MonoBehaviour {


	[SerializeField] private LayerMask ShootLayer; // A mask determining what is ground to the character
	public GameObject trunk;

	float LerpTime=1.0f;
	bool changeAngle = false;
	private BoxCollider2D col;
	private float stoneDist = 0;
	private float playerDist = 0;
	private bool fire = true;
	private float nextFire;
	public float fireRate;
	private bool greenDied = false;
	private bool green = false;					//at gunpoint
	private bool blue = false;					//at gunpoint

	void Awake(){
		col = GetComponent<BoxCollider2D>();
	}

	void OnTriggerExit2D(Collider2D other){
		//	Debug.Log("sleeve exit: " + other.transform.tag );
		if(other.transform.tag == "stone"){
			stoneDist = 0;
		}
		if(other.transform.tag == "green" || other.transform.tag == "blue"){
			playerDist = 0;
		}
	}
	void Update(){
		
		if(((playerDist < stoneDist && playerDist != 0) || (playerDist !=0 && stoneDist == 0)) && (Time.time > nextFire)){
		
			fire = false;
			nextFire = Time.time + fireRate;
			trunk.GetComponent<FireLion>().Fire();
		}
	}

}
