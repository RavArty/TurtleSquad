using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelOverAnimationFallDown : MonoBehaviour {

		public Camera 	MainCamera;
		public GameObject generalCanvas;		//to bring background closer
		public GameObject uiCanvas;
		public GameObject green;
		public GameObject greenBall;
		public GameObject gates;

		private Vector3 initPos;				//camera init pos
		private Vector3 lastPos;				//desired camera pos
		private bool moveBack = false;			//move camera back after anim
		private bool startImmediat = false;		//start level immediately
		private bool pressed = false;			//already pressed display - to end moving camera
		private bool reloaded;					//is level reladed
		private float Speed;					//speed for camera movement
		private Animator anim;
		private bool moveCameraClose = false;
		private bool winDialog = true;
		private float yPos = 0.0f;
		private float xPos = 0.0f;



		void Awake(){
	
	/*
		GameObject[] obj = GameObject.FindGameObjectsWithTag ("coinsAnim");
		for(int i = 0; i < obj.Length; i++){
			if(obj[i] != null){
				Destroy(obj[i]);
			}
		}
	*/
		//	GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeOut (green, 0.5f);
		//		anim = turtle.GetComponent<Animator>();
		//		anim.SetTrigger("Idle");
		generalCanvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
		uiCanvas.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
		initPos = MainCamera.transform.position;
//		Debug.Log("pos: "+ greenBall.transform.position.x);
		if(greenBall.transform.position.x  >= 8.5f){
	//		//	Debug.Log("pos: "+ green.transform.position.y);
			xPos = 11.0f;
		}else if(greenBall.transform.position.x  < 8.5f && greenBall.transform.position.x  > -12f){
			xPos = green.transform.position.x;
		}else if(greenBall.transform.position.x  <= -12f){
			xPos = -11.0f;
		}
			
		yPos = greenBall.transform.position.y;
		lastPos = new Vector3(green.transform.position.x, green.transform.position.y + 1.5f, green.transform.position.z - 10);
		Speed = Vector3.Distance (lastPos, initPos) / 1f;

		transform.position = lastPos;
		//	transform.GetComponent<Camera>().orthographicSize = 6.5f;
		//		StartCoroutine(DisableGreen());

		Time.timeScale = 0.00001f;
		StartCoroutine(MoveTo());
		//	StartCoroutine(RollBack());
		//	StartCoroutine(DisableTurtle());
		//		StartCoroutine(EnableGreen());
		StartCoroutine(MoveBackExecute());
		moveCameraClose = true;


		}

		IEnumerator MoveTo(){
			yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.8f));
			gates.transform.position = new Vector2(greenBall.transform.position.x + 0.5f, greenBall.transform.position.y + 0.5f);
			gates.SetActive(true);
		//	FadeObjectUnscaled.instance.FadeIn(gates, 0.5f);
			for(float t = 0; t < 1; t+=Time.unscaledDeltaTime/0.5f){
			MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, new Vector3(xPos, yPos + 1.5f, greenBall.transform.position.z - 10), Speed * Time.unscaledDeltaTime);
				MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 6.5f, Speed * Time.unscaledDeltaTime);
				yield return null;
			}
			GameObject.Find("LeftButton").transform.GetChild(0).gameObject.SetActive(false);		// active arrow button
			GameObject.Find("LeftButton").transform.GetChild(1).gameObject.SetActive(true);			// inactive arrow button
			GameObject.Find("RightButton").transform.GetChild(0).gameObject.SetActive(false);		// active arrow button
			GameObject.Find("RightButton").transform.GetChild(1).gameObject.SetActive(true);			// inactive arrow button

		}

		IEnumerator RollBack(){
			//	Debug.Log("roll0");
			yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.4f));

			//	GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeIn (turtle, 0.2f);
			//	anim.SetTrigger("RollBack");
			//	GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeOut (green, 0.1f);
		}

		IEnumerator DisableTurtle(){
			yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.4f));
			//	Debug.Log("disable turtle");
			GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeOut (green, 0.2f);

		}

		IEnumerator MoveBackExecute(){
			yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(2.5f));
			StartCoroutine(MoveBack());
		}

		IEnumerator MoveBack(){


			for(float t = 0; t < 1; t+=Time.unscaledDeltaTime/0.5f){
				MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, initPos, t);
				MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 26.11004f, t);
				yield return null;
			}
			ExecuteWinDialog();

		}

		IEnumerator ImmediateMoveBack(){

			for(float t = 0; t < 1; t+=Time.unscaledDeltaTime/0.5f){
				MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, initPos, t);
				MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 26.11004f, t);
				yield return null;
			}
			ExecuteWinDialog();
			//	TimeScaleFunc();
		}

		//	IEnumerator MoveBack(){
		//		yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(3));
		//		moveCameraClose = false;
		//		moveBack = true;
		//		//	generalCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
		//		//	uiCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
		//

		//	}

		IEnumerator Deactivate(){
			yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1));
			//	yield return new WaitForSeconds(1 * Time.unscaledDeltaTime);
			gameObject.SetActive(false);



		}

		void Update(){
			if(moveCameraClose){

				//	MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, new Vector3(greenBall.transform.position.x, greenBall.transform.position.y + 1.5f, greenBall.transform.position.z - 10), Speed * Time.unscaledDeltaTime);
				//	MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 6.5f, Speed * Time.unscaledDeltaTime);

			}
			if(moveBack){
				//	MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, initPos, Speed * Time.unscaledDeltaTime);
				//	MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 17.6f, Speed * Time.unscaledDeltaTime);
				//	if(winDialog){
				//		winDialog = false;
				//		ExecuteWinDialog();
				//	}
			}
	/*		if (Input.GetMouseButtonDown(0) && !pressed){
				pressed = true;

				StopCoroutine(MoveTo());
				StopCoroutine(RollBack());
				StopCoroutine(DisableTurtle());
				//		StartCoroutine(EnableGreen());
				StopCoroutine(MoveBackExecute());
				//	startImmediat = true;
				//	moveCameraClose = false;
				//	moveBack = false;
				StartCoroutine(ImmediateMoveBack());
				//	Debug.Log("mouse down");
			}*/
			if(startImmediat){

				MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, initPos, Speed * Time.unscaledDeltaTime);
				MainCamera.orthographicSize = Mathf.Lerp(MainCamera.orthographicSize, 26.11004f, Speed * Time.unscaledDeltaTime);

				GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeIn (green, 0.5f);
				//	GameObject.FindObjectOfType<FadeObjectUnscaled> ().FadeOut (turtle, 0.0f);
				if(winDialog){
					winDialog = false;
					ExecuteWinDialog();
				}


			}

		}

		void ExecuteWinDialog(){
			GameObject.Find("MenuButton").GetComponent<Button>().interactable = false;
			//	Debug.Log("ExecuteWinDialog");
			StartCoroutine(EndLevel());
		}

		IEnumerator EndLevel(){
			//	Debug.Log("EndLevel");
			yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(0.5f));
			//	Debug.Log("EndLevel2");
			GameObject.FindObjectOfType<GameManager> ().OnLevelComplete ();

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
