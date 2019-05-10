using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

/// <summary>
/// User interface events.
/// </summary>

[DisallowMultipleComponent]
public class UIEvents : MonoBehaviour
{
///	public GameObject main;
//	public GameObject world;
	public static bool soundOff = false;
	[HideInInspector]
	public float progressBar = 0;
	private AsyncOperation async;
	private GameObject worlds;
	private GameObject main;
	private bool isPausedGame = false;

	void  Awake() {
//		Debug.Log("keep: " + KeepDataOnPlayMode.instance.wordlScene);
		if(GameObject.Find("MainScene")){
			main = GameObject.Find("MainScene");
		}

		if(GameObject.Find("WorldScene")){
			worlds = GameObject.Find("WorldScene");
		}
		if(GameObject.Find("MainScene") && GameObject.Find("WorldScene")){
			if(KeepDataOnPlayMode.instance.wordlScene){
				FadingMain.instance.ClearScreen();
				worlds.SetActive(true);
				main.SetActive(false);
			}else{
				worlds.SetActive(false);
			}
		}

		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif

	}

	void Start(){
		Time.timeScale = 1;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	//	Debug.Log("isSoundOn: " + KeepDataOnPlayMode.instance.isSoundOn);
	}
/*	void OnLevelWasLoaded(int level){
		if (level == 100){
			Debug.Log("was loaded");
			if(GameObject.Find("Worlds")){
				worlds = GameObject.Find("Worlds");
				worlds.SetActive(false);
			}
			if(GameObject.Find("Main")){
				main = GameObject.Find("Main");
			}
		}
	}
*/

	
	
	public void ShowResetGameConfirmDialog ()
	{
		GameObject.Find ("ResetGameConfirmDialog").GetComponent<ConfirmDialog> ().Show ();
	}
	
	public void ShowExitConfirmDialog ()
	{
		GameObject.Find ("ExitConfirmDialog").GetComponent<ConfirmDialog> ().Show ();
	}
	
	
	public void ExitConfirmDialogEvent (GameObject value)
	{
		if (value.name.Equals ("YesButton")) {
			Debug.Log ("Exit Confirm Dialog : Yes button clicked");
			Application.Quit ();
		} else if (value.name.Equals ("NoButton")) {
			Debug.Log ("Exit Confirm Dialog : No button clicked");
		}
		GameObject.Find ("ExitConfirmDialog").GetComponent<ConfirmDialog> ().Hide ();
	}
	
	public void LoadMainScene (GameObject button)
	{
	//	FadingForMainMenu.instance.FillScreen("main");

		FadingMain.instance.ClearScreen();
	//	button.GetComponent<Button>().interactable = false;
		main.SetActive(true);
		worlds.SetActive(false);
		MusicSound.instance.ClickButtonSound ();
	//	StartCoroutine ("LoadSceneAsync", Scenes.mainScene);
	}
//	public void AfterLoadingMainScene(){
//		main.SetActive(true);
//		worlds.SetActive(false);
//	}
	
	/// <summary>
	/// On/Off  All sounds
	/// </summary>
	public void SoundOnOff ()
	{
		
		MusicSound.instance.ClickButtonSound ();
		if(soundOff){
			GameObject.FindObjectOfType<MusicSoundSettings> ().SoundsOn ();
			KeepDataOnPlayMode.instance.isSoundOn = true;
			soundOff = false;
		}else{
			GameObject.FindObjectOfType<MusicSoundSettings> ().SoundsOff ();
			KeepDataOnPlayMode.instance.isSoundOn = false;
			soundOff = true;
		}
		
	}
	
	/// <summary>
	/// On/Off Music.
	/// </summary>
	public void MusicOnOff ()
	{
		MusicSound.instance.ClickButtonSound ();
		
		if(MusicSound.instance.audioSources[1].isPlaying || MusicSound.instance.audioSources[3].isPlaying){
			GameObject.FindObjectOfType<MusicSoundSettings> ().MainMusicOff ();
			
		}else{
			GameObject.FindObjectOfType<MusicSoundSettings> ().MainMusicOn ();
			
		}
	}
	
	public void LoadSettingsScene ()
	{
		MusicSound.instance.ClickButtonSound ();
		StartCoroutine ("LoadSceneAsync", Scenes.settingsSecne);
	}

	public void LoadShopScene ()
	{
		MusicSound.instance.ClickButtonSound ();
		StartCoroutine ("LoadSceneAsync", Scenes.shop);
	}

	public void LoadBuyCoinsScene ()
	{
		MusicSound.instance.ClickButtonSound ();
		StartCoroutine ("LoadSceneAsync", Scenes.coins);
	}
	
