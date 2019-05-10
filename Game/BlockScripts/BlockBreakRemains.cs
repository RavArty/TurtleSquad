// Destroy tree blocks in pecies

using UnityEngine;
using System.Collections;

public class BlockBreakRemains : MonoBehaviour {


	private GameObject		remains_obj;
	public GameObject		remains;



	public void DestroyBlock(GameObject block){
		
		Destroy(block);

		remains_obj = ObjectPool.current.GetObject(remains);
		remains_obj.transform.position = transform.position;
		remains_obj.transform.rotation = transform.rotation;
		remains_obj.SetActive(true);
	
		for(int i = 0; i < remains_obj.transform.childCount; i++){
	
			var dir = remains_obj.transform.GetChild(i).localPosition; 
			float calc = 1 - (dir.magnitude / 10);
			if(calc <= 0){
				calc = 0;

			}
	
			remains_obj.transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(dir.normalized * calc * 5000);
		
			FadeObject.instance.FadeOut(remains_obj.transform.GetChild(i).gameObject, 1.0f);
		
		}

		remains_obj.GetComponent<DestroyRemains>()._DestroyRemains();
		Destroy (transform.gameObject);
	}
}
