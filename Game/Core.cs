using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour {


	void OnTriggerEnter2D (Collider2D col){

		if(col.transform.tag == "green")
		{
//			Destroy(transform.gameObject);

		}else if(col.transform.tag == "blue"){
			
//			Destroy(transform.gameObject);

		}else if(col.transform.tag == "Obstacle")
		{
			ObjectPool.current.PoolObject (gameObject);
		//	Destroy(transform.gameObject);

		}else if(col.transform.tag == "stone" || col.transform.tag == "stoneObstacle")
		{
			if(KeepDataOnPlayMode.instance.isSoundOn){
				GetComponent<AudioSource>().Play();
			}
		//	AudioSource.PlayClipAtPoint (stone_hit, Vector3.zero, MusicSound.instance.audioSources [2].volume);
		//	Destroy(transform.gameObject);
			ObjectPool.current.PoolObject (gameObject);
		}
	}

	public void DestroyObj(){
		ObjectPool.current.PoolObject (gameObject);
	}
}
