using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {

		player = GameObject.Find ("destroyer");
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3( player.transform.position.x, player.transform.position.y,  -10.0f);
	
	}
}
