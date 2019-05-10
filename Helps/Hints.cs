using UnityEngine;
using System.Collections;

public class Hints : MonoBehaviour {

	public static Hints instance = null;
	[HideInInspector]
	public bool isHintActive = false;
	[HideInInspector]
	public bool isAbleToMove = true;
	[HideInInspector]
	public bool isPauseActive = false;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		isAbleToMove = true;
	//	DontDestroyOnLoad (gameObject);


	}
}
