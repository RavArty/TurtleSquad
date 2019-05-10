using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControlFallDown : MonoBehaviour {

	public GameObject[] availableRooms;
	public GameObject[] blocks;

	private float spawnHeight;
	public float[] centerBlock;

	
	public List<GameObject> currentRooms;
	
	//	private float screenWidthInPoints;
	private float screenHeightInPoints;
	private GameObject[] blocksToRemove;
	
	// Use this for initialization
	void Start () {
		
		screenHeightInPoints = 2.0f * Camera.main.orthographicSize;
		spawnHeight = Camera.main.orthographicSize;
		
		
	}
	

	void FixedUpdate () {
	}
	
	
	void AddRoom(float farhtestRoomEndY)
	{
		//1
		int randomRoomIndex = Random.Range(0, availableRooms.Length);
		
		//2
		GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);
		
		
		//3
		//	float roomWidth = room.transform.FindChild("floor").localScale.x;
		float roomHeight = room.transform.Find("floor").localScale.y;
		
		//4
		float roomCenter = farhtestRoomEndY - roomHeight * 0.5f;
		
		//5
		room.transform.position = new Vector3(0, roomCenter, 0);


		

		//6
		currentRooms.Add(room);
		GenerateBlocks();

		
	} 
	
	void GenerateRoomIfRequred()
	{
		//1
		List<GameObject> roomsToRemove = new List<GameObject>();
		
		//2
		bool addRooms = true;        
		
		//3
		float playerY = transform.position.y;
		
		//4
		float removeRoomY = playerY + screenHeightInPoints;        
		
		//5
		float addRoomY = playerY - screenHeightInPoints;
		
		//6
		float farhtestRoomEndY = 0;
		
		foreach(var room in currentRooms)
		{
			//7
			float roomHeight = room.transform.Find("floor").localScale.y;
			float roomStartY = room.transform.position.y + (roomHeight * 0.5f);    
			float roomEndY = roomStartY - roomHeight;                            
			
			//8
			if (roomStartY < addRoomY)
				addRooms = false;
			
			//9
			if (roomEndY > removeRoomY)
				roomsToRemove.Add(room);
				RemoveBlocks();
			
			//10
			farhtestRoomEndY = Mathf.Min(farhtestRoomEndY, roomEndY);
		}
		
		//11
		foreach(var room in roomsToRemove)
		{
			currentRooms.Remove(room);
			Destroy(room);            
		}
		
		//12
		if (addRooms)
		
			AddRoom(farhtestRoomEndY);
	}

	void GenerateBlocks(){
		ShuffleArray(blocks);
		while(spawnHeight > transform.position.y - screenHeightInPoints){
//			Debug.Log ("spawn: " + spawnHeight);
		for (int i = 0; i< Random.Range(3, 7); i++){
			GameObject block = (GameObject)Instantiate(blocks[i]);
		//	string name = block.name;
			block.name = block.name.Replace("(Clone)", "");
		//		Debug.Log ("name: " +name);
			int center = int.Parse(block.name);
//				Debug.Log ("center: " +center);
			block.transform.position = new Vector2(centerBlock[center], spawnHeight);
		}
			spawnHeight -= 5;
		}

	}


	void ShuffleArray(GameObject[] arr){
		for (int i = arr.Length - 1; i > 0; i--) {
			int r = Random.Range(0, i);
			GameObject tmp = arr[i];
			arr[i] = arr[r];
			arr[r] = tmp;
		}

	}
	void RemoveBlocks(){
		blocksToRemove = GameObject.FindGameObjectsWithTag("ground_stone");
		foreach(GameObject blockToRemove in blocksToRemove){
			if(blockToRemove.transform.position.y > transform.position.y - screenHeightInPoints){
				if(blockToRemove != null){
					//Destroy(blockToRemove);
				}
			}
		}
	}
}
