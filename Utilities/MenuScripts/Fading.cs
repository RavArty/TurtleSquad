using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class Fading : MonoBehaviour {

	public static Fading instance;

	public SpriteRenderer fadeOutTexture;
//	[HideInInspector]
	public float fadeSpeed = 1.0f;

	private int drawDepth = -1000;
	private float alpha = 1.0f;
	private int fadeDir = -1; 
	private bool sceneStarting = true;
	private float time = 0f;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	//	DontDestroyOnLoad (gameObject);
		fadeOutTexture.transform.localScale = new Vector3(Screen.width, Screen.height, 0);
	}

	void Start(){
		
		StartCoroutine(ClearScreen());
	}

/*	void StartScene ()
	{
		// Fade the texture to clear.
		FadeToClear();

		// If the texture is almost clear...
		if(fadeOutTexture.color.a <= 0.01f)
		{
			// ... set the colour to clear and disable the GUITexture.
			fadeOutTexture.color = Color.clear;
			fadeOutTexture.enabled = false;

			// The scene is no longer starting.
			sceneStarting = false;
		}
	}
*/
	void FadeToClear ()
	{
		
		fadeOutTexture.color = Color.Lerp(fadeOutTexture.color, Color.clear, Time.deltaTime*fadeSpeed);

	}

	public float BeginFade(int direction){
		fadeDir = direction;
		return (fadeSpeed);
	}

	void OnLevelWasLoaded() {
	//	BeginFade(-1);
	//	Debug.Log("loaded");
	//	StartCoroutine(ClearScreen());
	}

	IEnumerator ClearScreen(){

		time = 0.0f;
		yield return null;
		while (time <= 1.0f)
		{
			fadeOutTexture.color = Color.Lerp(fadeOutTexture.color, Color.clear, time);

			time += Time.unscaledDeltaTime * (1.0f / fadeSpeed);
			yield return null;
		}
		fadeOutTexture.color = Color.clear;
		fadeOutTexture.enabled = false;

//		for(float t = 0; t < 1; t+=Time.unscaledDeltaTime/fadeSpeed){
//
//			fadeOutTexture.color = Color.Lerp(fadeOutTexture.color, Color.clear, t);
//
//			yield return null;
//		}
//		fadeOutTexture.color = Color.clear;
//		fadeOutTexture.enabled = false;

	}
	public void FillScreen(string sceneName){
		StartCoroutine(FillScreenCoroutine(sceneName));	
	}

	IEnumerator FillScreenCoroutine(string sceneName){

		fadeOutTexture.enabled = true;
		time = 1.0f;
		yield return null;
		while (time >= 0.0f)
		{
			fadeOutTexture.color = Color.Lerp(fadeOutTexture.color, Color.black, time);

			time -= Time.unscaledDeltaTime * (1.0f / fadeSpeed);
			yield return null;
		}
		GameObject.FindObjectOfType<UIEvents> ().StartLoadSceneAsync (sceneName);
//		fadeOutTexture.color = Color.clear;
//		fadeOutTexture.enabled = false;

//		for(float t = 0; t < 1; t+=Time.unscaledDeltaTime/fadeSpeed){
//
//			fadeOutTexture.color = Color.Lerp(fadeOutTexture.color, Color.green, t);
//
//			yield return null;
//		}
	//	SceneManager.LoadSceneAsync(Scenes.worldsScene);
	//	fadeOutTexture.color = Color.clear;
	//	fadeOutTexture.enabled = false;

	}


	IEnumerator StartIntro(){
		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.4f));
		StartIntroduction.instance.StartIntro();
	}
	 
	IEnumerator SetInactive(){
		yield return new WaitForSeconds(fadeSpeed);
		gameObject.SetActive(false);
	}

	public static class CoroutineUtil
	{
		public static IEnumerator WaitForRealSeconds(float time)
		{
			float start = Time.realtimeSinceStartup;
			while (Time.realtimeSinceStartup < start + time)
			{
				yield return null;
			}
		}
	}
}

