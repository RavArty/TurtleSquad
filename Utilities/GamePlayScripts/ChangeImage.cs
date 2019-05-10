using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour {

	public Sprite back;

	public void ChangeButton(){
		GetComponent<Image>().sprite = back;
	}
}
