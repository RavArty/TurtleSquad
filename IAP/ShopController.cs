using UnityEngine;
using System.Collections;

public class ShopController : MonoBehaviour {

	public Canvas shopCanvas;
	public GameObject powerUpPanel;
	public GameObject coinsPanel;
	public GameObject coinsPanelScrollView;
	public GameObject powerUpsButton;
	public GameObject coinsButton;
	public GameObject close;


	private int powerUpIndex;
	private int coinsIndex;
	private int blackAreaIndex;


	public void OpenShop(){
		if(KeepDataOnPlayMode.instance.isSoundOn){
			GetComponent<AudioSource> ().Play();
		}
		shopCanvas.enabled = true;
		shopCanvas.GetComponent<Animator>().SetTrigger("Appear");
		Time.timeScale = 0;
	}

	public void DisableAnimation(){
		shopCanvas.GetComponent<Animator> ().enabled = false;
	}
	public void CloseShop(){
		shopCanvas.GetComponent<Animator> ().enabled = true;
		shopCanvas.GetComponent<Animator>().SetTrigger("Disappear");
		coinsPanelScrollView.SetActive (false);
		powerUpPanel.transform.SetSiblingIndex(2);
		coinsPanel.transform.SetSiblingIndex(1);
		powerUpsButton.transform.SetSiblingIndex(3);
		coinsButton.transform.SetSiblingIndex(4);
		close.transform.SetSiblingIndex(5);
	}


	public void StartAfterClosingShop(){
		shopCanvas.enabled = false;
		Time.timeScale = 1;
	}

	public void PowerUpPanel(){
//		Debug.Log("PowerUpPanel");
		powerUpPanel.transform.SetSiblingIndex(2);
		coinsPanel.transform.SetSiblingIndex(1);
		powerUpsButton.transform.SetSiblingIndex(3);
		coinsButton.transform.SetSiblingIndex(4);
		close.transform.SetSiblingIndex(5);
		powerUpPanel.GetComponent<PowerUps> ().updateCoinsAfterAppearing ();
	
	}

	public void CoinsPanel(){
//		Debug.Log("CoinsPanel");
		powerUpPanel.transform.SetSiblingIndex(1);
		coinsPanel.transform.SetSiblingIndex(2);
		powerUpsButton.transform.SetSiblingIndex(3);
		coinsButton.transform.SetSiblingIndex(4);
		close.transform.SetSiblingIndex(5);
		coinsPanelScrollView.SetActive (true);
		coinsPanel.GetComponent<BuyCoins> ().updateCoinsAfterAppearing ();
	
	}

}
