using UnityEngine;
using System.Collections;

public class ShopButton : MonoBehaviour {

	public void PressShopButton(){
		GameObject.FindObjectOfType<ShopController> ().OpenShop();
	}

	public void PressCloseShopButton(){
		GameObject.FindObjectOfType<ShopController> ().CloseShop();
	}

}
