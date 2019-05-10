using UnityEngine;
using System.Collections;

public class CoroutineUtil : MonoBehaviour {

	public static IEnumerator WaitForRealSeconds(float time){
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time){
			yield return null;
		}
	}
}
