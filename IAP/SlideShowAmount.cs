using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlideShowAmount : MonoBehaviour {

	private GameObject panel;
	private GameObject OffButton;
	private Text laserLabel;
	private Text blueArrowLabel;
	private Text greenArmorLabel;

	private uint bombBalance;
	private uint bluArrowBalance;
	private uint greenArmorBalance;

	private bool takeArrow = true;
	private bool takeGreen = true;
	private bool cannonLaser = true;

	public bool isTookArrowSecondLevel = false;
	public bool isTookLaserFifthLevel = false;
	public bool isTookGreenSeventhLevel = false;

	void Awake(){
		OffButton = GameObject.Find("OffLaser");
		laserLabel = GameObject.Find ("laserLabel").GetComponent<Text>();
		blueArrowLabel = GameObject.Find ("blueLabel").GetComponent<Text>();
		greenArmorLabel = GameObject.Find ("greenLabel").GetComponent<Text>();

		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif


	}
	void Start () {
		panel = GameObject.Find("InAppPanel");
//		Debug.Log ("laser: " + TotalData.totalData.laser);
//		Debug.Log ("data: " + TotalData.totalData.totalCoins);
		laserLabel.text = TotalData.totalData.laser.ToString();
		greenArmorLabel.text = TotalData.totalData.green.ToString();
		blueArrowLabel.text = TotalData.totalData.blue.ToString();
//		FadeObjectUnscaled.instance.FadeOut(OffButton, 0);

//		bombBalance = 0;//(uint)StoreInventory.GetItemBalance("bomb");
//		bombLabel.text = bombBalance.ToString();

//		bluArrowBalance = 0;//(uint)StoreInventory.GetItemBalance("arrow");
//		blueArrowLabel.text = bluArrowBalance.ToString();

//		greenArmorBalance = 0;//(uint)StoreInventory.GetItemBalance("armor");
//		greenArmorLabel.text = greenArmorBalance.ToString();

	 
	}
	public void updatePurchases(){
		laserLabel.text = TotalData.totalData.laser.ToString();
		greenArmorLabel.text = TotalData.totalData.green.ToString();
		blueArrowLabel.text = TotalData.totalData.blue.ToString();
	}

	public void TakeArrow(){
		if(takeArrow){
			if (TotalData.totalData.blue > 0) {
				if (KeepDataOnPlayMode.instance.isSoundOn) {
					GetComponent<AudioSource> ().Play ();
				}
				isTookArrowSecondLevel = true;
				takeArrow = false;
				//	StoreInventory.TakeItem("bomb", 1);
				//	int balance = 1;
				//	bombBalance--;
				TotalData.totalData.blue -= 1;
				blueArrowLabel.text = TotalData.totalData.blue.ToString ();
				TotalData.SaveTotalToFile ();
				GameObject[] blue = GameObject.FindGameObjectsWithTag ("blue");
				for (int j = 0; j < blue.Length; j++) {
					blue [j].GetComponent<BlueTouch> ().SetImprovement (1);
				}
			}
		}
	//	bombLabel.text = bombBalance.ToString();

	}

//	public void TakeBlue(){
	//	StoreInventory.TakeItem("arrow", 1);
	//	bluArrowBalance--;
	//	blueArrowLabel.text = bluArrowBalance.ToString();
//		GameObject.FindObjectOfType<BlueTouch> ().BlueArrow ();
//		GameObject[] blue = GameObject.FindGameObjectsWithTag("blue");
//		for(int j = 0; j < blue.Length; j++){
//			blue[j].GetComponent<BlueTouch>().SetImprovement (1);
//		}
//	}

	public void TakeGreen(){
		if (TotalData.totalData.green > 0) {
		if(KeepDataOnPlayMode.instance.isSoundOn){
			GetComponent<AudioSource> ().Play();
		}
	//	StoreInventory.TakeItem("armor", 1);
	//	greenArmorBalance--;
	//	greenArmorLabel.text = greenArmorBalance.ToString();
	//	if(takeGreen){
	//		takeGreen = false;
			isTookGreenSeventhLevel = true;
			TotalData.totalData.green -= 1;
			greenArmorLabel.text = TotalData.totalData.green.ToString();
			TotalData.SaveTotalToFile();
			GreenTouch.instance.SetImprovement(1);
		//	GameObject.FindObjectOfType<GreenTouch> ().SetImprovement (1);
	//	}
	//	if(GreenTouch.instance.currentImpr == 1 && GreenTouch.instance.currentImpr)
		}
	}

	public void CannonLaser(){
		//	StoreInventory.TakeItem("armor", 1);
		//	greenArmorBalance--;
		//	greenArmorLabel.text = greenArmorBalance.ToString();
		if (cannonLaser) {
			if (TotalData.totalData.laser > 0) {
				if (KeepDataOnPlayMode.instance.isSoundOn) {
					GetComponent<AudioSource> ().Play ();
				}
				isTookLaserFifthLevel = true;
				panel.GetComponent<Animator> ().SetTrigger ("Laser");
				//	StartCoroutine(DeactivateAnimator());
				cannonLaser = false;
				//	OffButton.SetActive(true);
				TotalData.totalData.laser -= 1;
				laserLabel.text = TotalData.totalData.laser.ToString ();
				TotalData.SaveTotalToFile ();
				GameObject[] cannons = GameObject.FindGameObjectsWithTag ("cannon");
				for (int j = 0; j < cannons.Length; j++) {
					//	Debug.Log(cannons[j].name);
					//}
					if (cannons [j].GetComponent<CannonCollision> ()) {
						for (int i = 0; i < cannons [j].GetComponent<CannonCollision> ().laser.Length; i++) {
							cannons [j].GetComponent<CannonCollision> ().laser [i].SetActive (true);
							cannons [j].GetComponent<CannonCollision> ().laser [i].GetComponent<Animator> ().SetBool ("Active", true);
						}
					}
				}
			}
		}
	}
	IEnumerator DeactivateAnimator(){
		yield return new WaitForSeconds(0.5f);
		panel.GetComponent<Animator>().gameObject.SetActive(false);
	}

	public IEnumerator ActivePanel ()
	{
		
	//	panelMask.GetComponent<CanvasGroup>().blocksRaycasts = true;

		
		yield return 0;
		
		
		
	}


}
