using UnityEngine;
using System.Collections;

public class UIButton : MonoBehaviour {

	private bool isDoublePressed = false;

	public void RestartGame(){
		GameObject.FindObjectOfType<UIEvents>().ReloadLevel();
	}

	public void LevelSelectGame(){
//		Debug.Log ("level select game");
		GameObject.FindObjectOfType<UIEvents>().LoadLevelsScene();
	}

	public void SoundGame(){
		GameObject.FindObjectOfType<UIEvents>().SoundOnOff();
	}

	public void MusicGame(){
		GameObject.FindObjectOfType<UIEvents>().MusicOnOff();
	}

	public void NextGame(){
		GameObject.FindObjectOfType<UIEvents>().LoadNextLevel();
	}

	public void BackToMain(){
		GameObject.FindObjectOfType<UIEvents>().LoadMainScene(gameObject);
	}

	public void BackToWorld(){
		GameObject.FindObjectOfType<UIEvents>().LoadWorldsSceneFromLevels();
	}

	public void PauseGame(){
		GameObject.FindObjectOfType<UIEvents>().PauseTheGame();
	}

	public void DoubleCoins(){
	//	Debug.Log ("isDoublePressed: " + isDoublePressed);
	//	if (!isDoublePressed) {
			isDoublePressed = true;
	//		Debug.Log ("double coins");
			GameObject.FindObjectOfType<GameManager> ().ShowRewardedAd ();
	//	}
	}

	public void RightButton(){
		GameObject.FindObjectOfType<ScrollRectSnap>().RightButton();
	}

	public void LeftButton(){
		GameObject.FindObjectOfType<ScrollRectSnap>().LeftButton();
	}
}
