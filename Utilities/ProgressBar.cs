using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

	private Image img;

	// Use this for initialization
	void Start () {
		img = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		img.fillAmount = GameObject.FindObjectOfType<UIEvents> ().progressBar;
	//	Debug.Log("fill: " + GameObject.FindObjectOfType<UIEvents> ().progressBar);
	}
}
