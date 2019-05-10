using UnityEngine;
using System.Collections;

public class StoneObstacle : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.CompareTag("arrow") || col.CompareTag("core")){
			if(KeepDataOnPlayMode.instance.isSoundOn){
				GetComponent<AudioSource>().Play();
			}
		}
	}
}
