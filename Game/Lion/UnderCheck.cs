using UnityEngine;
using System.Collections;

public class UnderCheck : MonoBehaviour {

	public GameObject lion;
	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate (){
		Collider2D[] underHits = Physics2D.OverlapPointAll(gameObject.transform.position);

		for(int i = 0; i < underHits.Length; i++){
			if(underHits[i].tag == "stone" && !lion.GetComponent<Enemy>().dead){
				gameObject.transform.position = lion.GetComponent<Enemy>().startPosition;
			}
		}
	}
}
