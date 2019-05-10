using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayVideo : MonoBehaviour {

//	public MovieTexture movie;

	// Use this for initialization
	void Start () {
		Handheld.PlayFullScreenMovie("/Audio/test.mp4", Color.blue, FullScreenMovieControlMode.Hidden);
	
	}
	
	// Update is called once per frame
	void Update () {

	}
}
