using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Level2Hint2 : MonoBehaviour {

	public GameObject pauseDialog;
	public GameObject secondHint;
	public GameObject thirdHint;
	public GameObject blue;
	public GameObject[] deactivateButtons;
	private bool pressed = false;
	private bool touched = false;		// if green touched stone block

	private int 		screenSideLeft;
	private int 		screenSideRight;


	void Awake(){
		screenSideLeft = Screen.width/3;
		screenSideRight = (Screen.width/3)*2;

		Time.timeScale = 0;
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

				if  (touch.position.x > screenSideLeft && touch.position.x < screenSideRight 
					 && !pauseDialog.GetComponent<PauseDialog>().isShow){
					if(!pressed){
						pressed = false;
						if(hit.collider != null){
							if(hit.collider.gameObject.CompareTag("ground_tree") || hit.collider.gameObject.CompareTag("blue")
								|| hit.collider.gameObject.CompareTag("stone")|| hit.collider.gameObject.CompareTag("green") 
								|| hit.collider.gameObject.CompareTag("UIElement")){
								// do nothing
							}else{
								secondHint.GetComponent<Animator>().SetBool("Appear", false);
								Time.timeScale = 1;
								for(int i = 0; i < deactivateButtons.Length; i++){
									deactivateButtons[i].gameObject.GetComponent<Button>().interactable = true;
								}
								blue.GetComponent<BlueTouch>().Shoot();
								Hints.instance.isHintActive = false;
								Hints.instance.isAbleToMove = false;
								StartCoroutine(ActivateThird());
							}
						}else{
							secondHint.GetComponent<Animator>().SetBool("Appear", false);
							Time.timeScale = 1;
							for(int i = 0; i < deactivateButtons.Length; i++){
								deactivateButtons[i].gameObject.GetComponent<Button>().interactable = true;
							}
							blue.GetComponent<BlueTouch>().Shoot();
							Hints.instance.isHintActive = false;
							StartCoroutine(ActivateThird());
						}
					}
				}
				break;
			}
		}
				
		if(secondHint.activeSelf){
			if(pauseDialog.GetComponent<PauseDialog>().isShow){
				//	if(GameObject.FindObjectOfType<PauseDialog> ().isShow){
				secondHint.GetComponent<Animator>().enabled = false;
			}else{
				secondHint.GetComponent<Animator>().enabled = true;
			}
		}
		//		Debug.Log("treeBlock: " + treeBlock);
	}

	IEnumerator ActivateThird(){
		yield return new WaitForSeconds(1.0f);
		thirdHint.SetActive(true);
	}
}
