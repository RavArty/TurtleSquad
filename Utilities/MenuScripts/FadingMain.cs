using UnityEngine;
using System.Collections;

public class FadingMain : MonoBehaviour {

	public static FadingMain instance;
	public SpriteRenderer fadeOutTexture;

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

	}

	public void ClearScreen(){
		fadeOutTexture.color = Color.black;
		fadeOutTexture.transform.localScale = new Vector3(Screen.width, Screen.height, 0);
		StartCoroutine(ClearScreenRoutine());
	}

	IEnumerator ClearScreenRoutine(){

		time = 0.0f;
		yield return null;
		while (time <= 1.0f)
		{
			fadeOutTexture.color = Color.Lerp(fadeOutTexture.color, Color.clear, time);

			time += Time.unscaledDeltaTime * (1.0f / fadeSpeed);
			yield return null;
		}
		fadeOutTexture.color = Color.clear;
	//	fadeOutTexture.enabled = false;
	}
}
