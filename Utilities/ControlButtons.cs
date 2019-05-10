using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlButtons : MonoBehaviour {

	private Image leftButton;
	private Image rightButton;
	private Image centerButton;

	public Image leftButtonActive;
	public Image rightButtonActive;
	public Image centerButtonActive;
	// Use this for initialization
	void Awake () {
		leftButton = GameObject.Find("LeftButton").GetComponent<Image>();
		rightButton = GameObject.Find("RightButton").GetComponent<Image>();
		centerButton = GameObject.Find("TargetButton").GetComponent<Image>();
	}
	
	public void changeLeftButton(){
		//leftButton.sprite = 	
	}
}
