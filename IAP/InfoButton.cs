using UnityEngine;
using System.Collections;

public class InfoButton : MonoBehaviour {

	public bool isPressedInfo = false;

	public void PressShopButton(){
		GameObject.FindObjectOfType<InfoPurchaseController> ().OpenShop();
		isPressedInfo = true;
	}
}
