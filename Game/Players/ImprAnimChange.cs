using UnityEngine;
using System.Collections;

public class ImprAnimChange : MonoBehaviour {

	public SpriteRenderer ball;
	public Sprite shellBall;

	public void ChangeBall(){
		ball.sprite = shellBall;
	
	}

	public void SetInActive(){
		gameObject.SetActive(false);
	}
}
