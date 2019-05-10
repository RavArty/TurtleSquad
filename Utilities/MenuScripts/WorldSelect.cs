using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldSelect : MonoBehaviour {

//	public World world;
	private bool clicked = false;
	public AudioClip moveWall;

	public void AnimateWorld(World world){
		if(!clicked){
			clicked = true;
			MusicSound.instance.audioSources[2].clip = moveWall;
			MusicSound.instance.audioSources[2].Play();
		//	MusicSound.instance.ClickButtonSound ();
			GetComponent<Animator>().SetTrigger("Anim");
			StartCoroutine(WorldClick(world));
			Debug.Log("pressed: " + world);
		}
	}

	IEnumerator WorldClick(World world){
		yield return new WaitForSeconds(0.3f);

		GameObject.FindObjectOfType<UIEvents>().OnWorldClick(world);
	}
}
