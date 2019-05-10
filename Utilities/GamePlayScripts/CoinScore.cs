// Show number of collected coins

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoinScore : MonoBehaviour {

	private uint coins = 0;


	public void CollectCoin(uint inCoins){

		coins = coins + inCoins;
		gameObject.GetComponent<Text>().text = ": " + coins.ToString();

	}
}
