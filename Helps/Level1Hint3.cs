using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level1Hint3 : MonoBehaviour {

	public GameObject pauseDialog;
	public GameObject thirdHint;
	public GameObject[] deactivateButtons;
	private bool pressed = true;			// already pressed display
	private bool touched = false;			// if green touched stone block

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("green") && !touched && !GameObject.FindObjectOfType<KeepDataOnPlayMode> ().reloadedLevel){
			touched = true;
			thirdHint.SetActive(true);
			Time.timeScale = 0;
			Hints.instance.isHintActive = true;
			pressed = false;
			for(int i = 0; i < deactivateButtons.Length; i++){
				deactivateButtons[i].gameObject.GetComponent<Button>().interactable = false;
			}
		}
	}
		

	void Update(){

		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

		if (Input.touchCount > 0) {

			var touch = Input.GetTouch(Input.touchCount - 1);

			switch (touch.phase) {

			case TouchPhase.Began:
				if(hit.collider != null){

					if(!hit.collider.gameObject.CompareTag("UIElementPause") && !pressed && !pauseDialog.GetComponent<PauseDialog>().isShow){
						pressed = true;
						for(int i = 0; i < deactivateButtons.Length; i++){
							deactivateButtons[i].gameObject.GetComponent<Button>().interactable = true;
						}
						thirdHint.GetComponent<Animator>().SetBool("Appear", false);
						Time.timeScale = 1;
						Hints.instance.isHintActive = false;

					}
				}else{
					if(!pressed && !pauseDialog.GetComponent<PauseDialog>().isShow ){
						pressed = true;
						for(int i = 0; i < deactivateButtons.Length; i++){
							deactivateButtons[i].gameObject.GetComponent<Button>().interactable = true;
						}
						thirdHint.GetComponent<Animator>().SetBool("Appear", false);
						Time.timeScale = 1;
						Hints.instance.isHintActive = false;
					}

				}
				break;
			}
		}
	}

}
