using UnityEngine;
using System.Collections;

public class CloseButtonInfo : MonoBehaviour {

	public void CloseInfo(){
		GameObject.FindObjectOfType<InfoPurchaseController> ().CloseShop();
	}
}
