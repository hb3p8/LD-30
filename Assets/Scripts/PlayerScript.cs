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
	private float VelocityDempingTreshhold;
	private float VelocityDempingLowSpeed;

	private float AngularVelocityDamping;
	private float AngularDempingTreshhold;
	private float AngularyDempingLowSpeed;
	// Constants end

	private float lastAngularVelocity;
	private Vector2 lastVeclocity;

	private GameObject playerTurret;

	private List<GameObject> engines = new List<GameObject>();

	// Use this for initialization
	void Start () {

		GravityVec.x = 0.0f;
		GravityVec.y = -0.2f;

		Speed = 0.75f;
		AngularSpeed = 12.0f;
		
		MaxVelocity = 14.0f;

		MaxAngularVelocity = 150.0f;
		
		VelocityDemping = 0.2f;
		VelocityDempingTreshhold = 1.5f;
		VelocityDempingLowSpeed = 0.04f;
		
		AngularVelocityDamping = 8.0f;
		AngularDempingTreshhold = 50.0f;
		AngularyDempingLowSpeed = 0.0255f;
		
		lastAngularVelocity = 0.0f;
		lastVeclocity.x = 0.0f;
		lastVeclocity.y = 0.0f;

		playerTurret = GameObject.Find ("turret");

		engines.Add (GameObject.Find ("Engine1"));
		engines.Add (GameObject.Find ("Engine2"));
		engines.Add (GameObject.Find ("Engine3"));
	}
	
	// Update is called once per frame
	void Update ()
	{

		//Vector3 mousePos = Input.mousePosition;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		Vector3 mousePos = ray.origin;
		Vector3 currentPos = playerTurret.transform.position;//new Vector3 (Screen.width / 2, Screen.height / 2);
		//Debug.Log (mousePos.x);
		//Debug.Log (currentPos.x);
		//Debug.Log ("--------------------");

		float x = currentPos.x - mousePos.x;
		float y = currentPos.y - mousePos.y;
		float radians = Mathf.Atan2 (y, x);
		radians += Mathf.PI / 2f;
		
		playerTurret.transform.rotation = Quaternion.AngleAxis (radians / Mathf.PI * 180f, new Vector3 (0f, 0f, 1f));
	}
	
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (moveVertical < 0.0f)
		{
			moveVertical  = 0.0f;
		}

		if( moveVertical > 0.0f)
		{
			for( int i = 0; i < engines.Count ; i++ )
			{
				engines[i].particleEmitter.emit = true;
			}
		}
		else
		{
			for( int i = 0; i < engines.Count ; i++ )
			{
				engines[i].particleEmitter.emit = false;
			}
		}


		rigidbody2D.velocity += GravityVec;

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

		if( velocityMagnitude > VelocityDempingTreshhold )
		{
			rigidbody2D.velocity -= VelocityDemping * rigidbody2D.velocity.normalized;
		}
		else if( velocityMagnitude > 0.0f && velocityMagnitude < VelocityDempingTreshhold )
		{
			rigidbody2D.velocity *= 1.0f - VelocityDempingLowSpeed;
		}

		// AngularVelocity Damping
		if( Mathf.Abs( rigidbody2D.angularVelocity ) > AngularDempingTreshhold )
		{
			rigidbody2D.angularVelocity -= AngularVelocityDamping * Mathf.Sign(rigidbody2D.angularVelocity);
		}
		else if( Mathf.Abs( rigidbody2D.angularVelocity ) > 0.0f && Mathf.Abs( rigidbody2D.angularVelocity ) < AngularDempingTreshhold )
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
}
