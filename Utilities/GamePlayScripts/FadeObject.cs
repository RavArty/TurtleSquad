using UnityEngine;
using System.Collections;

public class FadeObject : MonoBehaviour {

	public static FadeObject instance;

	private  Transform[] fadeObj;
	private  float alphaValue;
//	private  float time;
	private float fadingOutSpeed;
	private Renderer[] rendererObjects;
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
	//	time = fadeTime;
	
		if(fadeTime != 0){
			fadingOutSpeed = 1.0f / fadeTime;
		}else{fadingOutSpeed = 0;}
		for(int i = 0; i < rendererObjects.Length; i++){
			StartCoroutine(FadeIn(rendererObjects[i], rendererObjects[i].GetComponent<Renderer>().material.color.a));
		}
	}

	public  void FadeOut(GameObject obj, float fadeTime){
		rendererObjects = obj.GetComponentsInChildren<Renderer>();
	//	time = fadeTime;
		if(fadeTime != 0){
			fadingOutSpeed = 1.0f / fadeTime;
		}else{fadingOutSpeed = 0;}
		for(int i = 0; i < rendererObjects.Length; i++){
	//		Debug.Log("rendererObjects" + i + ".." + rendererObjects[i]);
			StartCoroutine(FadeOut(rendererObjects[i], rendererObjects[i].GetComponent<Renderer>().material.color.a));
		}
	}

	IEnumerator FadeIn(Renderer obj, float alphaValue) { 

			while( alphaValue < 1.0f){
				if(fadingOutSpeed == 0){
					alphaValue = 1.0f;
				}else{
					alphaValue += Time.deltaTime * fadingOutSpeed;
				}
				if(obj != null){
					newColor = obj.GetComponent<Renderer>().material.color;
					newColor.a = Mathf.Max ( newColor.a, alphaValue ); 
				obj.GetComponent<Renderer>().sharedMaterial.color = newColor;
				//	obj.GetComponent<Renderer>().material.SetColor("_Color", newColor);
				}
				yield return null;
			}
		newColor.a = 1.0f; 
		if(obj != null)
		obj.GetComponent<Renderer>().sharedMaterial.color = newColor;
	//	obj.GetComponent<Renderer>().material.SetColor("_Color", newColor);
	}


	IEnumerator FadeOut(Renderer obj, float alphaValue) { 

		while( alphaValue > 0.0f){
			if(fadingOutSpeed == 0){
				alphaValue = 0.0f;
			}else{
				alphaValue -= Time.deltaTime * fadingOutSpeed;
			}
			if(obj != null){
				newColor = obj.GetComponent<Renderer>().material.color;
				newColor.a = Mathf.Min ( newColor.a, alphaValue ); 
				obj.GetComponent<Renderer>().sharedMaterial.color = newColor;
			//	obj.GetComponent<Renderer>().material.SetColor("_Color", newColor);
			}
			yield return null;
		}
		newColor.a = 0.0f; 
		if(obj != null)
		obj.GetComponent<Renderer>().sharedMaterial.color = newColor;
//		newColor.a = 0; 
//		if(obj != null){
//			obj.GetComponent<Renderer>().material.SetColor("_Color", newColor);
//		}
	//	Debug.Log("obj"  +obj.name+ ".." + obj.GetComponent<Renderer>().material.color.a);
	}
}
