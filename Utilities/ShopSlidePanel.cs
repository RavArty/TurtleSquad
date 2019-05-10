using UnityEngine;
using System.Collections;

public class ShopSlidePanel : MonoBehaviour {

	public GameObject panelMask;
	public Animator contentPanel;

	void Awake(){
		panelMask.GetComponent<CanvasGroup>().blocksRaycasts = true;

	}
	public void ToggleMenu() {
		bool isHidden = contentPanel.GetBool("isHidden");
		if(!isHidden){
			panelMask.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}else{
			panelMask.GetComponent<CanvasGroup>().blocksRaycasts = true;
		}
		contentPanel.SetBool("isHidden", !isHidden);
	//	GameObject.FindObjectOfType<GreenTouch> ().ZeroMovement ();
	}
}
