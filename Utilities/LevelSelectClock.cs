using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectClock : MonoBehaviour {

	public Transform ClockBar;
	public GameObject ClockHand;
	private float currentAmount = 0.0f;
	private bool startClock = false;
	private int sumTime = 0;
	[SerializeField] private float speed;


	void OnLevelWasLoaded(int level){
		if(level == 3){
			startClock = true;
			sumTime = GameObject.FindObjectOfType<LevelsTable> ().sumTime;
		//	Debug.Log("sumTime: " + sumTime);
		
		}
	}
	// Update is called once per frame
	void Update () {
		if(startClock){
		//	Debug.Log("startClock: " + startClock);
			if(currentAmount < sumTime){ 
				
				currentAmount += speed * Time.unscaledDeltaTime;
		//		Debug.Log("currentAmount: " + currentAmount);
			}
			ClockBar.GetComponent<Image>().fillAmount = currentAmount/1000;
		//	ClockHand.transform.rotation = Quaternion.Euler(0.0f, 0.0f,  currentAmount/1000);

			
		}
	}
}
