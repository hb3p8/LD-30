using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public Vector2 GravityVec;

	public float Speed;

	public float AngularSpeed;

	public float MaxVelocity;

	public float MaxAngularVelocity;

	// Use this for initialization
	void Start () {

		GravityVec.x = 0.0f;
		GravityVec.y = -0.2f;
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		rigidbody2D.velocity += GravityVec;

		//gameObject.transform.Rotate(0.0f,0.0f,-3.0f*moveHorizontal);

		rigidbody2D.angularVelocity -= moveHorizontal;

		Vector2 movement = new Vector2 (0.0f, moveVertical);
		rigidbody2D.velocity += movement * Speed;
	}
}
