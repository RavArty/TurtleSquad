using UnityEngine;
using System.Collections;

public class LionTriggerCheck : MonoBehaviour {

	private int intFront = 0;
	private int intBack = 0;
	private int intAbove = 0;
	private int intUnder = 0;

	void OnTriggerEnter2D(Collider2D other) {
	
		if(transform.name == "frontCheck" && (other.CompareTag("green")|| other.CompareTag("blue"))){
	
			intFront = 1;
			StartCoroutine(Attack(other.gameObject));
		}else if(transform.name == "frontCheck" && (!other.CompareTag("green")|| !other.CompareTag("blue"))){

			intFront = 2;
			transform.parent.gameObject.GetComponent<Enemy>().FrontCheck(other.gameObject);
		}else if(transform.name == "backCheck" && !other.CompareTag("stone")){
			intBack = 1;

			transform.parent.gameObject.GetComponent<Enemy>().BackCheck(other.gameObject);
		}else if(transform.name == "backCheck" && other.CompareTag("stone")){
			transform.parent.gameObject.GetComponent<Enemy>().BackCheckStone(other.gameObject);
		}else if(transform.name == "underCheck" && other.CompareTag("stone")){
			transform.parent.gameObject.GetComponent<Enemy>().TranformTo();
		}else if(transform.name == "underCheck" && (other.CompareTag("green") || other.CompareTag("blue"))){
			intUnder = 1;
			StartCoroutine(Attack(other.gameObject));
		}else if(transform.name == "aboveCheck" && (other.CompareTag("green") || other.CompareTag("blue"))){
			intAbove = 1;
			StartCoroutine(Attack(other.gameObject));
		}
	}
	IEnumerator Attack(GameObject obj){
		for(;;){
			if(obj != null && !obj.gameObject.GetComponent<Health>().isDead)
				transform.parent.gameObject.GetComponent<Enemy>().FrontCheck(obj);
		yield return new WaitForSeconds(0.5f);
		}
	}

	IEnumerator StartWalk(){
		yield return new WaitForSeconds(0.5f);
		transform.parent.gameObject.GetComponent<Enemy>().stopWalking = false;
	}

	void OnTriggerExit2D(Collider2D other){
		if(intFront == 1){
			intFront = 0;
			StopAllCoroutines();
			StartCoroutine(StartWalk());
		}else if(intFront == 2){
			intUnder = 0;
			StopAllCoroutines();
			StartCoroutine(StartWalk());
		}else if(intUnder == 1){
			intUnder = 0;
			StopAllCoroutines();
			StartCoroutine(StartWalk());
		}else if(intAbove == 1){
			intAbove = 0;
			StopAllCoroutines();
			StartCoroutine(StartWalk());
		}

	
	}
}
