using UnityEngine;
using System.Collections;

public class StartIntroduction : MonoBehaviour {

	public static StartIntroduction instance;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public GameObject intro;
	// Use this for initialization
	void Start () {
		intro.SetActive(true);

	}
	public void StartIntro(){
		intro.SetActive(true);
	}
	
	public void StopIntro(){
		intro.SetActive(false);
	}
}