	public void LoadCredits ()
	{
//		ShowAd();
		
		//	LoadingOverlay.loadOverlay();
		GameObject.FindObjectOfType<LoadingOverlay> ().loadOverlayTrue ();
		MusicSound.instance.ClickButtonSound ();
		StartCoroutine ("LoadSceneAsync", Scenes.credits);
	}
	
	public void LoadWorldsScene ()
	{
	//	if(GameObject.Find("Turtle")){
	//		GameObject.Find("Turtle").transform.GetChild(0).GetComponent<Animator>().Stop();
	//	}
		FadingMain.instance.ClearScreen();
	//	GameObject.Find("Main").SetActive(false);
	//	GameObject.Find("BackButton").GetComponent<Animator>().
		worlds.SetActive(true);
		main.SetActive(false);
	//	GameObject.FindObjectOfType<ChangeImage> ().ChangeButton ();
	//	GameObject.Find("BackButton").transform.GetChild(0).gameObject.GetComponent<Image>().overrideSprite = null;
	//	Debug.Log("backbutton: " + GameObject.Find("BackButton").transform.GetChild(0).gameObject.GetComponent<Image>().sprite.name);

	//	GameObject.Find("Worlds").SetActive(true);
	//	Fading.instance.FillScreen(Scenes.worldsScene);
		MusicSound.instance.ClickButtonSound ();
	//	StartCoroutine ("LoadSceneAsync", Scenes.worldsScene);
	}

	public void LoadWorldsSceneFromLevels(){
		KeepDataOnPlayMode.instance.wordlScene = true;
	//	FadingMain.instance.ClearScreen();
		MusicSound.instance.ClickButtonSound ();
		StartCoroutine ("LoadSceneAsync", Scenes.mainScene);
	//	LoadWorldsScene();
	}
//	public void AfterLoadingWorldsScene(){
//		main.SetActive(false);
//		worlds.SetActive(true);
//	}

	
	public void OnWorldClick (World world)
	{

	//	LoadingOverlay.loadOverlay();
	//	GameObject.FindObjectOfType<LoadingOverlay> ().loadOverlayTrue ();
	//	MusicSound.instance.ClickButtonSound ();
		World.selectedWorld = world;
	//	Fading.instance.FillScreen(Scenes.levelsScene);
		StartCoroutine ("LoadSceneAsync", Scenes.levelsScene);
	}
	
	public void LoadNextLevel ()
	{
		GameObject.FindObjectOfType<LoadingOverlay> ().loadOverlayTrue ();
		MusicSound.instance.ClickButtonSound ();
		GameObject.FindObjectOfType<WinDialog> ().StopAllCoroutines ();
		if (GameObject.FindGameObjectWithTag ("StarsEffect") != null) {
			GameObject.FindGameObjectWithTag ("StarsEffect").GetComponent<ParticleSystem> ().Pause ();
		}
		//check, if next level can be loaded
		KeepDataOnPlayMode.instance.reloadedLevel = false;
	//	KeepDataOnPlayMode.instance.reloadedTimes += 1;
	//	Debug.Log ("randomAds2: " + KeepDataOnPlayMode.instance.randomAds);
	//	Debug.Log ("reloadedTimes2: " + KeepDataOnPlayMode.instance.reloadedTimes);
	//	if (KeepDataOnPlayMode.instance.randomAds == KeepDataOnPlayMode.instance.reloadedTimes) {
	//		Debug.Log ("show ads next");
	//		KeepDataOnPlayMode.instance.reloadedTimes = 0;
	//		KeepDataOnPlayMode.instance.randomAds =  KeepDataOnPlayMode.instance.generateIntAds ();
	//		ShowAd ();
	//	}
		///after reaching reloaded levels 4-6 times, play ads without reward;
		/// and after playing put reloaded = 0;
		GameObject.FindObjectOfType<GameManager> ().NextLevel ();
	}

