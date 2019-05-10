using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Change music/sound button image
/// </summary>
public class MusicSoundSettings : MonoBehaviour {

	public static MusicSoundSettings musicSoundSettings;

	public Sprite soundOff;
	public Sprite musicOff;
//	public Sprite soundOn;
//	public Sprite musicOn;
	public GameObject musicButton;
	public GameObject soundButton;

	
	void Awake(){
//		Debug.Log("start MusicSoundSettings");
		if(musicButton != null){
			if(!MusicSound.instance.isMusicPlaying){
			//if(!MusicSound.instance.audioSources[1].isPlaying){
				musicButton.GetComponent<Button>().image.overrideSprite = musicOff;	
			}
			if(!MusicSound.instance.isAudidoPlaying){
			//if(MusicSound.instance.audioSources[2].volume == 0.0f){
				soundButton.GetComponent<Button>().image.overrideSprite = soundOff;
				
			}
		}
	}

	public void MainMusicOn(){
		MusicSound.instance.PlayMainMusic();
		musicButton.GetComponent<Button>().image.overrideSprite = null;
	}

	public void MainMusicOff(){
		musicButton.GetComponent<Button>().image.overrideSprite = musicOff;
		MusicSound.instance.StopMainMusic();
	}

	public void SoundsOn(){
		MusicSound.instance.isAudidoPlaying = true;
		MusicSound.instance.audioSources [2].volume = 0.5f;
		MusicSound.instance.audioSources [2].volume = 0.5f;
	//	soundButton.GetComponent<Button>().image.sprite = soundOn;
		soundButton.GetComponent<Button>().image.overrideSprite = null;

	}

	public void SoundsOff(){
		MusicSound.instance.isAudidoPlaying = false;
	//	Debug.Log ("soundoff = true settings");
		MusicSound.instance.audioSources [2].volume = 0.0f;
		MusicSound.instance.audioSources [2].volume = 0.0f;
	//	soundButton.GetComponent<Button>().image.sprite = soundOff;
		soundButton.GetComponent<Button>().image.overrideSprite = soundOff;

	}
	
}
