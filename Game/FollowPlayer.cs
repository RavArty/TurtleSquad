using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject targetObject;
	
	private float distanceToTarget;


	public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.

	public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
	public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
	public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.

	// Use this for initialization
	void Start () {

		distanceToTarget = transform.position.y + targetObject.transform.position.y;
	}

	bool CheckYMargin()
	{
		// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.

		return Mathf.Abs(transform.position.y - targetObject.transform.position.y) > yMargin;

	}
	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.

		float targetY = transform.position.y;
		

		
		// If the player has moved beyond the y margin...
	//	if(Mathf.Abs(transform.position.y - targetObject.transform.position.y) > yMargin){
			// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
		if(targetObject != null){
			if(CheckYMargin()){
				targetY = Mathf.Lerp(transform.position.y, targetObject.transform.position.y - distanceToTarget, ySmooth * Time.deltaTime);
			}
		}

		// Set the camera's position to the target position with the same z component.
		transform.position = new Vector2(transform.position.x, targetY);
	}
}
