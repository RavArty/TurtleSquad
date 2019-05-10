using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingOverlay : MonoBehaviour {

	private GameObject loadingOverlay;

	void Awake(){
/*		Transform[] children = GameObject.Find("AllObjects").gameObject.transform.GetComponentsInChildren<Transform>(true);
		foreach (Transform t in children) {
			Debug.Log("t: " + t.gameObject.name);
			if (t.gameObject.name == "LoadingOverlay"){
				Debug.Log("ttttttt: " + t.gameObject.name);
				loadingOverlay = t.gameObject;
			}
		}
*/
		loadingOverlay = GameObject.Find("LoadingOverlayCanvas").transform.GetChild(0).gameObject;
	}

	public void loadOverlayTrue(){
		loadingOverlay.SetActive(true);
	}
	public void loadOverlayFalse(){
		loadingOverlay.SetActive(false);
	}
/*
	public GameObject LoadingBar;
	public GameObject TextIndicator;
	public GameObject TextLoading;

	[SerializeField] private float currentAmount;
	private float speed;
	private bool loadBarInd = true;

	public  void loadOverlayTrue(){
		loadBarInd = true;
		speed = GameObject.FindObjectOfType<UIEvents> ().progressBar;
		LoadingBar.SetActive(true);
	}
	void Start(){
		speed = GameObject.FindObjectOfType<UIEvents> ().progressBar;
	}
	public  void loadOverlayFalse(){
		loadBarInd = false;
		LoadingBar.SetActive(false);
	}

	void Update(){
		if(loadBarInd){
			if(currentAmount < 100){
				currentAmount += speed * Time.deltaTime;
				TextIndicator.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
				TextLoading.gameObject.SetActive(true);
			}else{
				TextLoading.gameObject.SetActive(false);
				TextIndicator.GetComponent<Text>().text = "Done!";
			}
			LoadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
		}
	}
	*/
}
