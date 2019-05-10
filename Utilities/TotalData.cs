using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class TotalData {
	
	public static Total totalData;
//	public static int coins = 0;


	/// <summary>
	/// The name of the file.
	/// </summary>
	private static string fileName = "turtlesquad2.bin";

	/// <summary>
	/// The path of the file.
	/// </summary>
	private static string path;

	/// <summary>
	/// Whether the Worlds data is empty or null.
	/// </summary>
	private static bool isNullOrEmpty;







	///LevelData is class used for persistent loading and saving
	[System.Serializable]
	public class Total
	{
		public bool seventhLevelArm = false;
		public bool fifthLevelArm = false;
		public bool secondLevelArm = false;
		public bool noads = false;
		public int totalTime = 0;
		public int totalCoins = 0;
		public int green = 0;
		public int laser = 0;
		public int blue  = 0;

	}

	/// <summary>
	/// Save the worlds data to the file.
	/// </summary>
	/// <param name="worldsData">Worlds data.</param>
	public static void SaveTotalToFile ()
	{
		Debug.Log ("Saving Total Data");
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (path)) {
			Debug.Log ("Null or Empty path -TotalData-");
			return;
		}
			
		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (path, FileMode.Create);
			bf.Serialize (file, TotalData.totalData);
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
	public static void LoadTotalFromFile ()
	{
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (path)) {
			Debug.Log ("Null or Empty path");
//			return null;
		}
		if (!File.Exists (path)) {
			Debug.Log (path + " is not exists");
			TotalData.totalData = new Total();
			TotalData.totalData.totalCoins = 0;
			TotalData.totalData.totalTime = 0;
			TotalData.totalData.green = 3;
			TotalData.totalData.blue = 3;
			TotalData.totalData.laser = 3;
			TotalData.totalData.noads = false;			//change for free version to "false"
			TotalData.totalData.secondLevelArm = false;
			TotalData.totalData.fifthLevelArm = false;
			SaveTotalToFile();
		//	CreateFile();
//			return null;
		}

		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (path, FileMode.Open);
//			Debug.Log("opened file");
			TotalData.totalData = (Total)bf.Deserialize (file);
//			Debug.Log("totalData: " + totalData);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}

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
	//		string dataPath = Application.dataPath;
	//		string fileBasePath = dataPath.Substring (0, dataPath.Length - 5);
	//		path = fileBasePath.Substring (0, fileBasePath.LastIndexOf ('/')) + "/Documents";
		} else {//Others
//			Debug.Log("Application.dataPath: " + Application.dataPath);
			path = Application.dataPath;
		}

		path += "/" + fileName;
	}

	private static void CreateFile(){
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (path)) {
			Debug.Log ("Null or Empty path -TotalData-");
			return;
		}

		FileStream file = null;
		try {
			Debug.Log("create file");
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (path, FileMode.CreateNew);
			bf.Serialize (file, TotalData.totalData);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}
	}
}
