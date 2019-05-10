using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateRooms : MonoBehaviour {

	public GameObject background;
	public List<GameObject> currentRooms;

	private float screenHeightInPoints;

	
	// Use this for initialization
	void Start () {
		screenHeightInPoints = 2.0f * Camera.main.orthographicSize;
	}
	
	void FixedUpdate () {
		if(ObjectPool.current.start)
			GenerateRoomIfRequred();
	}
	
	
	void AddRoom(float farhtestRoomEndY)
	{
		GameObject room = ObjectPool.current.GetObject(background);
		float roomHeight = screenHeightInPoints;
		float roomCenter = farhtestRoomEndY - roomHeight;
		room.transform.position = new Vector2(0, roomCenter);

		room.SetActive (true);
		//6
		currentRooms.Add(room);
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
		float removeRoomY = playerY + 1.5f * screenHeightInPoints;        
		
		//5
		float addRoomY = playerY - screenHeightInPoints;
		
		//6
		float farhtestRoomEndY = 0;

		if(currentRooms.Count == 0){
			farhtestRoomEndY = screenHeightInPoints;
		}else{
//			ObjectPool.current.containerObject;
		foreach(var room in currentRooms)
		{
			float roomStartY = room.transform.position.y + screenHeightInPoints; 
			
            float roomEndY = roomStartY - screenHeightInPoints;  
			if (roomStartY < addRoomY)
				addRooms = false;
			
			//9
			if (roomEndY > removeRoomY)
				roomsToRemove.Add(room);
			
			//10
			farhtestRoomEndY = Mathf.Min(farhtestRoomEndY, roomEndY);
		}
		}
		
		//11
		foreach(var room in roomsToRemove)
		{
			currentRooms.Remove(room);
			ObjectPool.current.PoolObject (room);
		//	Destroy(room);            
		}
		
		//12
		if (addRooms)
			AddRoom(farhtestRoomEndY);
	}
}
