using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntroductionLevel : MonoBehaviour {

	public Camera 	MainCamera;
	public GameObject generalCanvas;		//to bring background closer
	public GameObject uiCanvas;
//	public GameObject turtle;
	public GameObject green;
	public GameObject enterGates;
	public GameObject Helps;
	public GameObject greenHealth;

	private Vector3 initPos;				//camera init pos
	private Vector3 lastPos;				//desired camera pos
	private bool moveBack = false;			//move camera back after anim
	private bool startImmediat = false;		//start level immediately
	private bool pressed = false;			//already pressed display - to end moving camera
	private bool reloaded;					//is level reladed
	private float Speed;					//speed for camera movement
	private Animator anim;
	private bool setTimeScale = true;
	private float xPos = 0.0f;
	private bool startHelp = false;
	private Button pause;
	private Button[] inAppButton;
	private Button 	 infoButton;
	private Button 	 menuButton;

	void Awake(){
		reloaded = GameObject.FindObjectOfType<KeepDataOnPlayMode> ().reloadedLevel;
		pause = GameObject.Find("MenuButton").GetComponent<Button>();

		inAppButton = GameObject.Find("InAppPanel").GetComponentsInChildren<Button>();
		infoButton = GameObject.Find("InfoPurchase").GetComponent<Button>();
		menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
//		for(int j = 0; j < inAppButton.Length; j++ ){
//			Debug.Log("button name: " + inAppButton[j].name);
//		}
		FadeObjectUnscaled.instance.FadeOut(greenHealth, 0);
		pause.interactable = false;

		for(int i = 0; i < inAppButton.Length; i++ ){
			inAppButton[i].interactable = false;
		}
		infoButton.interactable = false;
		menuButton.interactable = false;
	//	Debug.Log("reloaded: " + GameObject.FindObjectOfType<KeepDataOnPlayMode> ().reloadedLevel);
		StartCoroutine(Deactivate());

		if(!reloaded){
		//	GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeOut (green, 0.0f);
			generalCanvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
			uiCanvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;

			initPos = MainCamera.transform.position;
//			Debug.Log("green.x: " + green.transform.position.x);
			if(green.transform.position.x  < -6.5f){
//				Debug.Log("reached pos");
				xPos = -6.5f;
			}else if(green.transform.position.x  > 6.0f){
				xPos = 5.5f;
			}else{xPos = green.transform.position.x;}

			lastPos = new Vector3(xPos, green.transform.position.y - 1, green.transform.position.z - 10);
			Speed = Vector3.Distance (lastPos, initPos) / 1f;

			Time.timeScale = 0;
		//	Debug.Log("Timescale: " + Time.timeScale);
		}else{
		//	GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeOut (enterGates, 0.7f);
			pressed = true;
			pause.interactable = true;
			for(int i = 0; i < inAppButton.Length; i++ ){
				inAppButton[i].interactable = true;
			}
			infoButton.interactable = true;
			menuButton.interactable = true;
		
		}

	}

	void Start(){
		if(!reloaded){
			MainCamera.transform.position = lastPos;
			MainCamera.orthographicSize = 6.5f;
		
		//	StartCoroutine(EnableGreen());
			StartCoroutine(MoveBackExecute());
		//	StartCoroutine(StartHelps());

		}
	}
		

	IEnumerator EnableGreen(){
		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1.0f));
	//	GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeOut (enterGates, 0.5f);
	//	GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeIn (green, 0.5f);
	}

	IEnumerator MoveBackExecute(){
		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(2));
	//	enterGates.SetActive(false);
	
		StartCoroutine(MoveBack());
	}

	IEnumerator MoveBack(){
		

		for(float t = 0; t < 1; t+=Time.unscaledDeltaTime/0.5f){
			MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, initPos, t);
			MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 17.6f, t);
			yield return null;
		}
		startHelp = true;

		TimeScaleFunc();
	}

	IEnumerator ImmediateMoveBack(){
		
		for(float t = 0; t < 1; t+=Time.unscaledDeltaTime/0.5f){
			MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, initPos, t);
			MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 17.6f, t);
		//	enterGates.SetActive(false);
			yield return null;
		}
		TimeScaleFunc();
	}

	IEnumerator Deactivate(){
		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(5));
		MainCamera.GetComponent<StartIntroduction>().StopIntro();
	}
		

	void Update(){
	//	if(startHelp){
	//		startHelp = false;
	//		Debug.Log("update appear");
	//		ImmediateApear();
		//	StartCoroutine(StartHelps());
	//	}
		if (Input.GetMouseButtonDown(0) && !pressed){
			pressed = true;

		//	StopCoroutine(EnableGreen());
			StopCoroutine(MoveBackExecute());
			StopCoroutine(MoveBack());
			StartCoroutine(ImmediateMoveBack());
		//	startImmediat = true;
		//	moveBack = false;

		//	ImmediateApear();
		}
	}

