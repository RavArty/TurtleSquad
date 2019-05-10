using UnityEngine;
using System.Collections;

public class ActivateObj : MonoBehaviour {
	public GameObject obj;

	public void Activate(){
		obj.SetActive(true);
		FadeObject.instance.FadeIn(obj, 0.2f);
	}

}
