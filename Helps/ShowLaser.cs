using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLaser : MonoBehaviour {

	public GameObject arm;
	public GameObject slideShow;
	public GameObject laserCenter;


	private bool isArmAppeared = false;

	void Awake(){
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}
	void Start(){
		arm.transform.position = new Vector2(laserCenter.transform.position.x + 0.9f, laserCenter.transform.position.y - 2.0f);
		StartCoroutine (StartShowLaser ());
	}

	IEnumerator StartShowLaser(){
		yield return new WaitForSeconds (0.5f);

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
	//	Debug.Log ("isArmAppeared: " + isArmAppeared + slideShow.GetComponent<SlideShowAmount> ().isTookLaserFifthLevel);
		if (isArmAppeared) {
			if (slideShow.GetComponent<SlideShowAmount> ().isTookLaserFifthLevel) {
				arm.GetComponent<Animator> ().enabled = false;
				FadeObject.instance.FadeOut (arm, 1.0f);
			}
		//	isArmAppeared = false;
		}


	}

}
