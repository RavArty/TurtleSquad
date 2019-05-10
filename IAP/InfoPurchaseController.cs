using UnityEngine;
using System.Collections;

public class InfoPurchaseController : MonoBehaviour {

	public Canvas infoCanvas;
	public GameObject green;
	public GameObject blue;
	public GameObject cannon;
	public GameObject greenButton;
	public GameObject blueButton;
	public GameObject cannonButton;
	public GameObject close;


	private int powerUpIndex;
	private int coinsIndex;
	private int blackAreaIndex;


	public void OpenShop(){
		if(KeepDataOnPlayMode.instance.isSoundOn){
			GetComponent<AudioSource> ().Play();
		}
		infoCanvas.enabled = true;
		infoCanvas.GetComponent<Animator>().SetTrigger("Appear");
		Time.timeScale = 0;
	}

	public void CloseShop(){
		infoCanvas.GetComponent<Animator>().SetTrigger("Disappear");
	}
	public void StartAfterClosingShop(){
		infoCanvas.enabled = false;
		Time.timeScale = 1;
	}

	public void GreenPanel(){
		//	Debug.Log("PowerUpPanel");
		green.transform.SetSiblingIndex(3);
		blue.transform.SetSiblingIndex(2);
		cannon.transform.SetSiblingIndex(1);
		greenButton.transform.SetSiblingIndex(4);
		blueButton.transform.SetSiblingIndex(5);
		cannonButton.transform.SetSiblingIndex(6);
		close.transform.SetSiblingIndex(7);

	}

	public void BluePanel(){
		//	Debug.Log("PowerUpPanel");
		green.transform.SetSiblingIndex(2);
		blue.transform.SetSiblingIndex(3);
		cannon.transform.SetSiblingIndex(1);
		greenButton.transform.SetSiblingIndex(4);
		blueButton.transform.SetSiblingIndex(5);
		cannonButton.transform.SetSiblingIndex(6);
		close.transform.SetSiblingIndex(7);

	}

	public void CannonPanel(){
		//	Debug.Log("PowerUpPanel");
		green.transform.SetSiblingIndex(2);
		blue.transform.SetSiblingIndex(1);
		cannon.transform.SetSiblingIndex(3);
		greenButton.transform.SetSiblingIndex(4);
		blueButton.transform.SetSiblingIndex(5);
		cannonButton.transform.SetSiblingIndex(6);
		close.transform.SetSiblingIndex(7);

	}

}
