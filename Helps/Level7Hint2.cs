using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level7Hint2 : MonoBehaviour {

	public GameObject pauseDialog;
	public GameObject ball;
	public GameObject secondHint;
	public GameObject thirdHint;
	public GameObject[] deactivateButtons;
	private bool pressed = false;			//already pressed display


	void Awake(){
		Hints.instance.isHintActive = true;

	}

	void Start(){
		for(int i = 0; i < deactivateButtons.Length; i++){
			deactivateButtons[i].gameObject.GetComponent<Button>().interactable = false;
		}
	}

	void Update(){
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

		if (Input.touchCount > 0) {

			var touch = Input.GetTouch(Input.touchCount - 1);

			switch (touch.phase) {

			case TouchPhase.Began:
				if(hit.collider != null){
					if(!hit.collider.gameObject.CompareTag("UIElementPause") && !pressed
						&& !pauseDialog.GetComponent<PauseDialog>().isShow){

						ball.GetComponent<Rigidbody2D>().isKinematic = true;
						pressed = true;
						for(int i = 0; i < deactivateButtons.Length; i++){
							deactivateButtons[i].gameObject.GetComponent<Button>().interactable = true;
						}
						secondHint.GetComponent<Animator>().SetBool("Appear", false);		// disappear first hint and start timer
						Time.timeScale = 1;
						Hints.instance.isHintActive = false;
						thirdHint.SetActive(true);

					}
				}else{ 
					if(!pressed && !pauseDialog.GetComponent<PauseDialog>().isShow){
						
						pressed = true;
						for(int i = 0; i < deactivateButtons.Length; i++){
							deactivateButtons[i].gameObject.GetComponent<Button>().interactable = true;
						}
						secondHint.GetComponent<Animator>().SetBool("Appear", false);		// disappear first hint and start timer
						Time.timeScale = 1;
						Hints.instance.isHintActive = false;
						thirdHint.SetActive(true);
					}

				}
				break;
			}
		}
	}
}
