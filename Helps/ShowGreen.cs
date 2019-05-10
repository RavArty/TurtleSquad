using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGreen : MonoBehaviour {

	public GameObject arm;
	public GameObject slideShow;
	public GameObject greenCenter;
	public GameObject hint;


	private bool isArmAppeared = false;
	private bool showed = false;

	void Awake(){
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}
	void Start(){
		arm.transform.position = new Vector2(greenCenter.transform.position.x + 0.9f, greenCenter.transform.position.y - 2.0f);

		if(!TotalData.totalData.fifthLevelArm){
			TotalData.totalData.fifthLevelArm = true;
			TotalData.SaveTotalToFile ();
			//			if (!slideShow.GetComponent<SlideShowAmount> ().isTookArrowSecondLevel) {
			if (TotalData.totalData.laser > 0) {
				arm.GetComponent<SpriteRenderer> ().enabled = true;
				arm.GetComponent<Animator> ().SetTrigger ("Arm");
				isArmAppeared = true;
			}
			//			}
		}
	}

	void Update(){
		if (hint.GetComponent<Level2Hint3> ().isHint3Active) {
			ShowArm ();
		}
	//	Debug.Log ("isArmAppeared: " + isArmAppeared + slideShow.GetComponent<SlideShowAmount> ().isTookLaserFifthLevel);
		if (isArmAppeared) {
			if (slideShow.GetComponent<SlideShowAmount> ().isTookGreenSeventhLevel) {
				arm.GetComponent<Animator> ().enabled = false;
				FadeObject.instance.FadeOut (arm, 1.0f);
			}
			//	isArmAppeared = false;
		}
	}

	void ShowArm(){
		if (!showed) {
			showed = true;
			if (!TotalData.totalData.seventhLevelArm) {
				TotalData.totalData.seventhLevelArm = true;
				TotalData.SaveTotalToFile ();
				//			if (!slideShow.GetComponent<SlideShowAmount> ().isTookArrowSecondLevel) {
				if (TotalData.totalData.green > 0) {
					arm.GetComponent<SpriteRenderer> ().enabled = true;
					arm.GetComponent<Animator> ().SetTrigger ("Arm");
					isArmAppeared = true;
				}
				//			}
			}
		}
	}

}