	public void LoadNextLevelAfterChecking(string scnenName){
		StartCoroutine ("LoadSceneAsync", scnenName);
	}



	
	public void ReloadLevel(){
		
		MusicSound.instance.ClickButtonSound ();
		if(Time.timeScale == 0){
			Time.timeScale = 1;
		}
		if(GameObject.FindGameObjectWithTag("StarsEffect") != null){
			GameObject.FindGameObjectWithTag("StarsEffect").GetComponent<ParticleSystem>().Pause();
		}
		//Camera.main.GetComponent<IntroductionLevel>().enabled = false;
//		GameObject.FindObjectOfType<KeepDataOnPlayMode> ().reloadedLevel = true;
		KeepDataOnPlayMode.instance.reloadedLevel = true;
		KeepDataOnPlayMode.instance.reloadedTimes += 1;
		if (KeepDataOnPlayMode.instance.reloadedTimes > 5) {
			KeepDataOnPlayMode.instance.reloadedTimes = 0;
		}
	//	Debug.Log ("randomAds: " + KeepDataOnPlayMode.instance.randomAds);
	//	Debug.Log ("reloadedTimes: " + KeepDataOnPlayMode.instance.reloadedTimes);
		if (KeepDataOnPlayMode.instance.randomAds == KeepDataOnPlayMode.instance.reloadedTimes) {
				KeepDataOnPlayMode.instance.reloadedTimes = 0;
				KeepDataOnPlayMode.instance.randomAds =  KeepDataOnPlayMode.instance.generateIntAds ();
			TotalData.LoadTotalFromFile ();
			if (!TotalData.totalData.noads) {
				ShowAd ();
			}
		}
//		Debug.Log("reloaded level: " + GameObject.FindObjectOfType<KeepDataOnPlayMode> ().reloadedLevel);
		Application.LoadLevel(Application.loadedLevel);


	}
	
	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}
	
	public void OnLevelClick (TableLevel level)
	{
	//	LoadingOverlay.loadOverlay();
	//	reloadedLevel = false;
	//	Debug.Log("onlevelclick: " + reloadedLevel);
	//	LoadingOverlay.loadOverlayTrue();
		MusicSound.instance.isMusicMenu = false;
		MusicSound.instance.PlayMusicGame();
		GameObject.FindObjectOfType<LoadingOverlay> ().loadOverlayTrue ();
		MusicSound.instance.ClickButtonSound ();
		TableLevel.selectedLevel = level;
		string LevelName = TableLevel.selectedLevel.name;
		
		//		GameObject.FindObjectOfType<BlackArea> ().Show ();

		StartCoroutine ("LoadSceneAsync", LevelName);
	}
	
	public void LoadLevelsScene ()
	{
		MusicSound.instance.isMusicMenu = true;
		MusicSound.instance.PlayMusicMenu();
//		Fading.instance.FillScreen(Scenes.worldsScene);
		///if reloaded times 3-6 times, play video
		/// and put reloaded var to 0;
		GameObject.FindObjectOfType<LoadingOverlay> ().loadOverlayTrue ();
	//	Debug.Log("levels scene;;;");
		MusicSound.instance.ClickButtonSound ();
		

//		if(Time.timeScale == 0){
//			Time.timeScale = 1;
//		}
		if(GameObject.FindGameObjectWithTag("StarsEffect") != null){
			GameObject.FindGameObjectWithTag("StarsEffect").GetComponent<ParticleSystem>().Pause();
		}
	//	GameObject.FindObjectOfType<KeepDataOnPlayMode> ().reloadedLevel = false;
	//	KeepDataOnPlayMode.instance.reloadedLevel = false;
	//	KeepDataOnPlayMode.instance.reloadedTimes += 1;
		StartCoroutine ("LoadSceneAsync", Scenes.levelsScene);
		
	}
	
	public void PauseTheGame ()
	{
		MusicSound.instance.ClickButtonSound ();
//		GameObject.FindObjectOfType<GameManager> ().PauseDialog ();

//		if(Time.timeScale>0){
		if(!isPausedGame){
			isPausedGame = true;
			GameObject.FindObjectOfType<GameManager> ().ShowPauseDialog ();

		}else{
			isPausedGame = false;
			GameObject.FindObjectOfType<GameManager> ().HidePauseDialog ();
		}
	}

	public void InfoPurchase ()
	{
		MusicSound.instance.ClickButtonSound ();
		//		GameObject.FindObjectOfType<GameManager> ().PauseDialog ();

		//		if(Time.timeScale>0){
		if(!isPausedGame){
			isPausedGame = true;
			GameObject.FindObjectOfType<GameManager> ().ShowPauseDialog ();

		}else{
			isPausedGame = false;
			GameObject.FindObjectOfType<GameManager> ().HidePauseDialog ();
		}
	}

	public void StartLoadSceneAsync(string scnenName){
		StartCoroutine ("LoadSceneAsync", scnenName);
	}

	IEnumerator LoadSceneAsync (string sceneName)
	{
//		if(Time.timeScale == 0){
//			Time.timeScale = 1;
//		}	
		if (!string.IsNullOrEmpty (sceneName)) {

			async = SceneManager.LoadSceneAsync(sceneName);
		//	async = Application.LoadLevelAsync (sceneName);
		//	Debug.Log("async: " + async );
			while (!async.isDone) {
				progressBar = async.progress;
				if(GameObject.Find("progressBar")){
					GameObject.Find("progressBar").GetComponent<Image>().fillAmount = progressBar;
				}
//				print ("%: " + async.progress);
				yield return 0;
				
			}
		}
	}
}
