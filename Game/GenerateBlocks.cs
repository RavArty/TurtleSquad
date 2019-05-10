using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateBlocks : MonoBehaviour {

	public static GenerateBlocks instance;

	public GameObject[] blocks;
	public GameObject coins;
	public GameObject green;
	public GameObject redLine;

	
	public List<GameObject> currentBlocks;

	public List<GameObject> currentCoins;


	
	//	private float screenWidthInPoints;
	private float screenHeightInPoints;
	private float spawnHeight;
	private bool outOfGenerator = false;
	private float greenHeight;
	private uint countBlocks = 0;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Init(){
	//	AddBlock(-Camera.main.orthographicSize);

	}
	void Start () {
		greenHeight = green.GetComponent<CircleCollider2D>().bounds.size.y;
		//	float height = 2.0f * Camera.main.orthographicSize;
		//	screenWidthInPoints = height * Camera.main.aspect;
		screenHeightInPoints = 2.0f * Camera.main.orthographicSize;
	//	spawnHeight = Camera.main.orthographicSize;
	//	spawnHeight = currentBlocks[0];


		
	}

	
	void FixedUpdate () {


		GenerateRoomIfRequred();
	//	CheckCoins();

	}
	
	void AddBlock(float farhtestRoomEndY)
	{
		//1
		int randomRoomIndex = Random.Range(0, blocks.Length);
		
		//2
	
		GameObject block = ObjectPool.current.GetObject(blocks[randomRoomIndex]);

		float blockY = farhtestRoomEndY - greenHeight*2;
	//	Debug.Log ("roomCenter: " + roomCenter);
		//5
		block.transform.position = new Vector2(0, blockY);
		countBlocks++;
		generateCoins(block);
	//	SpeedUp(block);
		
		//6
		block.SetActive(true);
	//	Debug.Log ("countBlocks: " + countBlocks); 
		currentBlocks.Add(block);
		
	} 

	void CheckCoins(){
		List<GameObject> coinsToRemove = new List<GameObject>();

		float playerY = transform.position.y;
		float removeBlocks = playerY + screenHeightInPoints;        

	}

	void GenerateRoomIfRequred()
	{
		//1
		List<GameObject> blocksToRemove = new List<GameObject>();
		List<GameObject> coinsToRemove = new List<GameObject>();
		
		//2
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
	//	if(currentBlocks != null){

		
		foreach(var block in currentBlocks)
		{
			if (block.transform.position.y < addBlocks)
				addBlocksIndicator = false;
			
			//9
			if (block.transform.position.y > removeBlocks)
				blocksToRemove.Add(block);


			
			//10
			lastBlockEndY = Mathf.Min(lastBlockEndY, block.transform.position.y);
		
		}

		foreach(var coin in currentCoins)
		{


			if (coin.transform.position.y > removeBlocks)
				coinsToRemove.Add(coin);
		}

		
		//11
		foreach(var block in blocksToRemove)
		{
			currentBlocks.Remove(block);
			ObjectPool.current.PoolObject (block);
		
		}

		if (addBlocksIndicator){
			AddBlock(lastBlockEndY);
		}
		blocksToRemove.Clear();

	}
	
	void generateCoins(GameObject block){
		List<GameObject> childBlocks = new List<GameObject>();

			int randomCoinIndex = Random.Range(2, 3);
		//Debug.Log ("randomCoinIndex " + randomCoinIndex);
			if((countBlocks % randomCoinIndex) == 0){
			foreach(Transform child in block.transform){
				childBlocks.Add(child.gameObject);
			}
		
		
			for (int i = 0; i < Random.Range(2, childBlocks.Count - 1); i++){
			//	GameObject coin = (GameObject)Instantiate(coins[0]);
				GameObject coin = ObjectPool.current.GetObject(coins);
				coin.transform.position = new Vector2(childBlocks[i].transform.position.x, block.transform.position.y + 3.6f);
				coin.SetActive(true);
			//	currentCoins.Add(coin);
			}
			childBlocks.Clear();
					
		}	
				
				
			
	}
}

