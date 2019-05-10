using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReduceShadowSize : MonoBehaviour {

	public LayerMask groundCheckLayerMask;
	public GameObject shadow;
	public GameObject point1;
	public GameObject point2;
	public GameObject point3;
	public GameObject point4;

	public float point1OffSet;
	public float point2OffSet;
	public float point3OffSet;
	public float point4OffSet;

	private bool grounded1;
	private bool grounded2;
	private bool grounded3;
	private bool grounded4;
	private Vector3 shadowPos;

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
	}
	void Awake(){
		shadowPos = shadow.transform.position;
	}

}
