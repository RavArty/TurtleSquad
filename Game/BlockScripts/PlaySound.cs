using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	void Start () {
		if(KeepDataOnPlayMode.instance.isSoundOn){
			GetComponent<AudioSource> ().Play();
		}
	}


	

}
 