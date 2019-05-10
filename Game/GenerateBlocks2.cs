using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateBlocks2 : MonoBehaviour {

	public GameObject[] blocks;
	public GameObject coins;
	public GameObject[] spikes;
	public GameObject green;
	public GameObject redLine;

	public List<GameObject> currentBlocks;

	public List<GameObject> currentSpikes;



	//	private float screenWidthInPoints;
	private float screenHeightInPoints;
	private float spawnHeight;
	private bool outOfGenerator = false;
	private float greenHeight;
	private uint countBlocks = 0;
	int i = 0;

	// Use this for initialization
	void Init(){


	}
	void Start () {
		greenHeight = green.GetComponent<CircleCollider2D>().bounds.size.y;
		screenHeightInPoints = 2.0f * Camera.main.orthographicSize;

	}

	
	void FixedUpdate () {


		GenerateRoomIfRequred();
		CheckSpikes();

	}

	void AddBlock(float farhtestRoomEndY)
	{
		//1
		GameObject block = ObjectPool.current.GetObject(blocks[i]);
		i++;
		if(i == blocks.Length){
			i = 0;
		}
		float blockY = farhtestRoomEndY - greenHeight*2;
		block.transform.position = new Vector2(0, blockY);
		block.SetActive(true);
		countBlocks++;
		generateCoins(block);
		currentBlocks.Add(block);

	} 

	void CheckSpikes(){
		List<GameObject> spikesToRemove = new List<GameObject>();
		//	GameObject [] Coins = GameObject.FindGameObjectsWithTag ("Coins");
		float playerY = transform.position.y;
		float removeBlocks = redLine.transform.position.y;        

		foreach(var spike in currentSpikes){
			if(spike != null){
				if (spike.transform.position.y > removeBlocks){
					spikesToRemove.Add(spike); 
				}
			}
		}
		foreach(var spike in spikesToRemove)
		{
			currentSpikes.Remove(spike);
			ObjectPool.current.PoolObject (spike);     
		}
		spikesToRemove.Clear();


	}

	void GenerateRoomIfRequred()
	{
		//1
		List<GameObject> blocksToRemove = new List<GameObject>();
		bool addBlocksIndicator = true;        

		//3
		float playerY = transform.position.y;

		//4
		float removeBlocks = redLine.transform.position.y; 

		//5
		float addBlocks = playerY - screenHeightInPoints;

		//6
		float farhtestCameraEndY = 0;
		float lastBlockEndY = 10.0f;

		foreach(var block in currentBlocks)
		{
			if (block.transform.position.y < addBlocks)
				addBlocksIndicator = false;

			//9
			if (block.transform.position.y > removeBlocks)
				blocksToRemove.Add(block);

			lastBlockEndY = Mathf.Min(lastBlockEndY, block.transform.position.y);
		}

		foreach(var block in blocksToRemove)
		{
			currentBlocks.Remove(block);
			ObjectPool.current.PoolObject (block);
          
		}
		if (addBlocksIndicator) {
			AddBlock (lastBlockEndY);
		}
		blocksToRemove.Clear();
	}
	
	void generateCoins(GameObject block){
		List<GameObject> childBlocks = new List<GameObject>();

		int randomSpikeIndex = Random.Range(0, spikes.Length);
		int randomSpikeIndex2 = Random.Range(0, spikes.Length);

		//Debug.Log ("randomCoinIndex " + randomCoinIndex);
		for(int i = 0; i< block.transform.childCount; i++){
			if (block.transform.GetChild (i).name == "stoneX") {
				GameObject spike = ObjectPool.current.GetObject (spikes [randomSpikeIndex]);
				spike.transform.position = new Vector2 (block.transform.GetChild (i).position.x, block.transform.position.y + 3.6f);
				spike.SetActive (true);
				currentSpikes.Add (spike);
			}
		}

		for(int i = 0; i< block.transform.childCount; i++){
			if (block.transform.GetChild (i).name == "stoneY") {
				GameObject spike = ObjectPool.current.GetObject (spikes [randomSpikeIndex2]);
				spike.transform.position = new Vector2 (block.transform.GetChild (i).position.x, block.transform.position.y + 3.6f);
				spike.SetActive (true);
				currentSpikes.Add (spike);
			}
		}
		if(block.name != "Fourth2"){
			int chanceOfAppear = Random.Range(0, 3);
			foreach(Transform child in block.transform){
				if(child.name != "SideBlock" && child.name != "SideBlock1" && child.name != "SideBlock2" && child.name != "SideBlock3"){
					if(chanceOfAppear == 0){
						GameObject coin = ObjectPool.current.GetObject(coins);
						coin.transform.position = new Vector2(child.position.x, block.transform.position.y + 3.6f);
						coin.SetActive(true);	
					}
				}
			}
		}


	}
	

}

