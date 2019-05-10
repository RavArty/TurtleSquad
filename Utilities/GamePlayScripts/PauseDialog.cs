using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseDialog : MonoBehaviour {
	
	
	/// <summary>
	/// Win dialog animator.
	/// </summary>
	public Animator pauseDialogAnimator;
	public Text levelNumber;
	public GameObject Helps;
	public Sprite soundoff;
	public Sprite musicoff;
	public GameObject music;
	public GameObject sound;
	public bool isShow = false;

	[HideInInspector]
	public DataManager.WorldData currentWorldData;
	[HideInInspector]
	public DataManager.LevelData currentLevelData;
	
	
	void Awake(){
		currentWorldData = DataManager.FindWorldDataById (World.selectedWorld.ID, DataManager.filterdWorldsData);//Get the current world
		currentLevelData = currentWorldData.FindLevelDataById (TableLevel.selectedLevel.ID);///Get the current level
		levelNumber.text = "Level: " + currentWorldData.ID +"-"+ currentLevelData.ID;
	}
	
	
	
	/// <summary>
	/// When the GameObject becomes visible
	/// </summary>
	void OnEnable ()
	{
		///Hide the Win Dialog
		Hide ();
	}
	
	/// <summary>
	/// Show the Win Dialog.
	/// </summary>
	public void Show ()
	{
		isShow = true;
		if(Time.timeScale > 0){
			Time.timeScale = 0;
		}
	//	Debug.Log("show pause: " + isShow + Time.timeScale);
	//	BlackArea2.Show ();
		pauseDialogAnimator.SetTrigger ("Running");
	}

	/// <summary>
	/// Hide the Win Dialog.
	/// </summary>
	public void Hide ()
	{
		isShow = false;
	//	Debug.Log("hint: " + Helps.GetComponent<Helps>().isHintActive);
		if(Helps != null && Hints.instance.isHintActive){
			// don't react
		}else{
		//	Debug.Log("start timer");
			Time.timeScale = 1;
		}
	//	Debug.Log("hide pause: " + isShow + Time.timeScale);
	//	BlackArea2.Hide();
		pauseDialogAnimator.SetBool ("Running", false);
		StartCoroutine(DeactivateObj());
		
		
	}

	IEnumerator DeactivateObj(){
		yield return new WaitForSeconds(0.3f);
		gameObject.SetActive(false);
				
	}


	
	
	
	
}
