using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class LionEffect : MonoBehaviour
{

	public GameObject lionsEffectPrefab;


	public void CreateLionsEffect ()
	{
		GameObject lionsEffect = Instantiate (lionsEffectPrefab, transform.position, Quaternion.identity) as GameObject;
	}

}