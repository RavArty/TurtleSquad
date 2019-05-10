using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D other) {
		//		Destroy (other.gameObject);
	//	Debug.Log("other end: " + other.transform.name);
		if (other.gameObject.tag == "green"){

			GameObject.Find("MenuButton").GetComponent<Button>().interactable = false;
			StartCoroutine(EndLevel());

		}
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		//		Destroy (other.gameObject);
	//	Debug.Log("other end2: " + other.transform.name);
		if (other.gameObject.tag == "green"){

			GameObject.Find("MenuButton").GetComponent<Button>().interactable = false;
			StartCoroutine(EndLevel());
			
		}
	}
	
	IEnumerator EndLevel(){
		yield return new WaitForSeconds(0.5f);
//		if(GameObject.FindObjectOfType<GameManager> ().CurrentLevel == "Level1.1"){
//			GameObject.FindObjectOfType<HintOne> ().HintOver ();
//		}

//		Transform[] objects = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];
//		foreach(Transform go in objects){
//			if(go.GetComponent<Rigidbody2D>() != null){
//				go.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
//			}
		//	if(go.GetComponent<Animation>()!= null){
		//		go.GetComponent<Animation>().isActiveAndEnabled = false;
		//	}
//		}

		GameObject[] createdEnemies = GameObject.FindGameObjectsWithTag("lion");
		if(createdEnemies.Length > 0){
			for(int i = 0; i< createdEnemies.Length; i++){
		//		Destroy (createdEnemies[i]);
			}
		//	GameObject.FindObjectOfType<Spawner> ().StopSpawn ();
		}
		GameObject.FindObjectOfType<GameManager> ().OnLevelComplete ();

	}
}
