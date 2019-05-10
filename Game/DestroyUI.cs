using UnityEngine;
using System.Collections;

public class DestroyUI : MonoBehaviour {

	public float awakerDestroyDelay;

	void Awake(){
		Destroy(gameObject, awakerDestroyDelay);
	}
}
