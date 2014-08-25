using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

	// Gun constants
	private float nextFire;	
	public GameObject Shot;
	private float laserShotVelocity = 20.0f;
	private float fireRate = 1.5f;

	private GameObject player;

	// Use this for initialization
	void Start () {
	
		player = GameObject.Find ("destroyer");
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 currentPos = transform.position;

		if ((currentPos - player.transform.position).sqrMagnitude > 900f)
			return;
		
		float x = currentPos.x - player.transform.position.x;
		float y = currentPos.y - player.transform.position.y;
		float radians = Mathf.Atan2 (y, x);
		radians += Mathf.PI / 2f + 0.1f * Mathf.Sin (Time.time);
		
		transform.rotation = Quaternion.AngleAxis (radians / Mathf.PI * 180f, new Vector3 (0f, 0f, 1f));
		

		if (Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate + Random.Range (0f, 1f);
			
			Vector3 shotSpawnPos = transform.position;
			shotSpawnPos.z += 0.05f;
			
			UnityEngine.Object shot = Instantiate (Shot, shotSpawnPos,
			                                           transform.rotation * (Quaternion.AngleAxis(90.0f, new Vector3(0.0f,0.0f,1.0f))));

			shot.name = "Bul_aza";
			Vector3 turretDirection3d = transform.rotation * Vector3.up;
			Vector2 turretDirection2d = new Vector2 (turretDirection3d.x, turretDirection3d.y);
			
			((GameObject)shot).rigidbody2D.velocity = laserShotVelocity * turretDirection2d;
		}
	}
}
