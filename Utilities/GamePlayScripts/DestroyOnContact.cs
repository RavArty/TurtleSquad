// Destroys object if it falls outside play area

using UnityEngine;
using System.Collections;

public class DestroyOnContact : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other) {

		if (other.gameObject.CompareTag("blue")){
			other.GetComponent<BlueTouch>().DestroyParent();

		}
		if (other.gameObject.CompareTag("green")){

			other.GetComponent<GreenTouch>().DestroyParent();
			StartCoroutine("ReloadGame");

		}
		if (other.gameObject.CompareTag("core") || other.gameObject.CompareTag("arrow") || other.gameObject.CompareTag("lion")){

			ObjectPool.current.PoolObject (other.gameObject);

		}
		if(!other.gameObject.CompareTag("green") && !other.gameObject.CompareTag("blue") && !other.gameObject.CompareTag("core")
			&& !other.gameObject.CompareTag("arrow") && !other.gameObject.CompareTag("lion")){
			other.gameObject.SetActive(false);
		//	Destroy(other.gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D other) {

		if (other.gameObject.CompareTag("blue")){
			other.transform.GetComponent<BlueTouch>().DestroyParent();
		}

		if (other.gameObject.CompareTag("green")){
			other.transform.GetComponent<GreenTouch>().DestroyParent();
			StartCoroutine("ReloadGame");
		}
		if (other.gameObject.CompareTag("core")|| other.gameObject.CompareTag("arrow") || other.gameObject.CompareTag("lion")){
			ObjectPool.current.PoolObject (other.gameObject);
		}
		if(!other.gameObject.CompareTag("green") && !other.gameObject.CompareTag("blue") && !other.gameObject.CompareTag("core")
			&& !other.gameObject.CompareTag("arrow") && !other.gameObject.CompareTag("lion")){
			other.gameObject.SetActive(false);
		//	Destroy(other.gameObject);
		}

	}


		IEnumerator ReloadGame()
		{			
			yield return new WaitForSeconds(1);
			// ... and then reload the level.
		KeepDataOnPlayMode.instance.reloadedLevel = true;
		KeepDataOnPlayMode.instance.reloadedTimes += 1;
		if (KeepDataOnPlayMode.instance.reloadedTimes > 5) {
			KeepDataOnPlayMode.instance.reloadedTimes = 0;
		}
	//	GameObject.FindObjectOfType<KeepDataOnPlayMode> ().reloadedLevel = true;

		///show ads after 5-7 times player failed
		/// put var reloadedlevel = 0;
			Application.LoadLevel(Application.loadedLevel);
		}
}
