using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjectShake : MonoBehaviour {

	public float smooth = 2.0F;
	public float tiltAngle = 30.0F;
	public GameObject cannon_base;
	public float speed = 1;
	public float shake_decay_start = 0.002f;
	public float shake_intensity_start = 0.03f;

	private float shake_decay;
	private float shake_intensity;

	private Vector3 originPosition;
	private Quaternion originRotation;
	private bool shaking;
	private Transform transformAtOrigin;

	void OnEnable() {
		transformAtOrigin = cannon_base.transform;
	}


	void Update () {
		
		if(!shaking)
			return;
		if (shake_intensity > 0f){
			cannon_base.transform.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;
			cannon_base.transform.localRotation = new Quaternion(
				cannon_base.transform.localRotation.x + Random.Range (-shake_intensity,shake_intensity) * .2f,
				cannon_base.transform.localRotation.y + Random.Range (-shake_intensity,shake_intensity) * .2f,
				cannon_base.transform.localRotation.z + Random.Range (-shake_intensity*5,shake_intensity*5) * .2f,
				cannon_base.transform.localRotation.w + Random.Range (-shake_intensity,shake_intensity) * .2f);
			shake_intensity -= shake_decay;
		} else {
			shaking = false;
			transformAtOrigin.localPosition = originPosition;
			transformAtOrigin.localRotation = originRotation;
		}
	}

	public void Shake() {
		if(!shaking) {
			originPosition = transformAtOrigin.localPosition;
			originRotation = transformAtOrigin.localRotation;
		}
		shaking = true;
		shake_decay = shake_decay_start;
		shake_intensity = shake_intensity_start;
	}

	public void Ricochet(){
		shaking = true;
	
		shake_decay = shake_decay_start;
		shake_intensity = shake_intensity_start;
		StartCoroutine(GoBack());

	}
	public void RicochetBack(){
		shaking = true;

		shake_decay = shake_decay_start;
		shake_intensity = shake_intensity_start;
		StartCoroutine(GoForward());
	}

	IEnumerator GoBack() { 
		Quaternion target = Quaternion.Euler(0, 0, 5);
		for(float i = 0; i < 1.0f; i += Time.deltaTime / smooth){
			
			cannon_base.transform.rotation = Quaternion.Slerp(cannon_base.transform.rotation, target, i);
			yield return null;
		}
	}

	IEnumerator GoForward() { 
		Quaternion target = Quaternion.Euler(0, 0, 0);
		for(float i = 0; i < 1.0f; i += Time.deltaTime / smooth){

			cannon_base.transform.rotation = Quaternion.Slerp(cannon_base.transform.rotation, target, i);
			yield return null;
		}
	}

}