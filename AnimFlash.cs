using UnityEngine;
using System.Collections;
using GAF.Core;
using GAF.Assets;
using GAFInternal.Assets;

public class AnimFlash : MonoBehaviour {

	public GameObject idle;
	public GameObject bite;
	GAFMovieClip clip;
	GAFMovieClip clip2;

	void Start(){
		 clip = idle.transform.GetComponent<GAFMovieClip>();
		 clip2 = bite.transform.GetComponent<GAFMovieClip>();
	//	bite.transform.position = idle.transform.position;

	}

	void Update () {
		if (Input.GetKey("up")){
			idle.SetActive(true);
			bite.SetActive(false);
			clip.play();
		//	bite.transform.position = idle.transform.position;
		//	clip.setSequence("your_sequence_name", true/*if you want to start it immediatelly*/);
		}
		if (Input.GetKey("down")){
			idle.SetActive(false);
			bite.transform.position = idle.transform.position;
			bite.SetActive(true);
			clip2.play();
		//	clip.setSequence("your_sequence_name", true/*if you want to start it immediatelly*/);
		}

	}
}
