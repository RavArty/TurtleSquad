using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GAF.Assets;

public class Spawner : MonoBehaviour
{
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 2f;		// The amount of time before spawning starts.
	public bool initSpawnLions = false;

	public int maxLionsPerScene = 100;

	public GameObject[] enemies;		// Array of enemy prefabs.

	private GameObject go;
	private bool show = true;
	private int maxLionsPerSceneCounter = 0;
	
	
	private int i = 2;

	void Start(){
		if(initSpawnLions){
			InvokeLions();
		}
	}
	
	public void InvokeLions ()
	{
		InvokeRepeating("Spawn", spawnDelay, spawnTime);

	}
	
	void OnTriggerEnter2D (Collider2D other){

		if (other.transform.tag ==  "green" || other.transform.tag ==  "blue" ||  other.transform.tag ==  "Obstacle" 
			|| other.transform.tag ==  "stone" ){
			show = false;
		}else{show = true;}
	}
	
	void OnTriggerExit2D (Collider2D other){

		if (other.transform.tag ==  "green" || other.transform.tag ==  "blue" || other.transform.tag ==  "Obstacle"
			|| other.transform.tag ==  "stone" ){
			show = true;
		}else{
			//	show = false;
		}
	}
	
	public void StopSpawn(){
		CancelInvoke("Spawn");
	}
	void Update(){
		if(maxLionsPerScene == maxLionsPerSceneCounter){
			StopSpawn();
		}
		
	}
	
	void Spawn ()
	{
		i += 1;
		if(show){
			
			// Instantiate a random enemy.
			GameObject[] createdEnemies = GameObject.FindGameObjectsWithTag("lion");
				int enemyIndex = Random.Range(0, enemies.Length);
		    go = ObjectPool.current.GetObject(enemies[enemyIndex]);
			go.transform.position = new Vector3(transform.position.x, transform.position.y, -9);
			go.transform.rotation = transform.rotation;
			go.SetActive(true);
			go.GetComponent<Enemy>().startPosition = go.transform.position;
			go.GetComponent<Enemy>().stopWalking = true;
			go.GetComponent<Enemy>().appear = true;
			go.GetComponent<Animator>().SetTrigger("Appear");
			StartCoroutine(FireWalking());
				maxLionsPerSceneCounter += 1;
				
				if(go.transform.localScale.x == -1){
					Flip();
				}
			
		}
	}
	IEnumerator FireWalking(){
		yield return new WaitForSeconds(0.5f);
		ControlLionsLayerOrder.instance.ChangeOrder(go);
		go.GetComponent<Enemy>().AfterAppearance();
	}
	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		go.transform.localScale = enemyScale;
	}
}
