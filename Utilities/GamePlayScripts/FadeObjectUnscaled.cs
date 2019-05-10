using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeObjectUnscaled : MonoBehaviour {

	public static FadeObjectUnscaled instance;

	private  Transform[] fadeObj;
//	private  float alphaValue;
	private  float time;
	private float fadingOutSpeed;
	private Renderer[] rendererObjects;
	private Image[] imageObjects;
	private Color newColor;

	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	//	DontDestroyOnLoad (gameObject);


	}

	public  void FadeIn(GameObject obj, float fadeTime){

		rendererObjects = obj.GetComponentsInChildren<Renderer>();
		time = fadeTime;

		if(fadeTime != 0){
			fadingOutSpeed = 1.0f / fadeTime;
		}else{fadingOutSpeed = 0;}
		for(int i = 0; i < rendererObjects.Length; i++){
			StartCoroutine(FadeIn(rendererObjects[i],  rendererObjects[i].GetComponent<Renderer>().material.color.a));
		}
	}

	public  void FadeInImage(GameObject obj, float fadeTime){
		
		imageObjects = obj.GetComponentsInChildren<Image>();
		time = fadeTime;

		if(fadeTime != 0){
			fadingOutSpeed = 1.0f / fadeTime;
		}else{fadingOutSpeed = 0;}
		for(int i = 0; i < imageObjects.Length; i++){
			StartCoroutine(FadeInImage(imageObjects[i],  imageObjects[i].GetComponent<Image>().material.color.a));
		}
	}

	public  void FadeOut(GameObject obj, float fadeTime){
		
		rendererObjects = obj.GetComponentsInChildren<Renderer>();
		time = fadeTime;
		if(fadeTime != 0){
			fadingOutSpeed = 1.0f / fadeTime;
		}else{fadingOutSpeed = 0;}
		for(int i = 0; i < rendererObjects.Length; i++){
			StartCoroutine(FadeOut(rendererObjects[i], rendererObjects[i].GetComponent<Renderer>().material.color.a));
		}
	}

	public  void FadeOutImage(GameObject obj, float fadeTime){

		imageObjects = obj.GetComponentsInChildren<Image>();
		time = fadeTime;
		if(fadeTime != 0){
			fadingOutSpeed = 1.0f / fadeTime;
		}else{fadingOutSpeed = 0;}
		for(int i = 0; i < imageObjects.Length; i++){
			StartCoroutine(FadeOutImage(imageObjects[i], imageObjects[i].GetComponent<Image>().material.color.a));
		}
	}

	IEnumerator FadeIn(Renderer obj, float alphaValue) { 
		while( alphaValue < 1.0f){
			if(fadingOutSpeed == 0){
				alphaValue = 1.0f;
			}else{
				alphaValue += Time.unscaledDeltaTime * fadingOutSpeed;
			}

			newColor = obj.GetComponent<Renderer>().material.color;
			newColor = obj.GetComponent<Renderer>().material.color;
			newColor.a = Mathf.Max ( newColor.a, alphaValue ); 
			obj.GetComponent<Renderer>().sharedMaterial.color = newColor;
			yield return null;
		}
		newColor.a = 1.0f; 
		if(obj != null)
		obj.GetComponent<Renderer>().sharedMaterial.color = newColor;

	}

	IEnumerator FadeInImage(Image obj, float alphaValue) { 
	//	Debug.Log("fadein: " + obj.name);
		while( alphaValue < 1.0f){
			if(fadingOutSpeed == 0){
				alphaValue = 1.0f;
			}else{
				alphaValue += Time.unscaledDeltaTime * fadingOutSpeed;
			}

			newColor = obj.GetComponent<Image>().material.color;
			newColor = obj.GetComponent<Renderer>().material.color;
			newColor.a = Mathf.Max ( newColor.a, alphaValue ); 
			obj.GetComponent<Image>().color = newColor;
			yield return null;
		}
		newColor.a = 1.0f; 
		if(obj != null)
			obj.GetComponent<Image>().color = newColor;

	}


	IEnumerator FadeOut(Renderer obj, float alphaValue) { 
		while( alphaValue > 0.0f){
			if(fadingOutSpeed == 0){
				alphaValue = 0.0f;
			}else{
				alphaValue -= Time.unscaledDeltaTime * fadingOutSpeed;
			}

			newColor = obj.GetComponent<Renderer>().material.color;
			newColor.a = Mathf.Min ( newColor.a, alphaValue ); 
			obj.GetComponent<Renderer>().sharedMaterial.color = newColor;
			yield return null;
		}
		newColor.a = 0.0f;
		if(obj != null)
		obj.GetComponent<Renderer>().sharedMaterial.color = newColor;

	}

	IEnumerator FadeOutImage(Image obj, float alphaValue) { 
	//	Debug.Log("fadeout: " + obj.name);
		while( alphaValue > 0.0f){
			if(fadingOutSpeed == 0){
				alphaValue = 0.0f;
			}else{
				alphaValue -= Time.unscaledDeltaTime * fadingOutSpeed;
			}

			newColor = obj.GetComponent<Image>().material.color;
			newColor.a = Mathf.Min ( newColor.a, alphaValue ); 
			obj.GetComponent<Image>().color = newColor;
			yield return null;
		}
		newColor.a = 0.0f;
		if(obj != null)
			obj.GetComponent<Image>().color = newColor;

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
