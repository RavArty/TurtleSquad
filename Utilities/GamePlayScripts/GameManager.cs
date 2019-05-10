using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.Advertisements;

#pragma warning disable 0168 // variable declared but not used.

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
	[SerializeField] private LayerMask ShootLayer; // A mask determining what is ground to the character
	
	public static GameManager instance;
	/// <summary>
	/// The timer.
	/// </summary>
	public Timer timer;
	/// <summary>
	/// The timer countdown.
	/// </summary>
	public TimerCountdown timerCountdown;
	/// <summary>
	/// The coins label.
	/// </summary>
	private GameObject coinsLabel;
	/// <summary>
	/// Green ball.
	/// </summary>
	public GameObject green;
	
	/// <summary>
	/// Green ball.
	/// </summary>
	public GameObject[] gameObjects;

	/// <summary>
	/// The coin.
	/// </summary>
	public GameObject coin;
	/// <summary>
	/// Time period for three stars.
	/// </summary>
	public int  threeStarsTimePeriod;
	
	/// <summary>
	/// Time period for two stars.
	/// </summary>
	public int  twoStarsTimePeriod;
	
	/// <summary>
	/// The shape parent transform.
	/// </summary>
	public Transform shapeParent;
	
	
	/// <summary>
	/// The win dialog component. 
	/// </summary>
	public WinDialog winDialog;
	
	/// <summary>
	/// The window dialog.
	/// </summary>
	public GameObject WinDialogObj;

	/// <summary>
	/// The pause dialog object.
	/// </summary>
	public GameObject PauseDialogObj;
	
	
	/// <summary>
	/// The world title and level number texts.
	/// </summary>
	public Text worldTitle, levelNumber;
	
	/// <summary>
	/// Whether the game logic is running.
	/// </summary>
	public bool isRunning;
	
	/// <summary>
	/// The hint.
	/// </summary>
	private int hint = 0;
	
	
	/// <summary>
	/// The current shape.
	/// </summary>
	//		private Shape currentShape;
	
	
	/// <summary>
	/// The finish sound effect.
	/// </summary>
	public AudioClip finishSFX;
	
	/// <summary>
	/// The connected sound effect.
	/// </summary>
	public AudioClip connectedSFX;
	
	/// <summary>
	/// The level locked sound effect.
	/// </summary>
	public AudioClip levelLockedSFX;
	
	private int a = -1;
	private bool startPressObject = false;
	private int coins;
//	private GameObject[] coinsObj = new GameObject[20];
	private GameObject[] coinsObj;
	public DataManager.WorldData currentWorldData;
	public DataManager.LevelData currentLevelData;
//	public TotalData.Total currentTotalData;
	public string CurrentLevel;
	private int allPlayersCounter = 0;
	private bool instantiated = false;
	
	
	// Use this for initialization
	void Awake(){
		coinsLabel = GameObject.Find("CoinsScore");
		Application.targetFrameRate = 60;
		int layermask = ~(1 << 6);
//		Debug.Log("layermask: " + layermask);

		if(Time.timeScale < 1){
			Time.timeScale = 1;
		}
		currentWorldData = DataManager.FindWorldDataById (World.selectedWorld.ID, DataManager.filterdWorldsData);//Get the current world
		currentLevelData = currentWorldData.FindLevelDataById (TableLevel.selectedLevel.ID);///Get the current level
		CurrentLevel = "Level" + currentWorldData.ID +"."+ currentLevelData.ID;
	//	Debug.Log ("Level: " + CurrentLevel);
//		if(CurrentLevel == "Level1.1"){
		//	GameObject.FindObjectOfType<HintOne> ().Hint ();
		//	int bulletsBalance = StoreInventory.GetItemBalance("armor");
		//	Debug.Log("armor balance: " + bulletsBalance);	
//		}
//		if(CurrentLevel == "Level1.2"){
	//		GameObject.FindObjectOfType<HintTwo> ().Hint ();
//		}
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}
	
	
	void Start ()
	{
		if(green != null){
			green.transform.Find ("ActiveObject").gameObject.SetActive(true);
		}
	//	Debug.Log("level number: " + CurrentLevel);
		if(CurrentLevel != "Level1.6" && CurrentLevel != "Level1.25"){
	//		Debug.Log("started");
			Invoke("ArrangeCoins", 0.05f);
		}
	//	ArrangeCoins();
		//		Debug.Log ("Level: " + TableLevel.selectedLevel.ID);
		//	if(hint != 0){
		//		isRunning = false;
		//	}else{
		isRunning = true;
		//	}
		
		//				ResetGame ();
	}

