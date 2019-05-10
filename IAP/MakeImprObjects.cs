#if UNITY_EDITOR

using UnityEngine;
using System.Collections;
using UnityEditor;


public class MakeImprObjects {

	[MenuItem("Assets/Create/Improvements")]
	public static void CreateImprovements()
	{
		Improvements asset = ScriptableObject.CreateInstance<Improvements>();

		AssetDatabase.CreateAsset (asset, "Assets/Prefab/Improvements/NewImprovement.asset");
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
}

#endif