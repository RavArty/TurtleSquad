using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class TimerCountdown : MonoBehaviour {
	/// <summary>
	/// Text Component
	/// </summary>
	public Text uiText;
	public GameObject LevelEndIntro;
	public int seconds;
	
	/// <summary>
	/// The time in seconds.
	/// </summary>
	[HideInInspector]
	public int
		timeInSeconds;
	
	/// <summary>
	/// Whether the Timer is running
	/// </summary>
	private bool isRunning;
	
	void Awake ()
	{
		if (uiText == null) {
			uiText = GetComponent<Text> ();
		}
		///Start the Timer
		Start ();
	}
	
	/// <summary>
	/// Start the Timer.
	/// </summary>
	public void Start ()
	{
		if (!isRunning) {
			isRunning = true;
			timeInSeconds = seconds;
			StartCoroutine ("Wait");
		}
	}
	
	/// <summary>
	/// Stop the Timer.
	/// </summary>
	public void Stop ()
	{
		if (isRunning) {
			isRunning = false;
			StopCoroutine ("Wait");
		}
	}
	
	/// <summary>
	/// Reset the timer.
	/// </summary>
	public void Reset ()
	{
		Stop ();
		Start ();
	}
	
	/// <summary>
	/// Wait Coroutine.
	/// </summary>
	private IEnumerator Wait ()
	{
		while (isRunning) {
			ApplyTime ();
			yield return new WaitForSeconds (1);
			timeInSeconds--;
			if(timeInSeconds == 0){
		//		Debug.Log("timer countdown stop");
			//	GameObject.FindObjectOfType<GameManager> ().OnLevelComplete ();
				LevelEndIntro.SetActive(true);
			}
		}
	}
	
	/// <summary>
	/// Applies the time into TextMesh Component.
	/// </summary>
	private void ApplyTime ()
	{
		if (uiText == null) {
			return;
		}
		int mins = timeInSeconds / 60;
		int seconds = timeInSeconds % 60;
		
		uiText.text = ": " + GetNumberWithZeroFormat (mins) + ":" + GetNumberWithZeroFormat (seconds);
	}
	
	/// <summary>
	/// Get the number with zero format.
	/// </summary>
	/// <returns>The number with zero format.</returns>
	/// <param name="number">Ineger Number.</param>
	public static string GetNumberWithZeroFormat (int number)
	{
		string strNumber = "";
		if (number < 10) {
			strNumber += "0";
		}
		strNumber += number;
		
		return strNumber;
	}
}