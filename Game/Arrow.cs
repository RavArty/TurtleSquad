using UnityEngine;
using System.Collections;

/// <summary>
/// obstacles in front of arrow
/// </summary>
public class Arrow : MonoBehaviour {

	
	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.transform.tag == "lion")
		{
		//	Destroy (transform.gameObject);
		//	col.gameObject.GetComponent<Enemy>().Death();
		}
		else if(col.transform.tag == "Obstacle")
		{
			if(KeepDataOnPlayMode.instance.isSoundOn){
				GetComponent<AudioSource>().Play();
			}
		    ObjectPool.current.PoolObject (gameObject);
		}
		else if(col.transform.tag == "spike")
		{
			ObjectPool.current.PoolObject (gameObject);
			col.gameObject.GetComponent<Spike>().DestroySpike();
		}
		else if(col.transform.tag == "stone" || col.transform.tag == "stoneObstacle")
		{
			ObjectPool.current.PoolObject (gameObject);
			if(KeepDataOnPlayMode.instance.isSoundOn){
				GetComponent<AudioSource>().Play();
			}
		}
	}
	
	
}
