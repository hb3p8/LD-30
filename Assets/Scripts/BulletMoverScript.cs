using UnityEngine;
using System.Collections;

public class BulletMoverScript : MonoBehaviour
{
	private float speed; 

	public GameObject explosion;

	void Start ()
	{
		//setParam (10.0f, 0.0f);
	}

	void setParam( float newSpeed, float newRotAngle )
	{
		newRotAngle += 90.0f;
		Quaternion rotQuat = Quaternion.AngleAxis (newRotAngle , new Vector3 (0f, 0f, 1f));
		rigidbody2D.rotation = newRotAngle;
		rigidbody2D.velocity = rotQuat * Vector2.right * newSpeed;
	}

	void FixedUpdate()
	{
		if( (rigidbody2D.position - new Vector2(Camera.main.transform.position.x , Camera.main.transform.position.y)).magnitude > 50.0f )
		{
			Object.Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.name != "destroyer")
		{
			Object.Destroy (this.gameObject);
			Instantiate(explosion, transform.position, transform.rotation);
		}

	}
	
}
