using UnityEngine;
using System.Collections;

public class LaserOnOff : MonoBehaviour {

	public GameObject OnButton;
	public GameObject OffButton;

	public void ActivateOffButton(){
	//	Debug.Log("off");
		OffButton.SetActive(true);
		OnButton.SetActive(false);
		GameObject[] cannons = GameObject.FindGameObjectsWithTag("cannon");
		for(int j = 0; j < cannons.Length; j++){
			if(cannons[j].GetComponent<CannonCollision>()){
				for(int i = 0; i < cannons[j].GetComponent<CannonCollision>().laser.Length; i++){
				//	cannons[j].GetComponent<CannonCollision>().laser[i].SetActive(true);
					cannons[j].GetComponent<CannonCollision>().laser[i].GetComponent<Animator>().SetBool("Active", true);
				}
			}
		}
	}

	public void ActivateOnButton(){
	//	Debug.Log("on");
		OffButton.SetActive(false);
		OnButton.SetActive(true);
		GameObject[] cannons = GameObject.FindGameObjectsWithTag("cannon");
		for(int j = 0; j < cannons.Length; j++){
			if(cannons[j].GetComponent<CannonCollision>()){
				for(int i = 0; i < cannons[j].GetComponent<CannonCollision>().laser.Length; i++){
				//	cannons[j].GetComponent<CannonCollision>().laser[i].SetActive(true);
					cannons[j].GetComponent<CannonCollision>().laser[i].GetComponent<Animator>().SetBool("Active", false);
				}
			}
		}
	}

}
