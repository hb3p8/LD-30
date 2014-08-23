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
		GravityVec.y = -0.00f;
	
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

		Vector3 direction = new Vector3 (0.0f, 1.0f, 0.0f);

		direction = rigidbody2D.transform.rotation * Vector3.right;

		rigidbody2D.angularVelocity -= moveHorizontal;

		Vector2 direction2d = new Vector2(direction.x, direction.y);
		rigidbody2D.velocity += direction2d * moveVertical * Speed;
	}
}
