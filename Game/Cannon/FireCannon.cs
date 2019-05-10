using UnityEngine;
using System.Collections;

public class FireCannon : MonoBehaviour {


	public GameObject shot;
	public Transform  shotSpawn;
	public float speed;
	public int scale = 1;
	private GameObject shoot;
	private AudioSource audio;

	void Awake(){
		audio = GetComponent<AudioSource>();
	}

	public void Fire(){

		if(KeepDataOnPlayMode.instance.isSoundOn){
			audio.Play();
		}
		shoot = ObjectPool.current.GetObject(shot);
		shoot.transform.position = shotSpawn.position;
		shoot.transform.rotation = shotSpawn.transform.rotation;
		shoot.SetActive(true);
	
		shoot.transform.GetComponent<Rigidbody2D>().AddForce(shoot.transform.right * speed * scale);

	}
}
