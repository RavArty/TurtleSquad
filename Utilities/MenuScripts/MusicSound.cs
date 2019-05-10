using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class MusicSound : MonoBehaviour {
	
	public static MusicSound instance;
	
	[HideInInspector]
	public AudioSource[] audioSources;

	[HideInInspector]
	public bool isMusicMenu = true;
	[HideInInspector]
	public bool isMusicPlaying = true;
	[HideInInspector]
	public bool isAudidoPlaying = true;
	
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
		audioSources = GetComponents<AudioSource> ();
		
	}
	
	
	public void ClickButtonSound (){
		if(UIEvents.soundOff){
			Debug.Log ("soundoff = true");
			audioSources[0].Pause ();
		}else
			audioSources[0].Play ();
		//	AudioSource.PlayClipAtPoint (audioSources[0], Vector3.zero, MusicSound.instance.audioSources [1].volume);
		
	}
	public void StopMainMusic (){
		isMusicPlaying = false;
		if(isMusicMenu){
			audioSources[1].Pause ();
		}else{
			audioSources[3].Pause ();
		}
		
	}
	public void PlayMainMusic (){
		isMusicPlaying = true;

		if(isMusicMenu){
			audioSources[1].UnPause ();
		}else{
			audioSources[3].UnPause ();
		}
		
	}

	public void PlayMusicGame(){
		if(isMusicPlaying){
			audioSources[1].Stop ();
			audioSources[3].Play ();
		}
	}

	public void PlayMusicMenu(){
		if(isMusicPlaying){
			audioSources[1].Play ();
			audioSources[3].Stop ();
		}
	}
		
}