//	void OnLevelWasLoaded(int level){
//		Debug.Log("OnLevelWasLoaded");
//	}

	public void ArrangeCoins(){
//		Debug.Log("ArrangeCoins");
		GameObject[] allCoins = GameObject.FindGameObjectsWithTag("CoinsTrigger");
		int j = 0;
		for(int i = 0; i < allCoins.Length; i++){
//			Debug.Log("occupied: " + allCoins[i].name+".."+allCoins[i].GetComponent<BlockOccupation>().occupied);
			if(!allCoins[i].GetComponent<BlockOccupation>().occupied){
//				coinsObj[j] = allCoins[i];
				j += 1;
			}
		}

		coinsObj = new GameObject[j];
		int k = 0;
		for(int i = 0; i < allCoins.Length; i++){
			if(!allCoins[i].GetComponent<BlockOccupation>().occupied){
				coinsObj[k] = allCoins[i];
				k += 1;
			}
		}
		ShuffleArray(coinsObj);
		int amount = (int)(0.3 * coinsObj.Length);
//		Debug.Log("amount"+amount);
		for(int i = 0; i < amount + 1; i++){
			GameObject placedCoin = ObjectPool.current.GetObject(coin);
			placedCoin.transform.position = new Vector2(coinsObj[i].transform.position.x, coinsObj[i].transform.position.y + 0.5f);
			placedCoin.transform.rotation = coinsObj[i].transform.rotation;
			placedCoin.SetActive(true);
		//	GameObject placedCoin = Instantiate(coin, new Vector2(coinsObj[i].transform.position.x, coinsObj[i].transform.position.y + 0.5f), coinsObj[i].transform.rotation) as GameObject;
		}

	}

	void ShuffleArray(GameObject[]  arr){
				for (int i = arr.Length - 1; i > 0; i--) {
			int r = UnityEngine.Random.Range(0, i);
					GameObject tmp = arr[i];
					arr[i] = arr[r];
					arr[r] = tmp;
				}
	}
	// Update is called once per frame
	void Update ()
	{
		if(green == null){
			StartCoroutine("ReloadGame");
		}
		if (!isRunning) {
			return;
		}
//		Profiler.BeginSample("GameManager gc collect update()");
//		if (Time.frameCount % 30 == 0)
//		{
//			System.GC.Collect();
//		}
//		Profiler.EndSample();

//		Profiler.BeginSample("GameManager raycast update()");
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero, Mathf.Infinity, ShootLayer);
		if (Input.touchCount > 0) {
			
			var touch = Input.GetTouch(0);
			
			// Handle finger movements based on touch phase.
			switch (touch.phase) {
				// Record initial touch position.
			case TouchPhase.Began:
				
				if(hit.collider != null){
					
					for(int i = 0; i < gameObjects.Length; i++){
						if(gameObjects[i] != null){
				//		Debug.Log("hit obj: " + hit.collider.gameObject.tag);
							if(hit.collider.gameObject.transform == gameObjects[i].transform.Find("ball")){
						//	if(hit.collider.gameObject.transform == gameObjects[i].transform){
								startPressObject = true;
							}
						}
						
					}
				}
				
				break;

				
				
				// Report that a direction has been chosen when the finger is lifted.
			case TouchPhase.Ended:
				if(hit.collider != null){
					for(int i = 0; i < gameObjects.Length; i++){
						if(gameObjects[i] != null){
							if((hit.collider.gameObject.transform == gameObjects[i].transform.Find("ball")) && startPressObject){
						//	if((hit.collider.gameObject.transform == gameObjects[i].transform) && startPressObject){
								a = i;
							}
						}
					}
				}
				break;
			}
		}
		if( a != -1){
			for(int i = 0; i < gameObjects.Length; i++){
				if(gameObjects[i] != null){
					gameObjects[i].transform.Find ("ActiveObject").gameObject.SetActive(false);
				}
			}
			gameObjects[a].transform.Find ("ActiveObject").gameObject.SetActive(true);
			a = -1;
		}
	//	Profiler.EndSample();
