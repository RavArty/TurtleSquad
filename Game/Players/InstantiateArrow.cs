using UnityEngine;
using System.Collections;

public class InstantiateArrow : MonoBehaviour {

	public GameObject blue;


	public void GetParentObj(GameObject parent){
		blue = parent;
	}
	public void CallInstantiateArrow(){
		blue.GetComponent<BlueTouch>().InstantiateArrow();
	}
}
