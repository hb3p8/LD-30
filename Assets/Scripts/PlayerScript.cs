using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerScript : MonoBehaviour {

	// Constants
	private Vector2 GravityVec;
	private float Speed;
	private float AngularSpeed;
	private float MaxVelocity;
	private float MaxAngularVelocity;

	private float VelocityDemping;
	private float VelocityDempingThreshold;
	private float VelocityDempingLowSpeed;

	private float AngularVelocityDamping;
	private float AngularDempingThreshold;
	private float AngularyDempingLowSpeed;
	// Constants end

	private float lastAngularVelocity;
	private Vector2 lastVeclocity;

	private GameObject playerTurret;

	private List<GameObject> engines = new List<GameObject>();

	private float GravityMagnitude = 0.4f;

	private int HP;
	private int MaxHP = 100;

	public int GetHP()
	{
		return HP;
	}

	private int laserDamage = 10;
	private float physicalDamage = 8.1f;
	private float damageMinVelosity = 2.8f;

	private float FireRate;
	private float lastFireTime;

	// Gun constants
	private float nextFire;	
	public GameObject Shot;
	private float laserShotVelocity = 32.0f;
	private float fireRate = 0.1f;

	private bool isDead = false;
	private int crazyC = 2;

	public GameObject Explosion;

	public GameObject Engine;
	// Use this for initialization
	void Start () {

		GravityVec.x = 0.0f;
		GravityVec.y = -GravityMagnitude;

		Speed = 0.75f;
		AngularSpeed = 12.0f;
		
		MaxVelocity = 14.0f;

		MaxAngularVelocity = 150.0f;
		
		VelocityDemping = 0.2f;
		VelocityDempingThreshold = 1.5f;
		VelocityDempingLowSpeed = 0.04f;
		
		AngularVelocityDamping = 8.0f;
		AngularDempingThreshold = 50.0f;
		AngularyDempingLowSpeed = 0.0255f;
		
		lastAngularVelocity = 0.0f;
		lastVeclocity.x = 0.0f;
		lastVeclocity.y = 0.0f;

		playerTurret = GameObject.Find ("turret");

		for (int x = 0; x < 3; ++x)
		{
			Vector3 pos = transform.position;
			pos.z -= 1.0f;
			pos.y -= 0.5f;
			pos.x += 0.25f * (x - 1);
			
			GameObject engine = (GameObject)Instantiate(Engine, pos, Quaternion.identity);
			engine.transform.parent = transform;
			engines.Add (engine);
		}

		HP = MaxHP;


	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isDead && crazyC > 0)
		{
			Vector3 pos = transform.position;
			pos.z -= 1.0f;
			
			Instantiate(Explosion, pos, transform.rotation);
			crazyC--;
			return;
		}
		else if(isDead)
		{
			rigidbody2D.velocity += GravityVec * 0.1f;
			return;
		}

		const float halfDist = 300.0f;
		if (transform.position.y < halfDist)
		{
			float factor = Mathf.Clamp01 (transform.position.y / halfDist);
			GravityVec.y = -GravityMagnitude * (1 - factor); 
		}
		else
		{
			float factor = Mathf.Clamp01 (2.0f - transform.position.y / halfDist);
			GravityVec.y = GravityMagnitude * (1 - factor); 
		}

		//Vector3 mousePos = Input.mousePosition;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Vector3 mousePos = ray.origin;
		Vector3 currentPos = playerTurret.transform.position;//new Vector3 (Screen.width / 2, Screen.height / 2);

		float x = currentPos.x - mousePos.x;
		float y = currentPos.y - mousePos.y;
		float radians = Mathf.Atan2 (y, x);
		radians += Mathf.PI / 2f;
		
		playerTurret.transform.rotation = Quaternion.AngleAxis (radians / Mathf.PI * 180f, new Vector3 (0f, 0f, 1f));



		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;

			Vector3 shotSpawnPos = playerTurret.transform.position;
			shotSpawnPos.z += 0.05f;

			UnityEngine.Object temp_shot = Instantiate(Shot, shotSpawnPos,
			                                           playerTurret.transform.rotation * (Quaternion.AngleAxis(90.0f, new Vector3(0.0f,0.0f,1.0f))));

			Vector3 turretDirection3 = playerTurret.transform.rotation * Vector3.up;
			Vector2 turretDirection2 = new Vector2(turretDirection3.x, turretDirection3.y);

			((GameObject)temp_shot).rigidbody2D.velocity = /*rigidbody2D.velocity + */laserShotVelocity*turretDirection2;
		}

	}
	
	void FixedUpdate()
	{
		if (isDead)
		{
			return;		
		}

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (moveVertical < 0.0f)
		{
			moveVertical  = 0.0f;
		}

		if( moveVertical > 0.0f)
		{
			//for( int i = 0; i < engines.Count ; i++ )
			//{
				//engines[i].particleEmitter.emit = true;
				//engines[i].particleSystem.particleEmitter.emit = true;
			//}
		}
		else
		{
			for( int i = 0; i < engines.Count ; i++ )
			{
				//engines[i].particleEmitter.emit = false;
				engines[i].transform.GetChild(0).particleSystem.Clear();
				engines[i].transform.GetChild(1).particleSystem.Clear();
			}
		}


		rigidbody2D.velocity += GravityVec;
		//Debug.Log (rigidbody2D.velocity.y);

		Vector3 direction = new Vector3 (0.0f, 1.0f, 0.0f);

		direction = rigidbody2D.transform.rotation * Vector3.up;

		rigidbody2D.angularVelocity -= moveHorizontal * AngularSpeed;

		Vector2 direction2d = new Vector2(direction.x, direction.y);
		rigidbody2D.velocity += direction2d * moveVertical * Speed;

		// Check Velocity
		if( rigidbody2D.velocity.magnitude > MaxVelocity )
		{
			//rigidbody2D.velocity = lastVeclocity;
			rigidbody2D.velocity = rigidbody2D.velocity.normalized * MaxVelocity;

		}

		// Check AngukarVelocity
		if( Mathf.Abs(rigidbody2D.angularVelocity) > MaxAngularVelocity )
		{
			//rigidbody2D.angularVelocity = lastAngularVelocity;
			rigidbody2D.angularVelocity = Mathf.Min( Mathf.Abs(rigidbody2D.angularVelocity), MaxAngularVelocity ) * Mathf.Sign(rigidbody2D.angularVelocity);
		}

		//Velocity Damping
		float velocityMagnitude = rigidbody2D.velocity.magnitude;

		if( velocityMagnitude > VelocityDempingThreshold )
		{
			rigidbody2D.velocity -= VelocityDemping * rigidbody2D.velocity.normalized;
		}
		else if( velocityMagnitude > 0.0f && velocityMagnitude < VelocityDempingThreshold )
		{
			rigidbody2D.velocity *= 1.0f - VelocityDempingLowSpeed;
		}

		// AngularVelocity Damping
		if( Mathf.Abs( rigidbody2D.angularVelocity ) > AngularDempingThreshold )
		{
			rigidbody2D.angularVelocity -= AngularVelocityDamping * Mathf.Sign(rigidbody2D.angularVelocity);
		}
		else if( Mathf.Abs( rigidbody2D.angularVelocity ) > 0.0f && Mathf.Abs( rigidbody2D.angularVelocity ) < AngularDempingThreshold )
		{
			rigidbody2D.angularVelocity *= 1.0f - AngularyDempingLowSpeed;
		}

		// Coordinations looping

		//Debug.Log(Camera.main.WorldToScreenPoint(GameObject.Find("Main Camera").transform.position).x);

		//Debug.Log(Camera.main.orthographicSize);

		if( rigidbody2D.transform.position.x > GameControllerScript.Right + Camera.main.orthographicSize*2.0f + 3.0f )
		{
			rigidbody2D.transform.position = new Vector2( GameControllerScript.Left - Camera.main.orthographicSize*2.0f - 3.0f, rigidbody2D.transform.position.y );
		}
		else if( rigidbody2D.transform.position.x < GameControllerScript.Left - Camera.main.orthographicSize*2.0f - 3.0f )
		{
			rigidbody2D.transform.position = new Vector2( GameControllerScript.Right + Camera.main.orthographicSize*2.0f + 3.0f, rigidbody2D.transform.position.y );
		}

 		lastAngularVelocity = rigidbody2D.angularVelocity;
 		lastVeclocity = rigidbody2D.velocity;

	}

	void CheckHP()
	{
		Debug.Log (HP);

		if (HP < 0)
		{
			Debug.Log("player is dead");

			Vector3 pos = transform.position;
			pos.z -= 1.0f;

			Instantiate(Explosion, pos, transform.rotation);

			isDead = true;
			// stop engines
			for( int i = 0; i < engines.Count ; i++ )
			{
				engines[i].transform.GetChild(0).particleSystem.Stop();
				engines[i].transform.GetChild(1).particleSystem.Stop();
			}

			audio.Play();

			GameControllerScript.IsFailed = true;
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		//Debug.Log( lastVeclocity.magnitude );

		if(other.relativeVelocity.magnitude < damageMinVelosity)
			return;

		//Debug.Log ("physical hit");
		HP -= Mathf.Min( (int)(physicalDamage * other.relativeVelocity.magnitude) , 101 );
		CheckHP ();

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if( other.gameObject.name == "redLaserRay(Clone)" )
			return;

		//Debug.Log ("laser hit");
		HP -= laserDamage;
		CheckHP();
	}




}