//--------------------------------------------------------------------------------------------------------------
/*		for(int i = 0; i < gameObjects.Length; i++){
			if(gameObjects[i] != null){
				if(gameObjects[i].transform.Find ("ActiveObject").gameObject.activeSelf == false){
					allPlayersCounter++;
				}
			}
		}
		Debug.Log("allPlayersCounter: " + allPlayersCounter);
		if(allPlayersCounter == gameObjects.Length + 1){
			if(green != null){
				green.transform.Find ("ActiveObject").gameObject.SetActive(true);
			}
		}
*/
//--------------------------------------------------------------------------------------------------------------			


	}
	
	IEnumerator ReloadGame()
	{	
		
		//	AsyncOperation async = Application.LoadLevelAsync (Application.loadedLevel);
		//	while (!async.isDone) {
		//		yield return 0;
		//		
		//	}
		
		
		//		Debug.Log ("destroyer34");
		// ... pause briefly
		yield return new WaitForSeconds(2);
		//		Debug.Log ("destroyer4");
		// ... and then reload the level.
		KeepDataOnPlayMode.instance.reloadedTimes += 1;
		if (KeepDataOnPlayMode.instance.reloadedTimes > 5) {
			KeepDataOnPlayMode.instance.reloadedTimes = 0;
		}
		if (KeepDataOnPlayMode.instance.randomAds == KeepDataOnPlayMode.instance.reloadedTimes) {
			KeepDataOnPlayMode.instance.reloadedTimes = 0;
			KeepDataOnPlayMode.instance.randomAds =  KeepDataOnPlayMode.instance.generateIntAds ();
			TotalData.LoadTotalFromFile ();
			if (!TotalData.totalData.noads) {
				ShowAd ();
			}
		}
		Application.LoadLevel(Application.loadedLevel);
	}
	
	/// <summary>
	/// Go to the next level.
	/// </summary>
	public void NextLevel ()
	{
		if (TableLevel.selectedLevel.ID >= 1 && TableLevel.selectedLevel.ID <= LevelsTable.tableLevels.Count) {
			///Get the next level and check if it's locked , then do not load the next level
			DataManager.WorldData currentWorldData = DataManager.FindWorldDataById (World.selectedWorld.ID, DataManager.filterdWorldsData);//Get the current world
			DataManager.LevelData currentLevelData = currentWorldData.FindLevelDataById (TableLevel.selectedLevel.ID);///Get the current level
			
			if (currentLevelData .ID + 1 < currentWorldData.levelsData.Count) {
				DataManager.LevelData nextLevelData = currentWorldData.FindLevelDataById (TableLevel.selectedLevel.ID + 1);///Get the next level
				if (nextLevelData.isLocked) {
					///Play lock sound effectd
//					Debug.Log ("locked");
					if (levelLockedSFX != null) {
//						AudioSource.PlayClipAtPoint (levelLockedSFX, Vector3.zero, GeneralCanvas.instance.audioSources [1].volume);
					}
					///Skip the next
					return;
				}
				string LoadNextLevel = "Level" + currentWorldData.ID +"."+ nextLevelData.ID;
				GameObject.FindObjectOfType<UIEvents> ().LoadNextLevelAfterChecking (LoadNextLevel);
			//	StartCoroutine(LoadNextScene(LoadNextLevel));
				//	Application.LoadLevel (LoadNextLevel);
				TableLevel.selectedLevel.ID = TableLevel.selectedLevel.ID + 1;
				
//				Debug.Log ("nextLevelData.name " + LoadNextLevel);
			}
			//Load levels scene
			if (currentLevelData .ID == LevelsTable.tableLevels.Count) {
				
				//	Application.LoadLevel (Scenes.levelsScene);
			}// else {
			//Load game scene
			//				Debug.Log ("nextLevelData.name " + currentWorldData.ID + currentLevelData.ID);
			//								TableLevel.selectedLevel = LevelsTable.tableLevels [TableLevel.selectedLevel.ID];///Set the selected level
			//				Debug.Log ("TableLevel.selectedLevel.name" + TableLevel.selectedLevel);
			//								Application.LoadLevel (Scenes.gameScene);
			//						}
		} else {
			///Play lock sound effectd
			if (levelLockedSFX != null) {
//				AudioSource.PlayClipAtPoint (levelLockedSFX, Vector3.zero, GeneralCanvas.instance.audioSources [1].volume);
			}
		}
	}
	IEnumerator LoadNextScene (string sceneName)
	{
		
		if (!string.IsNullOrEmpty (sceneName)) {
			
			AsyncOperation async = Application.LoadLevelAsync (sceneName);
			while (!async.isDone) {
				yield return 0;
				
			}
		}
	}
	void CoinsToInt(){
		string coinsLabelText = coinsLabel.GetComponent<Text>().text.Replace(": ", "");
		//		Debug.Log ("name: " +name);
		coins = int.Parse(coinsLabelText);
	}
	
	
	public void ShowPauseDialog(){

		PauseDialogObj.SetActive(true);

		if(!PauseDialogObj.GetComponent<PauseDialog>().isShow){
			PauseDialogObj.GetComponent<PauseDialog>().Show();
		}
	}

	public void HidePauseDialog(){

		PauseDialogObj.GetComponent<PauseDialog>().Hide();

	}
		
	public void ShowRewardedAd()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}

	private void HandleShowResult(ShowResult result)
	{
	//	Debug.Log ("show result");
		switch (result)
		{
		case ShowResult.Finished:
			TotalData.LoadTotalFromFile ();
	//		Debug.Log ("The ad was successfully shown.");
			int curValue = TotalData.totalData.totalCoins;
	//		Debug.Log ("total 0: " + curValue);
			TotalData.totalData.totalCoins += coins;
	//		Debug.Log ("total 1: " + TotalData.totalData.totalCoins);
		//	TotalData.totalData.totalCoins += currentLevelData.levelScore;
	//		KeepDataOnPlayMode.instance.reloadedTimes = 0;
			GameObject.Find ("x2Coins").SetActive (false);
			TotalData.SaveTotalToFile();
			GameObject.FindObjectOfType<WinDialog> ().fireCountTo (curValue, TotalData.totalData.totalCoins);

			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}

	public void ShowAd()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show();
		}
	}

	/// <summary>
	///On level complete.
	/// </summary>
	public void OnLevelComplete ()
	{
		if (instantiated) {
			return;
		}
		instantiated = true;
		KeepDataOnPlayMode.instance.reloadedTimes += 1;
		if (KeepDataOnPlayMode.instance.reloadedTimes > 5) {
			KeepDataOnPlayMode.instance.reloadedTimes = 0;
		}
		if (KeepDataOnPlayMode.instance.randomAds == KeepDataOnPlayMode.instance.reloadedTimes) {
			KeepDataOnPlayMode.instance.reloadedTimes = 0;
			KeepDataOnPlayMode.instance.randomAds =  KeepDataOnPlayMode.instance.generateIntAds ();
			TotalData.LoadTotalFromFile ();
			if (!TotalData.totalData.noads) {
				ShowAd ();
			}
		}
//		Debug.Log("on Level Complete");
		int timerType = 0;
		if(timer != null){
//			Debug.Log("timer");
		timer.Stop ();
			timerType = 1;
			CoinsToInt();
		}else{
//			Debug.Log("timer countdown");
			CoinsToInt();
			timerCountdown.Stop();
			timerType = 2;
		}
		isRunning = false;
		//	Time.timeScale = 0;
		try {
			///Save the stars level

			DataManager.WorldData currentWorldData = DataManager.FindWorldDataById (World.selectedWorld.ID, DataManager.filterdWorldsData);
			
			DataManager.LevelData currentLevelData = currentWorldData.FindLevelDataById (TableLevel.selectedLevel.ID);
			TotalData.LoadTotalFromFile();

//			Debug.Log ("currentWorldData: " + currentWorldData);
//			Debug.Log ("currentLevelData: " + currentLevelData.ID);

//			Debug.Log("timerType: " + timerType);
			if(timerType == 1){
//				Debug.Log("first");
			//			currentLevelData.leveltime = (int)timer.timeInSeconds;
	//			Debug.Log ("currentLevelData.leveltime " + currentLevelData.leveltime);
			int currentTime = currentLevelData.leveltime;
				currentLevelData.leveltime = timer.timeInSeconds;
				if(currentTime == 0){
	//				if(currentLevelData.ID == 1){
	//					TotalData.totalData.laser = 3;
	//					TotalData.totalData.green = 3;
	//					TotalData.totalData.blue = 3;
	//					TotalData.SaveTotalToFile();
				//		Debug.Log("done");
	//				}
		//			Debug.Log("zero");
			///		currentWorldData.Worldtime += timer.timeInSeconds;
				}
		//		currentWorldData.Worldtime += currentTime;
				TotalData.totalData.totalTime += timer.timeInSeconds;

		//		currentWorldData.Worldtime += currentLevelData.leveltime;
		//		currentTotalData.totalTime += currentWorldData.Worldtime;


				if(currentTime > timer.timeInSeconds){
		//			Debug.Log("less");
					currentLevelData.leveltime = timer.timeInSeconds;
					currentWorldData.Worldtime += timer.timeInSeconds;
					TotalData.totalData.totalTime -= (currentTime - timer.timeInSeconds);
		//			currentWorldData.Worldtime -= (currentTime - timer.timeInSeconds);
				}
				TotalData.SaveTotalToFile();
		//		Debug.Log("currentTime: " + currentTime);
		//		Debug.Log ("currentWorldData.Worldtime: " + currentWorldData.Worldtime);
				currentLevelData.levelScore = coins;
		//		currentWorldData.worldScore += coins;
			//	DataManager.SaveWorldsDataToFile (DataManager.);
				//TotalData.totalData.totalCoins = coins;

//				Debug.Log ("totaldata2: " + TotalData.totalData.totalCoins);
//				Debug.Log("currentTotalData.totalCoins: " + currentTotalData.totalCoins);
				//TotalData.SaveTotalToFile();
//				Debug.Log("gamemanager: " + currentTotalData.totalCoins);
			//			if (currentLevelData.starsLevel < TableLevel.StarsNumber.TWO){
			//				Debug.Log ("good thing");
			//			}
			//Calculate the stars rating
				if (timer.timeInSeconds >= 0 && timer.timeInSeconds <= threeStarsTimePeriod) {
					currentLevelData.starsLevel = TableLevel.StarsNumber.THREE;
					
				} else if (timer.timeInSeconds > threeStarsTimePeriod && timer.timeInSeconds <= twoStarsTimePeriod) {
					currentLevelData.starsLevel = TableLevel.StarsNumber.TWO;
				} else {
					currentLevelData.starsLevel = TableLevel.StarsNumber.ONE;
				}
			}else if(timerType == 2){
//				Debug.Log("second");
//				Debug.Log ("timerCountdown.timeInSeconds " + timerCountdown.timeInSeconds);
			//	int currentScore = currentLevelData.leveltime;
			//	currentWorldData.Worldtime += currentLevelData.leveltime;
			//	if(currentLevelData.leveltime > timer.timeInSeconds || currentLevelData.leveltime == 0){
			//		currentLevelData.leveltime = timer.timeInSeconds;
			//		currentWorldData.Worldtime -= timer.timeInSeconds;
			//	}
			//	currentLevelData.levelScore = coins;
				//			if (currentLevelData.starsLevel < TableLevel.StarsNumber.TWO){
				//				Debug.Log ("good thing");
				//			}
				//Calculate the stars rating
			//	Debug.Log ("test");
				currentLevelData.levelScore = coins;
				currentWorldData.worldScore += coins;
			//	TotalData.totalData.totalCoins += coins;
			//	TotalData.SaveTotalToFile();
				if (timerCountdown.timeInSeconds >= 0 && timerCountdown.timeInSeconds <= threeStarsTimePeriod) {
					currentLevelData.starsLevel = TableLevel.StarsNumber.THREE;
					
				} else if (timerCountdown.timeInSeconds > threeStarsTimePeriod && timerCountdown.timeInSeconds <= twoStarsTimePeriod) {
					currentLevelData.starsLevel = TableLevel.StarsNumber.TWO;
				} else {
					currentLevelData.starsLevel = TableLevel.StarsNumber.ONE;
				}

			}
//			Debug.Log ("test2");
//			Debug.Log ("levelsdata.count " + currentWorldData.levelsData.Count);
			if (currentLevelData.ID + 1 <= currentWorldData.levelsData.Count) {
				//				///Unlock the next level
//				Debug.Log ("Unlock next level");
				DataManager.LevelData nextLevelData = currentWorldData.FindLevelDataById (TableLevel.selectedLevel.ID + 1);
//				Debug.Log ("nextleveldata: " + nextLevelData);
				nextLevelData.isLocked = false;
				DataManager.SaveWorldsDataToFile (DataManager.filterdWorldsData);
			}
			
			if (currentLevelData .ID  == currentWorldData.levelsData.Count) {
//				Debug.Log ("Last level: " + currentLevelData .ID);
				DataManager.WorldData nextWorldData = DataManager.FindWorldDataById (World.selectedWorld.ID + 1 , DataManager.filterdWorldsData);
				nextWorldData.WorldIsLocked = false;
//				Debug.Log ("world " + nextWorldData.ID + nextWorldData.WorldIsLocked);
				
			}
//			Debug.Log ("test3");
			DataManager.SaveWorldsDataToFile (DataManager.filterdWorldsData);
			TotalData.totalData.totalCoins += coins;
//			Debug.Log ("currentWorldData.Worldtime: " + currentWorldData.Worldtime);
			TotalData.SaveTotalToFile();
			
			//	for (int ID = 0; ID < 5; ID ++){
			//	int ID = 2;
			//		Debug.Log("World_GameManger " + World.selectedWorld.ID + World.selectedWorld.WorldIsLocked);
			//	}
			BlackArea2.Show ();
			WinDialogObj.gameObject.SetActive(true);
			winDialog.starsNumber = currentLevelData.starsLevel;
			winDialog.Show ();
		} catch (Exception ex) {
			Debug.Log (ex.Message);
		}
//		Debug.Log ("You win...");
		Time.timeScale = 0;
		//AudioSource.PlayClipAtPoint(finishSFX,Vector3.zero);
		
	}

	public void ShowRewardedAd50Coins()
	{
		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult50Coins };
			Advertisement.Show("rewardedVideo", options);
		}
	}

	private void HandleShowResult50Coins(ShowResult result)
	{
			Debug.Log ("show result");
		switch (result)
		{
		case ShowResult.Finished:
			TotalData.LoadTotalFromFile ();
			//		Debug.Log ("The ad was successfully shown.");
			int curValue = TotalData.totalData.totalCoins;
			//		Debug.Log ("total 0: " + curValue);
			TotalData.totalData.totalCoins += 50;
			//		Debug.Log ("total 1: " + TotalData.totalData.totalCoins);
			//	TotalData.totalData.totalCoins += currentLevelData.levelScore;
			//		KeepDataOnPlayMode.instance.reloadedTimes = 0;
			//GameObject.Find ("x2Coins").SetActive (false);
			TotalData.SaveTotalToFile();
		//	GameObject.FindObjectOfType<PowerUps> ().updateCoins (curValue, TotalData.totalData.totalCoins);
			GameObject.FindObjectOfType<BuyCoins> ().updateCoinsAfterAppearing ();

			//	GameObject.FindObjectOfType<WinDialog> ().fireCountTo (curValue, TotalData.totalData.totalCoins);

			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
		
	
}