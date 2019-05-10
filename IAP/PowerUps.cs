using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUps : MonoBehaviour {

	private Text totalCoins;
	public Text totalProgress;
	private int  curValue;
	public float duration = 2;

	void Awake(){
		totalCoins = GameObject.Find ("CoinsAmount").GetComponent<Text>();
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}

	void Start(){
		TotalData.LoadTotalFromFile ();
		totalCoins.text = TotalData.totalData.totalCoins.ToString();
	}

	public void updateCoinsAfterAppearing(){
		totalCoins.text = TotalData.totalData.totalCoins.ToString();
	}

	public void updateCoins(int current, int target){
		StartCoroutine (CountToFrom (current, target));
	}

	IEnumerator CountToFrom(int current, int target){
		//	Debug.Log ("started fun");
		for (float timer = 0; timer < duration; timer += Time.unscaledDeltaTime) {
			float progress = timer / duration;
			curValue = (int)Mathf.Lerp (current, target, progress);
			totalProgress.text = curValue + "";
			yield return null;

		}
		//		Debug.Log ("started fun 2");
		curValue = TotalData.totalData.totalCoins;
		totalProgress.text = curValue + "";
	}

	public IEnumerator CountTo(){
		//		Debug.Log ("started fun");
		for (float timer = 0; timer < duration; timer += Time.unscaledDeltaTime) {
			float progress = timer / duration;
			curValue = (int)Mathf.Lerp (0, TotalData.totalData.totalCoins, progress);
			totalProgress.text = curValue + "";
			yield return null;

		}
		//	Debug.Log ("started fun 2");
		curValue = TotalData.totalData.totalCoins;
		totalProgress.text = curValue + "";
	}

	public void buyGreen(){
		if (TotalData.totalData.totalCoins >= 300) {
			int curValue = TotalData.totalData.totalCoins;
			TotalData.totalData.totalCoins -= 300;
			TotalData.totalData.green += 1;
			TotalData.SaveTotalToFile();
			updateCoinsAfterAppearing ();
		//	updateCoins (curValue, TotalData.totalData.totalCoins);
		//	totalCoins.text = TotalData.totalData.totalCoins.ToString();
			GameObject.FindObjectOfType<SlideShowAmount> ().updatePurchases();
		}
	}

	public void buyBlue(){
		if (TotalData.totalData.totalCoins >= 100) {
			int curValue = TotalData.totalData.totalCoins;
			TotalData.totalData.totalCoins -= 100;
			TotalData.totalData.blue += 1;
			TotalData.SaveTotalToFile();
			updateCoinsAfterAppearing ();
		//	updateCoins (curValue, TotalData.totalData.totalCoins);
		//	totalCoins.text = TotalData.totalData.totalCoins.ToString();
			GameObject.FindObjectOfType<SlideShowAmount> ().updatePurchases();
		}
	}

	public void buyLaser(){
		if (TotalData.totalData.totalCoins >= 70) {
			int curValue = TotalData.totalData.totalCoins;
			TotalData.totalData.totalCoins -= 70;
			TotalData.totalData.laser += 1;
			TotalData.SaveTotalToFile();
			updateCoinsAfterAppearing ();
		//	updateCoins (curValue, TotalData.totalData.totalCoins);
		//	totalCoins.text = TotalData.totalData.totalCoins.ToString();
			GameObject.FindObjectOfType<SlideShowAmount> ().updatePurchases();
		}
	}

}
