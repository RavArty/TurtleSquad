using UnityEngine;
using System.Collections;

public class BlueImprAnim : MonoBehaviour {

	public GameObject ball;
	// Use this for initialization
	void UpdateArrow(){
		ball.GetComponent<BlueTouch>().SetImprBlue();
	}
}
