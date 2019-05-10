using UnityEngine;
using System.Collections;
using System;

#pragma warning disable 0168 // variable declared but not used.

public class World : MonoBehaviour
{
	/// <summary>
	/// The title of the world.
	/// </summary>
	public string title = "";
	public int world_number;
	
	/// <summary>
	/// The id of the world.
	/// </summary>
	[HideInInspector]
	public int ID;
	
	/// <summary>
	/// The shapes of the world.
	/// </summary>
	public String[] levels;
	
	[HideInInspector]
	public bool WorldIsLocked = true;
	
	/// <summary>
	/// The selected world.
	/// </summary>
	public static World selectedWorld;
	
	// Use this for initialization
	void Awake ()
	{
		///Setting up the ID of the Mission
		try {
			ID = int.Parse (name.Split ('-') [1]);
//			Debug.Log ("name: "+ gameObject.name);
			if (string.IsNullOrEmpty (title)) {
				title = "World " + gameObject.name.Split ('-') [1];
			}
		} catch (Exception ex) {
			Debug.LogError ("Invalid World Name");
		}
//		Debug.Log ("Setting up World " + title + " of ID " + ID);
	}
}
