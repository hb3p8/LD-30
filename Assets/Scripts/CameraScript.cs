using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

/*
	private GameObject player;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("destroyer");
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3( player.transform.position.x, player.transform.position.y,  -10.0f);
	
	}
*/


public float xMargin = 1f;		// Distance in the x axis the player can move before the camera follows.
public float yMargin = 1f;		// Distance in the y axis the player can move before the camera follows.
public float xSmooth = 8f;		// How smoothly the camera catches up with it's target movement in the x axis.
public float ySmooth = 8f;		// How smoothly the camera catches up with it's target movement in the y axis.
public Vector2 maxXAndY;		// The maximum x and y coordinates the camera can have.
public Vector2 minXAndY;		// The minimum x and y coordinates the camera can have.


private Transform player;		// Reference to the player's transform.


void Awake ()
{
	// Setting up the reference.
	player = GameObject.Find ("destroyer").transform;

	maxXAndY = new Vector2( GameControllerScript.Right, GameControllerScript.Up );
	minXAndY = new Vector2( GameControllerScript.Left, GameControllerScript.Bottom );

	transform.position = new Vector3( player.transform.position.x, player.transform.position.y,  -10.0f);

}


bool CheckXMargin()
{
	// Returns true if the distance between the camera and the player in the x axis is greater than the x margin.
	return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
}


bool CheckYMargin()
{
	// Returns true if the distance between the camera and the player in the y axis is greater than the y margin.
	return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
}


void FixedUpdate ()
{
	TrackPlayer();
}


void TrackPlayer ()
{
	// By default the target x and y coordinates of the camera are it's current x and y coordinates.
	float targetX = transform.position.x;
	float targetY = transform.position.y;
	
	/*
	// If the player has moved beyond the x margin...
	if(CheckXMargin())
		// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
		targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);
	
	// If the player has moved beyond the y margin...
	if(CheckYMargin())
		// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
		targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
	*/

	transform.position = new Vector3( player.transform.position.x, player.transform.position.y,  -10.0f);

	// The target x and y coordinates should not be larger than the maximum or smaller than the minimum.
	targetX = Mathf.Clamp(player.transform.position.x, minXAndY.x, maxXAndY.x);
	targetY = Mathf.Clamp(player.transform.position.y, minXAndY.y, maxXAndY.y);

	//Debug.Log(player.transform.position.x);
	//Debug.Log(player.transform.position.y);
	//Debug.Log("-----------------------");

	
	// Set the camera's position to the target position with the same z component.
	transform.position = new Vector3(targetX, targetY, transform.position.z);
}

}