/*
	void Update(){
		if(moveBack){
			MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, initPos, Speed * Time.unscaledDeltaTime);
			MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 17.6f, Speed * Time.unscaledDeltaTime);
			TimeScaleFunc();
		}
		if (Input.GetMouseButtonDown(0) && !pressed){
			pressed = true;
			StopCoroutine(EnableGreen());
			StopCoroutine(MoveBack());
			startImmediat = true;
			moveBack = false;

			ImmediateApear();
		}
		if(startImmediat){
			MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, initPos, Speed * Time.unscaledDeltaTime);
			MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 17.6f, Speed * Time.unscaledDeltaTime);
		//	if(Time.timeScale < 1){
		//		Time.timeScale = 1;
		//	}
		}

	}
	*/

	void TimeScaleFunc(){
		if(setTimeScale){
			setTimeScale = false;
		//	StopCoroutine(EnableGreen());
		//	StopCoroutine(MoveBackExecute());
		//	StopCoroutine(MoveBack());

			if(Helps == null){
				if(Time.timeScale < 1){
					Time.timeScale = 1;
					pause.interactable = true;
					for(int i = 0; i < inAppButton.Length; i++ ){
						inAppButton[i].interactable = true;
					}
					infoButton.interactable = true;
					menuButton.interactable = true;
					}
			}else{
				startHelp = true;
			//	Debug.Log("start help time");
				StartCoroutine(StartHelps());

			//	Helps.SetActive(true);
			}
		}
	}
	void ImmediateApear(){
		if(setTimeScale){
			setTimeScale = false;
//			GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeIn  (green, 0.0f);
//			GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeOut (turtle, 0.0f);
///			GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeOut (enterGates, 0.7f);
			if(Helps == null){
				if(Time.timeScale < 1){
			//		StartCoroutine(ImmediateApeearTimeScale());
					Time.timeScale = 1;
					pause.interactable = true;
					for(int i = 0; i < inAppButton.Length; i++ ){
						inAppButton[i].interactable = true;
					}
					infoButton.interactable = true;
					menuButton.interactable = true;
				}
			}else{
				Debug.Log("start help0");
				StartCoroutine(StartHelps());
			
			//	Helps.SetActive(true);
			}

		}
	}
	IEnumerator ImmediateApeearTimeScale(){
		yield return new WaitForSeconds(0.3f);
		Time.timeScale = 1;
	}

	IEnumerator StartHelps(){
	//	Debug.Log("start help func");
		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1));
	//	Debug.Log("start help");
		Helps.SetActive(true);
	//	Time.timeScale = 1;
	//	GameObject.Find("Time").GetComponent<Timer>().Stop();
	//	Debug.Log("name: " + GameObject.Find("Helps").transform.GetChild(0).gameObject.name);
	//	FadeObjectUnscaled.instance.FadeInImage(GameObject.Find("Helps").transform.GetChild(0).gameObject, 1.5f);
		pause.interactable = true;
		for(int i = 0; i < inAppButton.Length; i++ ){
			inAppButton[i].interactable = true;
		}
		infoButton.interactable = true;
		menuButton.interactable = true;
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
