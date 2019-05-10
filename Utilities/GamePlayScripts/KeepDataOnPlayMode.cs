// Keep data between scenes

using UnityEngine;
using System.Collections;
using Facebook.Unity;

[DisallowMultipleComponent]
public class KeepDataOnPlayMode : MonoBehaviour {

	public static KeepDataOnPlayMode instance = null;
	[HideInInspector]
	public bool 	reloadedLevel = false;
	public int 		reloadedTimes = 0;
	public int 		randomAds = 0;

	[HideInInspector]
	public bool 	isSoundOn = true;
	[HideInInspector]
	public bool wordlScene = false;

	void Awake () {
//		Debug.Log("keepon: " + wordlScene);
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);

		if (FB.IsInitialized) {
			FB.ActivateApp();
		} else {
			//Handle FB.Init
			FB.Init( () => {
				FB.ActivateApp();
			});
		}

		#if UNITY_IOS
		Application.targetFrameRate = 60;
		#endif

	}

	void Start(){
		randomAds = generateIntAds ();

	}

	public int generateIntAds(){
		int randomAd = Random.Range (3, 5);
		return randomAd;
	}
}
