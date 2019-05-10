using UnityEngine;
using System.Collections;

public class DestroyRemains : MonoBehaviour {
	
	void Awake(){
		StartCoroutine(DestroyParent());

		
	}
	public void _DestroyRemains(){
//		Debug.Log("destroy remains");
		StartCoroutine(DestroyElements());
	}
	public void DestroyPolygonCollider(){
		StartCoroutine(DestroyPolygonColliderCoroutine());
	}
	public void DisableObject(){
		StartCoroutine(DestroyParent());
	}
	
	IEnumerator FadeTo(float aValue, float aTime)
	{
		yield return new WaitForSeconds(1.0f);
		
		foreach(Transform child in gameObject.transform)
		{
			float alpha = child.transform.GetComponent<Renderer>().material.color.a;
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
			{
				Color newColor = new Color(1, 1, 1, Mathf.Clamp(alpha,aValue,t));
				child.transform.GetComponent<Renderer>().material.color = newColor;
				
			}
		}
		yield return null;
	}

	IEnumerator DestroyPolygonColliderCoroutine()
	{	
//		Debug.Log("DestroyElements");
		yield return new WaitForSeconds(1);
//		Debug.Log("DestroyElements 1sec");

		foreach(Transform child in gameObject.transform)
		{
			if(child.GetComponent<PolygonCollider2D>()){
				child.GetComponent<PolygonCollider2D>().isTrigger = true;
			}
		}
		//		Debug.Log("DestroyElements end");
	}

	IEnumerator DestroyElements()
	{	
//				Debug.Log("DestroyElements");
		yield return new WaitForSeconds(1);
//				Debug.Log("DestroyElements 1sec");
		
		foreach(Transform child in gameObject.transform)
		{
			if (child.GetComponent<PolygonCollider2D> ()) {
				child.GetComponent<PolygonCollider2D> ().isTrigger = true;
			}
			if (child.GetComponent<CircleCollider2D> ()) {
				child.GetComponent<CircleCollider2D> ().isTrigger = true;
			}
		}
		//		Debug.Log("DestroyElements end");
	}

	IEnumerator DestroyParent()
	{	
		//		Debug.Log("DestroyElements");
		yield return new WaitForSeconds(1);
//		ObjectPool.current.PoolObject (gameObject);
	//	Destroy(gameObject);


	}
}
