using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class Destroy : MonoBehaviour
{
		/// <summary>
		/// Destroy time.
		/// </summary>
		public float time;

		// Use this for initialization
		void Start ()
		{
				///Destroy the current gameobject after n time
			StartCoroutine(SetInactive());
			//	Destroy (gameObject, time);
		}
	IEnumerator SetInactive(){
		yield return new WaitForSeconds(time);
	//	gameObject.SetActive(false);
		ObjectPool.current.PoolObject (gameObject);
	}
		void Update()
		{
			if (Time.timeScale < 0.01f)
			{
				if(gameObject != null){
				//	Debug.Log ("unscaled");
					gameObject.GetComponent<ParticleSystem>().Simulate(Time.unscaledDeltaTime, true, false);
				}
			}
		}
}
