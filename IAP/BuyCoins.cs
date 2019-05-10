using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuyCoins : MonoBehaviour {

	private Text totalCoins;
	public Text totalProgress;
	private int  curValue;
	public float duration = 2;

	void Awake(){
		totalCoins = GameObject.Find ("CoinsAmountAfterBuying").GetComponent<Text>();
		#if UNITY_IOS
		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
		#endif
	}

	void Start(){
		TotalData.LoadTotalFromFile ();
//		Debug.Log (TotalData.totalData.totalCoins);
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



}
