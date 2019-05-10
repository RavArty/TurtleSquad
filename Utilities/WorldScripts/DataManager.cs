using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class DataManager : MonoBehaviour
{
	
	/// <summary>
	/// The scene worlds data.
	/// (will be loaded from the Worlds scene)
	/// </summary>
	private static List<WorldData> sceneWorldsData;
	
	/// <summary>
	/// The file worlds data.
	/// (will be loaded from the file)
	/// </summary>
	private static List<WorldData> fileWorldsData;
	
	/// <summary>
	/// The filterd worlds data.
	/// (The final worlds data after filtering)
	/// </summary>
	public static List<WorldData> filterdWorldsData;

//	public static WorldData worldDataObj;


	
	/// <summary>
	/// The name of the file.
	/// </summary>
	private static string fileName = "turtlesquad.bin";
	
	/// <summary>
	/// The path of the file.
	/// </summary>
	private static string path;
	
	/// <summary>
	/// Whether the Worlds data is empty or null.
	/// </summary>
	private static bool isNullOrEmpty;
	
	
	/// <summary>
	/// Whether it's need to save new data.
	/// </summary>
	private static bool needsToSaveNewData;
	
	
	
	void Awake ()
	{
		try {
			#if UNITY_IPHONE
			//Enable the MONO_REFLECTION_SERIALIZER(For IOS Platform Only)
			System.Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
			#endif
			
			///Reset flags
			isNullOrEmpty = false;
			needsToSaveNewData = false;
			
			///Load Worlds data from the Worlds Scene
			sceneWorldsData = LoadWorldsDataFromScene ();
			if (sceneWorldsData == null) {
				return;
			}
			
			if (sceneWorldsData.Count == 0) {
				return;
			}
			
			///Load Worlds data from the file
			fileWorldsData = LoadWorldsDataFromFile ();
			
			if (fileWorldsData == null) {
				isNullOrEmpty = true;
			} else {
				if (fileWorldsData.Count == 0) {
					isNullOrEmpty = true;
				}
			}
			
			///If the Worlds data in the file is null or empty,then save the scene Worlds data to the file
			if (isNullOrEmpty) {
				SaveWorldsDataToFile (sceneWorldsData);
				filterdWorldsData = sceneWorldsData;
			} else {
				///Otherwise get the filtered Worlds Data
				filterdWorldsData = GetFilterdWorldsData ();
				///If it's need to save a new Worlds data to the file ,the save it
				if (needsToSaveNewData) {
					SaveWorldsDataToFile (filterdWorldsData);
				}
				//	WorldsUnlock();
			}
		} catch (Exception ex) {
			Debug.Log (ex.Message);
		}
		Debug.Log("datamanager loaded");
	}




	///WorldData is class used for persistent loading and saving
	[System.Serializable]
	public class WorldData
	{
		public int ID;//the ID of the World
		public int Worldtime = 0;
		public int worldScore = 0;
		public bool WorldIsLocked = true;
		public List<LevelData> levelsData = new List<LevelData> ();//the levels of the world
		
		/// <summary>
		/// Find the level data by ID.
		/// </summary>
		/// <returns>The level data.</returns>
		/// <param name="ID">the ID of the level.</param>
		public LevelData FindLevelDataById (int ID)
		{
			foreach (LevelData levelData in levelsData) {
				if (levelData.ID == ID) {
					return levelData;
				}
			}
			return null;
		}
	}
	
	///LevelData is class used for persistent loading and saving
	[System.Serializable]
	public class LevelData
	{
		public int ID;//THE id of the level
		public bool isLocked = true;
		public int leveltime = 0;
		public int levelScore = 0;
		public TableLevel.StarsNumber starsLevel = TableLevel.StarsNumber.ZERO;//number of stars (stars rating)
	}
	
	
	/// <summary>
	/// Reset the game data.
	/// </summary>
	public static void ResetData ()
	{
		try {
			fileWorldsData = LoadWorldsDataFromFile ();
			
			if (fileWorldsData == null) {
				return; 
			}
			
			foreach (WorldData worldData in fileWorldsData) {
				if (worldData == null) {
					continue;
				}
				foreach (LevelData levelData in worldData.levelsData) {
					if (levelData == null) {
						continue;
					}
					
					///UnLock the level of ID equals 1(first level) ,otherwise lock the others
					if (levelData.ID == 1) {
						levelData.isLocked = false;
					} else {
						levelData.isLocked = true;
					}
					
					///Set star level to zero for each level
					levelData.starsLevel = TableLevel.StarsNumber.ZERO;
				}
			}
			
			SaveWorldsDataToFile (fileWorldsData);
		} catch (Exception ex) {
			Debug.Log (ex.Message);
			return;
		}
		Debug.Log ("Game Data has been reset successfully");
	}
	
	private void WorldsUnlock(){
		
		GameObject [] Worlds = GameObject.FindGameObjectsWithTag ("World");
		GameObject tableWorldGameObject = null;
		Debug.Log ("Worlds array: " + Worlds);
		World TempWorld = null;
		
		foreach (GameObject worldGameObject in Worlds) {
			TempWorld = worldGameObject.GetComponent<World> ();
			foreach (WorldData worldData in fileWorldsData) { 
				if(worldData.ID == TempWorld.ID){
					if(!worldData.WorldIsLocked){
						TempWorld.transform.Find ("Lock").gameObject.SetActive (false);
					}
				}
			}
			
		}
		
		
	}
	public void UnlockSubFunc(World WorldSub, int ID){
		
		foreach (WorldData worldData in fileWorldsData) { 
			Debug.Log ("worldData sub " + worldData.ID +worldData.WorldIsLocked);
			if(worldData.ID == ID ||worldData.ID == 1){
				if(!worldData.WorldIsLocked){
					WorldSub.transform.Find ("Lock").gameObject.SetActive (false);
					//		WorldSub.GetComponent<Button> ().interactable = true;
				//	WorldSub.GetComponent<Animator> ().enabled = true;
					
				}
			}
			
		}
	}
	
	/// <summary>
	/// Load the worlds data from the scene.
	/// </summary>
	/// <returns>The worlds data from the scene.</returns>
	private List<WorldData> LoadWorldsDataFromScene ()
	{
//		Debug.Log ("Loading Worlds Data");
		
		GameObject [] worlds = GameObject.FindGameObjectsWithTag ("World");
		
		if (worlds == null) {
			Debug.Log ("No World with 'World' tag found");
			return null;
		}
		
		World tempWorld = null;
		
		List<WorldData> tempWorldsData = new List<WorldData> ();
		WorldData tempWorldData = null;
		foreach (GameObject worldGameObject in worlds) {
			tempWorld = worldGameObject.GetComponent<World> ();
			tempWorldData = new WorldData ();
			
			tempWorldData.ID = tempWorld.ID;
			tempWorldData.WorldIsLocked = tempWorld.WorldIsLocked;
			tempWorldData.levelsData = GetLevelData (tempWorld.levels);
//			Debug.Log ("Scene: " + tempWorldData.WorldIsLocked+"..."+tempWorldData.ID);
			if(tempWorldData.ID == 1 || !tempWorldData.WorldIsLocked){
				tempWorld.transform.Find ("Lock").gameObject.SetActive (false);
				tempWorld.GetComponent<Button> ().interactable = true;
			//	tempWorld.GetComponent<Animator> ().enabled = true;
			}
			
			tempWorldsData.Add (tempWorldData);
		}
		
		return tempWorldsData;
	}
	
	/// <summary>
	/// Get the level data.
	/// </summary>
	/// <returns>The new levels data.</returns>
	/// <param name="levels">The current levels data.</param>
	private List<LevelData> GetLevelData (String [] levels)
	{
		if (levels == null) {
			return null;
		}
		
		LevelData tempLevelData = null;
		List<LevelData> tempLevelsData = new List<LevelData> ();
		int ID = 1;
		for (int i = 0; i <levels.Length; i++) {
			tempLevelData = new LevelData ();
			tempLevelData.ID = ID;
			ID++;
			if (i == 0) {
				///First level must be unlocked
				tempLevelData.isLocked = false;
			}
			tempLevelsData.Add (tempLevelData);
		}
		
		return tempLevelsData;
	}
	
	/// <summary>
	/// Get the filterd worlds data.
	/// (Compare the Worlds data in the file with the Worlds data in the scene)
	/// Scenario :
	/// -you may have a set of worlds saved into file
	/// -if you add/delete a world or level
	/// -then it's need to determine and save the new data
	/// </summary>
	/// <returns>The filterd worlds data.</returns>
	private List<WorldData> GetFilterdWorldsData ()
	{
		if (fileWorldsData == null || sceneWorldsData == null) {
			return null;
		}
		
		WorldData tempWorldData = null;
		List<WorldData> tempFilteredWorldsData = new List<WorldData> ();
		
		foreach (WorldData worldData in sceneWorldsData) {
			///Get the World data from the file
			tempWorldData = FindWorldDataById (worldData.ID, fileWorldsData);
			if (tempWorldData != null) {
				///If the number of levels in the World Equals the number of levels in the file 
				if (worldData.levelsData.Count == tempWorldData.levelsData.Count) {
					///Add tempWorldData(file world data) to the filtered list
					tempFilteredWorldsData.Add (tempWorldData);
				} else {//Otherwise,its need to save new data
					//TODO:levels data will be lost,when a level is added or removed
					needsToSaveNewData = true;
					///Add the  worldData(scene world data) to the filtered list 
					tempFilteredWorldsData.Add (worldData);
				}
			} else {//Otherwise,its need to save new data
				needsToSaveNewData = true;
				///Add the worldData(scene world data) to the filtered list 
				tempFilteredWorldsData.Add (worldData);
			}
		}
		return tempFilteredWorldsData;
	}
	
	/// <summary>
	/// Save the worlds data to the file.
	/// </summary>
	/// <param name="worldsData">Worlds data.</param>
	public static void SaveWorldsDataToFile (List<WorldData> worldsData)
	{
		Debug.Log ("Saving Worlds Data");
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (path)) {
			Debug.Log ("Null or Empty path");
			return;
		}
		
		if (worldsData == null) {
			Debug.Log ("Null Data");
			return;
		}
		
		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (path, FileMode.Create);
			bf.Serialize (file, worldsData);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}
	}


	/// <summary>
	/// Load the worlds data from the file.
	/// </summary>
	/// <returns>The Worlds data.</returns>
	public static List<WorldData> LoadWorldsDataFromFile ()
	{
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (path)) {
			Debug.Log ("Null or Empty path");
			return null;
		}
		if (!File.Exists (path)) {
			Debug.Log (path + " is not exists");
			return null;
		}
		
		List<WorldData> worldsData = null;
		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (path, FileMode.Open);
			worldsData = (List<WorldData>)bf.Deserialize (file);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}
		
		return worldsData;
	}
	
	/// <summary>
	/// Finds the world data by id.
	/// </summary>
	/// <returns>the world data by ID.</returns>
	/// <param name="ID">the ID of the world.</param>
	/// <param name="worldsData">Worlds data list to search in.</param>
	public static WorldData FindWorldDataById (int ID, List<WorldData> worldsData)
	{
		if (worldsData == null) {
			return null;
		}
		
		foreach (WorldData worldData in worldsData) {
			if (worldData.ID == ID) {
				return worldData;
			}
			
		}
		return null;
	}
	
	/// <summary>
	/// Settings up the path of the file ,relative to the current platform.
	/// </summary>
	private static void SettingUpFilePath ()
	{
		if (Application.platform == RuntimePlatform.Android) {//Android Platform
			path = Application.persistentDataPath;
		} else if (Application.platform == RuntimePlatform.IPhonePlayer) {//IOS Platform
			///Get iPhone Documents Path
			path = Application.persistentDataPath;
		//	string dataPath = Application.dataPath;
		//	string fileBasePath = dataPath.Substring (0, dataPath.Length - 5);
		//	path = fileBasePath.Substring (0, fileBasePath.LastIndexOf ('/')) + "/Documents";
		} else {//Others
			path = Application.dataPath;
		}
		
		path += "/" + fileName;
	}
	
}