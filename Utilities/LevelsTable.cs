using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.

[DisallowMultipleComponent]
public class LevelsTable : MonoBehaviour
{
	/// <summary>
	/// The table levels.
	/// </summary>
	public static List<TableLevel> tableLevels;
	
	/// <summary>
	/// The levels parent.
	/// </summary>
	public Transform levelsParent;
	
	/// <summary>
	/// The star on sprite.
	/// </summary>
	public Sprite starOn;
	
	/// <summary>
	/// The star off sprite.
	/// </summary>
	public Sprite starOff;
	
	/// <summary>
	/// The level prefab.
	/// </summary>
	public GameObject levelPrefab;
	
	/// <summary>
	/// The empty world text.
	/// </summary>
	public GameObject emptyWorldText;

	public Text time;
	public Text coins;

	
	
	/// <summary>
	/// temporary transform.
	/// </summary>
	private Transform tempTransform;
	
	/// <summary>
	/// temporary mission data.
	/// </summary>
	private DataManager.WorldData tempWorldData;
	
	/// <summary>
	/// temporary level data.
	/// </summary>
	private DataManager.LevelData tempLevelData;
	/// <summary>
	/// The current total data.
	/// </summary>
	public TotalData.Total currentTotalData;
	public int sumTime = 0;
	private int sumCoins = 0;
	
	void Awake ()
	{
		///define table levels
		tableLevels = new List<TableLevel> ();

		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}
	
