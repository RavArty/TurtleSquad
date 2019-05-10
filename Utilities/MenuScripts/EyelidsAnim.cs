using UnityEngine;
using System.Collections;

public class EyelidsAnim : MonoBehaviour {

	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = gameObject.GetComponent<Animator>();
		InvokeRepeating("LaunchEyeLids", 2, 4.0F);
	}
	
	void LaunchEyeLids(){
		anim.SetTrigger("Start");
	}
}
