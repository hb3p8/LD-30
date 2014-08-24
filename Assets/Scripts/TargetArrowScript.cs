using UnityEngine;
using System.Collections;

public class TargetArrowScript : MonoBehaviour {

	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("destroyer").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.parent = Camera.main.transform;

		transform.rotation = Quaternion.identity;

		float angle;

		angle = Vector2.Angle( Vector2.up, GameControllerScript.GlobalTarget - new Vector2(transform.position.x, transform.position.y) );

		transform.Rotate (0.0f,0.0f,angle);
	}
}