	/// <summary>
	/// When the GameObject becomes visible
	/// </summary>
	public void OnEnable ()
	{
		try {
			///Create new levels
			CreateLevels ();
			if (!string.IsNullOrEmpty (World.selectedWorld.title)) {
				GameObject title = GameObject.Find ("Title");
				if (title != null) {
					title.GetComponent<Text> ().text = World.selectedWorld.title;
				}
			}
		} catch (Exception ex) {
			Debug.Log ("Make sure you selected a world");
		}
		
		///Setting up the top title
		
	}
	
	
	/// <summary>
	/// Creates the levels.
	/// </summary>
	private void CreateLevels ()
	{
		///Clear current tableLevels list
		tableLevels.Clear ();
		//Get Shapes from the wanted (selected) World
		String [] shapes = World.selectedWorld.levels;
		
		if (shapes.Length == 0) {
			if (emptyWorldText != null)
				emptyWorldText.SetActive (true);
			Debug.Log ("There are no Levels in this World");
			return;
		} else {
			if (emptyWorldText != null)
				emptyWorldText.SetActive (false);
		}
		
		TableLevel tableLevelComponent = null;
		GameObject tableLevelGameObject = null;
		
		///The ID of the level
		int ID = 0;


		///Create Levels
		for (int i = 0; i <shapes.Length; i++) {
			
			ID = (i + 1);//the id of the level
			
			tableLevelGameObject = Instantiate (levelPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			tableLevelGameObject.transform.SetParent (levelsParent);//setting up level parent
			tableLevelComponent = tableLevelGameObject.GetComponent<TableLevel> ();//get TableLevel Component
			tableLevelComponent.ID = ID;//setting up level ID
			tableLevelGameObject.name = "Level" + World.selectedWorld.ID+ "." + ID;//level name
			tableLevelGameObject.transform.localScale = Vector3.one;
//			Debug.Log ("ID " + ID);
			SettingUpLevel (tableLevelComponent, ID);//setting up the level contents (stars number ,islocked,...)
			tableLevels.Add (tableLevelComponent);//add table level component to the list
		}
		
//		Debug.Log ("New levels have beeen created");
	}
	
	/// <summary>
	/// Settings up the level contents in the table.
	/// </summary>
	/// <param name="tableLevel">Table level.</param>
	/// <param name="ID">ID of the level.</param>
	private void SettingUpLevel (TableLevel tableLevel, int ID)
	{
		if (tableLevel == null) {
			return;
		}
		
		///Get World Data of the current World
		
		tempWorldData = DataManager.FindWorldDataById (World.selectedWorld.ID, DataManager.filterdWorldsData);
	//	Debug.Log("tempworlddata0 " + World.selectedWorld.ID + ".." + DataManager.filterdWorldsData);
	//	Debug.Log("tempworlddata " + World.selectedWorld.ID + tempWorldData + "..." + DataManager.filterdWorldsData);
		if (tempWorldData == null) {
			Debug.Log ("Null WorldData");
			return;
		}
		
		///Get Level Data of the wanted (selected) Level
		tempLevelData = tempWorldData.FindLevelDataById (tableLevel.ID);
		if (tempLevelData == null) {
			Debug.Log ("Null LevelData");
			return;
		}
//		Debug.Log("tempLevelData " + tableLevel.ID + "..."+tempLevelData.isLocked + "...."+tempLevelData);
		///		Debug.Log("time for levels " + tempLevelData.leveltime);
		
		//If the level is locked then , skip the next
		if (tempLevelData.isLocked) {
			return;
		}
		//show summary time
		TotalData.LoadTotalFromFile();
//		Debug.Log ("totaldata: " + TotalData.totalData.totalCoins);
//		Debug.Log("levelsTable: " + currentTotalData.totalTime)
	//	sumTime  = currentTotalData.totalTime;
//		Debug.Log ("Worldtime: " + TotalData.totalData.totalTime);
	//	Debug.Log ("leveltime: " + tempLevelData.leveltime);
	//	Debug.Log ("levelScore: " +  tempLevelData.levelScore);
	//	sumTime = TotalData.totalData.totalTime;
	//	sumTime = tempWorldData.Worldtime;
	//	sumTime += tempLevelData.leveltime;
	//	time.text = ": " + currentTotalData.totalCoins.ToString();
		time.text = ": " + TotalData.totalData.totalTime.ToString();

		//show summary coins
	//	sumCoins = 
		sumCoins += tempLevelData.levelScore;
		coins.text = ": " + TotalData.totalData.totalCoins.ToString();
//		Debug.Log ("data: " + TotalData.totalData.totalCoins);
//		Debug.Log ("laser: " + TotalData.totalData.laser);

	//	Debug.Log ("time: " + tempLevelData.leveltime );
		//Enable level animator
		tableLevel.GetComponent<Animator> ().enabled = true;
		
		///Make the button interactable
		tableLevel.GetComponent<Button> ().interactable = true;
		
		///Show the stars of the level
		tableLevel.transform.Find ("Stars").gameObject.SetActive (true);
		
		///Hide the lock
		tableLevel.transform.Find ("Lock").gameObject.SetActive (false);

		tableLevel.transform.Find ("Background").gameObject.SetActive (true);
		
		///Show the title of the level
		tableLevel.transform.Find ("LevelTitle").gameObject.SetActive (true);
		
		///Setting up the level title
		tableLevel.transform.Find ("LevelTitle").GetComponent<Text> ().text = ID.ToString ();
		
		///Get stars Number from current Level Data
		tableLevel.starsNumber = tempLevelData.starsLevel;
		tempTransform = tableLevel.transform.Find ("Stars");
		
		///Apply the current Stars Rating 
		if (tempLevelData.starsLevel == TableLevel.StarsNumber.ONE) {//One Star
			tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
			tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOff;
			tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
		} else if (tempLevelData.starsLevel == TableLevel.StarsNumber.TWO) {//Two Stars
			tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
			tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOn;
			tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
		} else if (tempLevelData.starsLevel == TableLevel.StarsNumber.THREE) {//Three Stars
			tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOn;
			tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOn;
			tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOn;
		} else {//Zero Stars
			tempTransform.Find ("FirstStar").GetComponent<Image> ().sprite = starOff;
			tempTransform.Find ("SecondStar").GetComponent<Image> ().sprite = starOff;
			tempTransform.Find ("ThirdStar").GetComponent<Image> ().sprite = starOff;
		}

	}
}