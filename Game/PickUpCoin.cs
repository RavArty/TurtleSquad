using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickUpCoin : MonoBehaviour {

	public  GameObject 	ui_points;


	void OnCollisionEnter2D(Collision2D collider){
		if((collider.transform.tag == "green" || collider.transform.tag == "blue") 
			&& !collider.gameObject.GetComponent<Health>().isDead){
			//			if(!collider.GetComponent<Health>().isDead){
			CollectCoin();
		}
	}
	void OnTriggerEnter2D (Collider2D collider){
//		Debug.Log("collider: " + collider.transform.tag );
		if((collider.transform.tag == "green" || collider.transform.tag == "blue") ){
				CollectCoin();
		}
	}
	
	void CollectCoin(){
	    if(gameObject != null){
			Vector2 scorePos;
			scorePos = transform.position;
			scorePos.y += 2.0f;
			Instantiate(ui_points, scorePos, Quaternion.identity);
			if(gameObject != null){

				gameObject.GetComponent<CoinEffect>().CreateStarsEffect();
				ObjectPool.current.PoolObject (gameObject);
	
			}
		
		GameObject.FindObjectOfType<CoinScore> ().CollectCoin (1);
		}
	}

}
