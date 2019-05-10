using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class StarsEffect : MonoBehaviour
{
		/// <summary>
		/// The stars effect prefab.
		/// </summary>
		public GameObject starsEffectPrefab;

		/// <summary>
		/// Create the stars effect.
		/// </summary>
		public void CreateStarsEffect ()
		{
				GameObject starsEffect = Instantiate (starsEffectPrefab, transform.position, Quaternion.identity) as GameObject;
		//		starsEffect.transform.parent = transform;//setting up Stars Effect Parent
		}

}