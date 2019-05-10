using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

/// <summary>
/// Scroll Worlds
/// </summary>
public class ScrollRectSnap : MonoBehaviour {
	
	public RectTransform worldsPanel;
	public Button[] buttonWordls;
	public RectTransform centerToCompare;
	public float speed = 1.0f;
	public Vector2 startPos;
	public float direction;
	public bool directionChosen;
	public GameObject leftButton;
	public GameObject rightButton;
	
	private float[] distanceWorldToCenter;
	private bool dragging = false; 
	private int distanceBetweenWorlds; 
	private int closestWorldToCenter;
	private float LerpTime=1.0f;
	
	
	void Start(){
		int bttnLength = buttonWordls.Length;
		distanceWorldToCenter = new float[bttnLength]; 
		closestWorldToCenter = 0;
		distanceBetweenWorlds = (int)Mathf.Abs(buttonWordls[1].GetComponent<RectTransform>().anchoredPosition.x 
		                                       - buttonWordls[0].GetComponent<RectTransform>().anchoredPosition.x);
		
	}
	
	void Update(){
		if (Input.touchCount > 0) {
			var touch = Input.GetTouch(0);
			
			
			switch (touch.phase) {
				
			case TouchPhase.Began:
//				Debug.Log ("began ");
				for (int i = 0; i < buttonWordls.Length; i++){
					distanceWorldToCenter[i] = Mathf.Abs(centerToCompare.transform.position.x 
					                                     - buttonWordls[i].transform.position.x);
				}
				float minDistance = Mathf.Min(distanceWorldToCenter); 
				for (int a = 0; a < buttonWordls.Length; a++){
					if(minDistance == distanceWorldToCenter[a]){
						closestWorldToCenter = a;
					}
				}
				
				
				startPos = touch.position;
				directionChosen = false;
				break;
				
			case TouchPhase.Moved:
//				Debug.Log ("moved ");
				direction = touch.position.x - startPos.x;
				
				break;
				
			case TouchPhase.Ended:
				directionChosen = true;
				break;
				
			}
		}
		//disable left and right buttons on edge postions--------------------------
		if(closestWorldToCenter == 0){
			leftButton.GetComponent<Button> ().interactable = false;
			leftButton.gameObject.SetActive (false);
		}else{
			leftButton.GetComponent<Button> ().interactable = true;
			leftButton.gameObject.SetActive (true);
		}
		
		if(closestWorldToCenter == (buttonWordls.Length - 1)){
			rightButton.GetComponent<Button> ().interactable = false;
			rightButton.gameObject.SetActive (false);
		}else{
			rightButton.GetComponent<Button> ().interactable = true;
			rightButton.gameObject.SetActive (true);
		}
		//--------------------------------------------------------------------------
		if (directionChosen) {
			
//			Debug.Log ("direction " + direction);
			if(direction < -5){
				if(closestWorldToCenter != buttonWordls.Length - 1){
					closestWorldToCenter += 1;
				}
				direction = 0;
			}else if(direction > 5 ){
				if(closestWorldToCenter != 0){
					closestWorldToCenter -= 1;
				}
				
				direction = 0;
			}
			
			LerpToBttn(closestWorldToCenter * - distanceBetweenWorlds);
			
			
		}
	}
	
	public void LeftButton(){
		MusicSound.instance.ClickButtonSound ();
		LerpTime=0.0f;
		
		for (int i = 0; i < buttonWordls.Length; i++){
			distanceWorldToCenter[i] = Mathf.Abs(centerToCompare.transform.position.x - buttonWordls[i].transform.position.x);
		}
		float minDistance = Mathf.Min(distanceWorldToCenter); 
		for (int a = 0; a < buttonWordls.Length; a++){
			if(minDistance == distanceWorldToCenter[a]){
				closestWorldToCenter = a;
			}
		}
		
		
		if(closestWorldToCenter != 0){
			closestWorldToCenter -= 1;
			StartCoroutine(LerpByBttn(closestWorldToCenter * - distanceBetweenWorlds));
		}
		
	}
	
	public void RightButton(){
		MusicSound.instance.ClickButtonSound ();
		LerpTime=0.0f;
		
		for (int i = 0; i < buttonWordls.Length; i++){
			distanceWorldToCenter[i] = Mathf.Abs(centerToCompare.transform.position.x - buttonWordls[i].transform.position.x);
		}
		float minDistance = Mathf.Min(distanceWorldToCenter); 
		for (int a = 0; a < buttonWordls.Length; a++){
			
			if(minDistance == distanceWorldToCenter[a]){
				closestWorldToCenter = a;
			}
		}
		
		if(closestWorldToCenter != buttonWordls.Length - 1){
			
			closestWorldToCenter += 1;
			StartCoroutine(LerpByBttn(closestWorldToCenter * - distanceBetweenWorlds));
		}
		
	}
	
	IEnumerator LerpByBttn(int position){
		
		for(LerpTime = 0; LerpTime < 1; LerpTime+=Time.deltaTime*speed){
			float newX = Mathf.Lerp(worldsPanel.anchoredPosition.x, position, LerpTime);
			Vector2 newPosition = new Vector2(newX, worldsPanel.anchoredPosition.y); 
			worldsPanel.anchoredPosition = newPosition;
			yield return null;
			
		}
		
	}
	void LerpToBttn(int position){
		
		float newX = Mathf.Lerp(worldsPanel.anchoredPosition.x, position, Time.deltaTime * 5f);
		Vector2 newPosition = new Vector2(newX, worldsPanel.anchoredPosition.y); 
		worldsPanel.anchoredPosition = newPosition;
		
	}
	
	public void StartDrag(){
		dragging = true;
	}
	
	public void EndDrag(){
		dragging = false; 
	}
}
