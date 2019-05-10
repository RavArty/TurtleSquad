using UnityEngine;
using System.Collections;

public class SpawnLine : MonoBehaviour {

	public GameObject line;
	private GameObject spawnLine;
	private GameObject spawnLine2;
	float i = 10;
	float y = 10;
	// Use this for initialization
	void Start () {
	//	for(float i = 10; i > - 11; i-0.5f){

		while(y > - 11){
			y -= 0.5f;
			spawnLine2 = Instantiate(line, line.transform.position, line.transform.rotation) as GameObject;
			spawnLine2.transform.Rotate(0,0,90);
			Vector2 pos2 = spawnLine2.transform.position;
			pos2.x = y;
			pos2.y = 0;
			spawnLine2.transform.position = pos2;
		}

			while(i > - 11){
				i -= 0.5f;
				spawnLine = Instantiate(line, line.transform.position, line.transform.rotation) as GameObject;
				
				Vector2 pos = spawnLine.transform.position;
				pos.y = i;
				spawnLine.transform.position = pos;
			}

	//	Debug.Log("line");
	//	}
	}


}
