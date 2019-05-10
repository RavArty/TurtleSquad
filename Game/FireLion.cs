using UnityEngine;
using System.Collections;

public class FireLion : MonoBehaviour {


	public GameObject shot;
	public Transform  shotSpawn;
	public float speed;
	private GameObject shoot;



	public void Fire(){

	//	AudioSource.PlayClipAtPoint (cannon_fire, Vector3.zero, MusicSound.instance.audioSources [2].volume);
		shoot = Instantiate(shot, shotSpawn.position, shotSpawn.transform.rotation) as GameObject; 
		shoot.transform.GetComponent<Rigidbody2D>().AddForce(shoot.transform.right * speed);

	}
}
