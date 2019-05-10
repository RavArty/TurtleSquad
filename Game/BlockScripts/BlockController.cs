// Block press management

using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

	[SerializeField] private LayerMask TapLayer;

//---------------------------------------------------------------------------------------------------------------
	void FixedUpdate(){

		RaycastHit2D blockhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero, Mathf.Infinity, TapLayer);

		if(blockhit.collider != null){
			if(blockhit.collider.transform.tag == "ground_tree"){
		if (Input.touchCount > 0) {

			var touch = Input.GetTouch(Input.touchCount - 1);

			switch (touch.phase) {


			case TouchPhase.Ended:

				if(Time.timeScale == 1){
					

					blockhit.collider.transform.GetComponent<BlockBreak>().Break();
				
				}
				break;
					}
				}
			}
		}
	}

}
