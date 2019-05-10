﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Helps3 : MonoBehaviour {

	public GameObject pauseDialog;
	public GameObject firstHint;
	public GameObject[] deactivateButtons;
	private bool pressed = false;			//already pressed display


	//	public bool isHintActive = true;

	void Awake(){
		Hints.instance.isHintActive = true;
		//	isHintActive = true;
	}

	void Start(){
		for(int i = 0; i < deactivateButtons.Length; i++){
			deactivateButtons[i].gameObject.GetComponent<Button>().interactable = false;
		}
	}

	void Update(){
		//		Debug.Log("isHintActive: " + isHintActive);
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

		if (Input.touchCount > 0) {

			var touch = Input.GetTouch(Input.touchCount - 1);

			switch (touch.phase) {

			case TouchPhase.Began:
				
				if(hit.collider != null){
					if(!hit.collider.gameObject.CompareTag("UIElementPause") && !pressed 
						&& !pauseDialog.GetComponent<PauseDialog>().isShow){
						pressed = true;
						for(int i = 0; i < deactivateButtons.Length; i++){
							deactivateButtons[i].gameObject.GetComponent<Button>().interactable = true;
						}
						firstHint.GetComponent<Animator>().SetBool("Appear", false);		// disappear first hint and start timer
						Time.timeScale = 1;
						Hints.instance.isHintActive = false;
					}
				}else{
					if(!pressed && !pauseDialog.GetComponent<PauseDialog>().isShow){
					pressed = true;
					for(int i = 0; i < deactivateButtons.Length; i++){
						deactivateButtons[i].gameObject.GetComponent<Button>().interactable = true;
					}
					firstHint.GetComponent<Animator>().SetBool("Appear", false);		// disappear first hint and start timer
					Time.timeScale = 1;
					Hints.instance.isHintActive = false;
				}

			}
				break;
			}
		}
	}
}
