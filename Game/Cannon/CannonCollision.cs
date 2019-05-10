// Rotate and destroy cannon

using UnityEngine;
using System.Collections;

public class CannonCollision : MonoBehaviour {
	

	public int 			rotation = 0;
	public GameObject 	rotateToSleeve1;
	public GameObject 	rotateToSleeve2;
	public GameObject[] laser;

	public GameObject blast;
	public GameObject destroyDot;
	public GameObject parent_trunk;
	public GameObject parent_trunk2;
	public GameObject remains_base;
	public GameObject remains_base2;
	public GameObject remains_trunk;
	public GameObject shadow;
	
	private GameObject _remains_base;
	private GameObject _remains_trunk;
	private GameObject _remains_trunk2;

	private int 		rotateTo = 0;
	private bool 		isDestroyed = false;
	private GameObject 	health;


//---------------------------------------------------------------------------------------------------------------
	void Start(){
		foreach (Transform t in transform)
		{
			if (t.name == "healthDisplay"){
				health = t.gameObject;
			}
		}
				
		for(int i = 0; i < laser.Length; i ++){
			laser[i].SetActive(false);
		}
	}
//---------------------------------------------------------------------------------------------------------------

	void FixedUpdate(){
		if(rotation == 1){
			if(parent_trunk != null && rotateToSleeve1 != null && rotateToSleeve2 != null){
				if(parent_trunk.transform.rotation == rotateToSleeve1.transform.rotation){
					rotateTo = 2;
				}else if(parent_trunk.transform.rotation == rotateToSleeve2.transform.rotation){
					rotateTo = 3;
				}
				if(rotateTo == 2){
					parent_trunk.transform.rotation = Quaternion.RotateTowards(parent_trunk.transform.rotation, rotateToSleeve2.transform.rotation, 30 * Time.deltaTime);
				}else if(rotateTo == 3){
					parent_trunk.transform.rotation = Quaternion.RotateTowards(parent_trunk.transform.rotation, rotateToSleeve1.transform.rotation, 30 * Time.deltaTime);

				}
				}
			}
	}
//---------------------------------------------------------------------------------------------------------------	

	public void DestroyCannon(GameObject col){
		if(isDestroyed){
			return;	
		}
		isDestroyed = true;

    	AudioSource.PlayClipAtPoint (cannon_destroy, Vector3.zero, MusicSound.instance.audioSources [2].volume);
		blast.SetActive(true);
		blast.GetComponent<Animator>().SetTrigger("Blast");

		if (parent_trunk2 != null){
			DestroyCannon2();
		}
		Destroy(parent_trunk);
		FadeObject.instance.FadeOut(health, 0);

		_remains_trunk = ObjectPool.current.GetObject(remains_trunk);
		_remains_trunk.transform.position = parent_trunk.transform.position;
		_remains_trunk.transform.rotation = parent_trunk.transform.rotation;
		_remains_trunk.SetActive(true);

		if (remains_base != null){
			remains_base.GetComponent<Rigidbody2D>().isKinematic = false;
			remains_base.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
			remains_base.GetComponent<Rigidbody2D>().AddTorque(100 );
			FadeObject.instance.FadeOut(remains_base.transform.gameObject, 1.0f);
		}

		for (int i = 0; i < _remains_trunk.transform.childCount; i++){

			var dir = _remains_trunk.transform.GetChild(i).localPosition; 
			float calc = 1 - (dir.magnitude / 10);
			if(calc <= 0){
				calc = 0;
			}

			_remains_trunk.transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(dir.normalized * calc * 1000);
			FadeObject.instance.FadeOut( _remains_trunk.transform.GetChild(i).gameObject, 1.0f);
		
			if(remains_base != null){
				var dir2 = _remains_trunk.transform.GetChild(i).localPosition; 
				float calc2 = 1 - (dir2.magnitude / 10);
				if(calc2 <= 0){
					calc2 = 0;
				}

				remains_base.GetComponent<Rigidbody2D>().isKinematic = false;
				remains_base.GetComponent<Rigidbody2D>().AddForce(dir2.normalized * calc2 * 300);
				FadeObject.instance.FadeOut(remains_base.transform.gameObject, 1.0f);
			
			}

			if (shadow != null){
				GameObject.FindObjectOfType<FadeObject> ().FadeOut (shadow.transform.gameObject, 1.0f);
			}	
		}

		_remains_trunk.GetComponent<DestroyRemains>().DestroyPolygonCollider();

		StartCoroutine(Die());

	}
//---------------------------------------------------------------------------------------------------------------	

	void DestroyCannon2(){
		
		Destroy(parent_trunk2);

		_remains_trunk2 = ObjectPool.current.GetObject(remains_trunk);
		_remains_trunk2.transform.position = parent_trunk2.transform.position;
		_remains_trunk2.transform.rotation = parent_trunk2.transform.rotation;
		_remains_trunk2.SetActive(true);

		if (remains_base2 != null){
			remains_base2.GetComponent<Rigidbody2D>().isKinematic = false;
			remains_base2.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
			remains_base2.GetComponent<Rigidbody2D>().AddTorque(100 );
			FadeObject.instance.FadeOut(remains_base2.transform.gameObject, 1.0f);
		}

		for (int i = 0; i < _remains_trunk2.transform.childCount; i++){

			var dir = _remains_trunk2.transform.GetChild(i).localPosition; 
			float calc = 1 - (dir.magnitude / 10);
			if(calc <= 0){
				calc = 0;
			}

			_remains_trunk2.transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(dir.normalized * calc * 1000);
			FadeObject.instance.FadeOut( _remains_trunk2.transform.GetChild(i).gameObject, 1.0f);

				
		}

		_remains_trunk2.GetComponent<DestroyRemains>().DestroyPolygonCollider();
	}
//---------------------------------------------------------------------------------------------------------------	

	private IEnumerator Die(){
		yield return new WaitForSeconds(1.0f);
		Destroy (transform.gameObject);
	}
	
}
