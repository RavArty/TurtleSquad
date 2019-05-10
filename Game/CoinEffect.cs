using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class CoinEffect : MonoBehaviour
{

	public  GameObject coinsEffectPrefab;
	private GameObject coinsEffect;

	public void CreateStarsEffect ()
	{
		
		coinsEffect = ObjectPool.current.GetObject(coinsEffectPrefab);
		coinsEffect.transform.position = transform.position;
		coinsEffect.transform.rotation = Quaternion.identity;
		coinsEffect.SetActive(true);
	}

}