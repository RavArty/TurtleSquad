// Destroy block after touching

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BlockBreak : MonoBehaviour {

	public GameObject	parent;
	public Transform	cannon;			// if there is an object on block
	public AudioClip	blockBreak;

	private bool grounded = true;
	private GameObject remains_obj;
	private AudioSource audioSources;

//---------------------------------------------------------------------------------------------------------------

	void Awake () {
		audioSources = GetComponent<AudioSource> ();

	}
//---------------------------------------------------------------------------------------------------------------
	public void Break(){


		if (cannon == null){
			grounded = false;
		}
		if (!grounded){
			parent.GetComponent<BlockBreakRemains>().DestroyBlock(gameObject);
		}
	}
//---------------------------------------------------------------------------------------------------------------


}	