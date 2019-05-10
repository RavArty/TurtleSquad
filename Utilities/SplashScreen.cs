using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Awake () {

		StartCoroutine ("LoadSceneAsync");
	}
	
	IEnumerator LoadSceneAsync ()
	{
		

		yield return new WaitForSeconds(2);

		AsyncOperation async = Application.LoadLevelAsync (Scenes.mainScene);
			

	}
}
