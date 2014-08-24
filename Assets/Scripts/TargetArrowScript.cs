using UnityEngine;
using System.Collections;

public class TargetArrowScript : MonoBehaviour {

	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("destroyer").transform;
		transform.parent = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 target = GameControllerScript.GlobalTarget;
		float x = player.transform.position.x - target.x;
		float y = player.transform.position.y - target.y;
		float radians = Mathf.Atan2 (y, x);
		radians += Mathf.PI / 2f;
		
		transform.rotation = Quaternion.AngleAxis (radians / Mathf.PI * 180f, new Vector3 (0f, 0f, 1f));

/*
		transform.rotation = Quaternion.identity;

		float angle;

		angle = Vector2.Angle( Vector2.up, GameControllerScript.GlobalTarget - new Vector2(transform.position.x, transform.position.y) );

		transform.Rotate (0.0f,0.0f,angle);*/
	}
}
