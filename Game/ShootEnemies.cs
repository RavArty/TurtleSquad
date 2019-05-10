using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootEnemies : MonoBehaviour {

	[SerializeField] private LayerMask ShootLayer;
	public List<GameObject> enemiesInRange;
	public GameObject trunk;
	public GameObject trunk_par;
	public float radius;
	public float inTime;
	public float fireRate;
	private float lastShotTime;
	private float nextFire;


	// Use this for initialization
	void Start () {
		enemiesInRange = new List<GameObject>();
		lastShotTime = Time.time;
		StartCoroutine(DetectObj());
	
	}
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, radius);
	}


	IEnumerator DetectObj() {
		for(;;) {
			Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, radius,ShootLayer);


			GameObject target = null;
			float minimalEnemyDistance = float.MaxValue;

//			Profiler.BeginSample("ShootEnemies for fixedupdate()");
			for(int i = 0; i < enemiesInRange.Length; i++){
				
				if(enemiesInRange[i].tag == "blue" || enemiesInRange[i].tag == "green"){
					if(!enemiesInRange[i].GetComponent<Health>().isDead){
						
						float distanceToGoal = Vector2.Distance(gameObject.transform.position, enemiesInRange[i].transform.position);
					
						if(distanceToGoal < minimalEnemyDistance){
							target = enemiesInRange[i].gameObject;
							minimalEnemyDistance = distanceToGoal;
						}
					}
				}

			}

			if(target != null  && trunk_par != null){
				
				nextFire = Time.time + fireRate;

				Vector3 direction =  target.transform.position - trunk_par.transform.position;

	//			Profiler.BeginSample("ShootEnemies lerp fixedupdate()");
				trunk_par.transform.rotation = Quaternion.Lerp(trunk_par.transform.rotation, Quaternion.AngleAxis(
					Mathf.Atan2 (direction.y, direction.x) * 180 / Mathf.PI,
					new Vector3 (0, 0, 1)), Time.deltaTime*inTime);
				float angle = Mathf.Atan2 (direction.y, direction.x) * 180 / Mathf.PI;
	//			Profiler.EndSample();

			}
			yield return new WaitForSeconds(.03f);
		}
	}

	IEnumerator ChangeAngle(Vector3  direction){


		for(float t = 0; t < 1; t+=Time.deltaTime/inTime){
		    trunk_par.transform.rotation = Quaternion.Lerp(trunk_par.transform.rotation, Quaternion.AngleAxis(
					Mathf.Atan2 (direction.y, direction.x) * 180 / Mathf.PI,
					new Vector3 (0, 0, 1)), t);
		    yield return null;
		}
	

	}

	float CalcDist(Collider2D enemy){

		return  Vector2.Distance(gameObject.transform.position, enemy.transform.position);

	}

}
