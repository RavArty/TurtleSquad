using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonPosition : MonoBehaviour {

	public GameObject pos;

	// Use this for initialization
	void Start () {
		transform.position = pos.transform.position;
	}
	

}
