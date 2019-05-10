using UnityEngine;
using System.Collections;
using GAF;

public class ControlLionsLayerOrder : MonoBehaviour {

	public static ControlLionsLayerOrder instance = null;
	int i = 0;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public void ChangeOrder(GameObject obj){
		i += 1;
		obj.GetComponent<GAF.Core.GAFAnimator>().gameObject.SetActive(false);
		obj.GetComponent<GAF.Core.GAFAnimator>().settings.spriteLayerValue += i;
		obj.GetComponent<GAF.Core.GAFAnimator>().gameObject.SetActive(true);
	}
}
