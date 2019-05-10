using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowArbalet : MonoBehaviour {

	public GameObject arm;
	public GameObject arm2;
	public GameObject slideShow;
	public GameObject blueCenter;
	public GameObject cannon;
	public GameObject infoButton;
	public bool isShowed = false;

	private bool isArmAppeared = false;
	private bool isArmAppeared2 = false;

	void Awake(){
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}
	void Start(){
		arm.transform.position = new Vector2(blueCenter.transform.position.x + 0.9f, blueCenter.transform.position.y - 2.0f);
		arm2.transform.position = new Vector2(infoButton.transform.position.x - 0.9f, infoButton.transform.position.y - 1.0f);
	
	}
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.CompareTag ("blue")) {
			if(!TotalData.totalData.secondLevelArm){
				TotalData.totalData.secondLevelArm = true;
				TotalData.SaveTotalToFile ();
				if (!slideShow.GetComponent<SlideShowAmount> ().isTookArrowSecondLevel) {
					if (TotalData.totalData.blue > 0) {
						arm.GetComponent<SpriteRenderer> ().enabled = true;
						arm.GetComponent<Animator> ().SetTrigger ("Arm");
						isArmAppeared = true;
					}
				}
			}
		}
	}

	void Update(){
	//	Debug.Log ("isArmAppeared: " + isArmAppeared + slideShow.GetComponent<SlideShowAmount> ().isTookArrowSecondLevel);
		if (isArmAppeared) {
			if (slideShow.GetComponent<SlideShowAmount> ().isTookArrowSecondLevel) {
				arm.GetComponent<Animator> ().enabled = false;
				FadeObject.instance.FadeOut (arm, 1.0f);
				ShowInfo ();

			}
			if (cannon == null) {
				arm.GetComponent<Animator> ().enabled = false;
				FadeObject.instance.FadeOut (arm, 1.0f);
			}
	//		isArmAppeared = false;
		}

		if (isArmAppeared2) {
			if (infoButton.GetComponent<InfoButton> ().isPressedInfo) {
				arm2.GetComponent<Animator> ().enabled = false;
				FadeObject.instance.FadeOut (arm2, 1.0f);
			}
		}
	}

	void ShowInfo(){
		if (!isShowed) {
			isShowed = true;
			arm2.GetComponent<SpriteRenderer> ().enabled = true;
			arm2.GetComponent<Animator> ().SetTrigger ("Arm");
			isArmAppeared2 = true;
		}
	}
	
}
