using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

	public GameObject remains;
	public AudioClip spike_destroy;
	public GameObject blast;
	public GameObject ui_points;
	private bool detectOnce = true;
	private GameObject remains_obj;


	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.transform.tag == "lion"){
			
			coll.gameObject.GetComponent<Enemy>().Death();
		}
		
	}


	public void DestroySpike(){
		if(detectOnce){
			MusicSound.instance.audioSources[2].clip = spike_destroy;
			MusicSound.instance.audioSources[2].Play();
			detectOnce = false;
		gameObject.GetComponent<SpriteRenderer>().sprite = null;

	
		blast.SetActive(true);
		blast.GetComponent<Animator>().SetTrigger("Blast");

		Vector2 scorePos;
		scorePos = transform.position;
		scorePos.y += 2.0f;
		Instantiate(ui_points, scorePos, Quaternion.identity);
		GameObject.FindObjectOfType<CoinScore> ().CollectCoin (10);
		

		StartCoroutine(Die());

			Collider2D[] cols = GetComponents<Collider2D>();
			foreach(Collider2D c in cols){
				c.enabled = false;
			}
		
		remains_obj = ObjectPool.current.GetObject(remains);
		remains_obj.transform.position = transform.position;
		remains_obj.transform.rotation = transform.rotation;
		remains_obj.SetActive(true);
		foreach (Transform child in remains_obj.transform)
		{
		//	int i = 0;
			var dir = child.localPosition; 
			float calc = 1 - (dir.magnitude / 10);
			if(calc <= 0){
				calc = 0;
			}

			child.GetComponent<Rigidbody2D>().AddForce(dir.normalized * calc * 800);
	
			GameObject.FindObjectOfType<FadeObject> ().FadeOut (child.transform.gameObject, 1.0f);
			
		}

		remains_obj.GetComponent<DestroyRemains>()._DestroyRemains();
		}
	}

	private IEnumerator Die(){
		yield return new WaitForSeconds(0.5f);
		Destroy (transform.gameObject);
	}
}
