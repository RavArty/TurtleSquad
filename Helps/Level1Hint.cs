using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Level1Hint : MonoBehaviour {

	public GameObject pauseDialog;
	public GameObject ball;
	public GameObject secondHint;
	public GameObject treeBlock;
	public GameObject[] deactivateButtons;
	private bool pressed = true;
	private bool touched = false;		// if green touched stone block

	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("green") && !touched && !GameObject.FindObjectOfType<KeepDataOnPlayMode> ().reloadedLevel){				// pause game  when green collides with stone block (level1)
			if(treeBlock != null){
				ball.GetComponent<Rigidbody2D>().isKinematic = true;
				touched = true;
				secondHint.SetActive(true);
				Time.timeScale = 0;
			//	Hints.instance.isAbleToMove = false;
				Hints.instance.isHintActive = true;
				pressed = false;
				for(int i = 0; i < deactivateButtons.Length; i++){
					deactivateButtons[i].gameObject.GetComponent<Button>().interactable = false;
				}
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
					if(hit.collider.gameObject.CompareTag("ground_tree") && touched){
						ball.GetComponent<GreenTouch> ().axisMovement = 0;
						for(int i = 0; i < deactivateButtons.Length; i++){
							deactivateButtons[i].gameObject.GetComponent<Button>().interactable = true;
						}
						Time.timeScale = 1;									// start game after player destroyes tree block (level1)
					//	Hints.instance.isHintActive = false;
					}
				}
				break;
			}
		}
			
		if(secondHint.activeSelf){
			if(pauseDialog.GetComponent<PauseDialog>().isShow){
				secondHint.GetComponent<Animator>().enabled = false;
			}else{
				secondHint.GetComponent<Animator>().enabled = true;
			}
		}
		if(treeBlock == null){
			secondHint.GetComponent<Animator>().SetBool("Appear", false);
		//	StartCoroutine(AbleToMove());
			ball.GetComponent<Rigidbody2D>().isKinematic = false;
		}

	}

	IEnumerator AbleToMove(){
		yield return new WaitForSeconds(0.5f);
	//	Debug.Log("move");
	//		ball.GetComponent<Rigidbody2D>().isKinematic = false;
	//	Hints.instance.isAbleToMove = true;
	}
}
