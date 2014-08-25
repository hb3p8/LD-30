using UnityEngine;
using System.Collections;

public class BulletMoverScript : MonoBehaviour
{
	private float speed; 

	public GameObject explosion;

	private bool isEnemyShot = false;
	private bool needToDestroy = false; // \m/

	void Start ()
	{
		//setParam (10.0f, 0.0f);
		isEnemyShot = this.gameObject.name == "eshot";
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
		if( needToDestroy || (rigidbody2D.position - new Vector2(Camera.main.transform.position.x , Camera.main.transform.position.y)).magnitude > 50.0f )
		{
			Object.Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		bool isHitWithPlayer = other.gameObject.name == "destroyer";
		bool isHitWithEnemy = other.gameObject.name == "enemyturret";

		if (isHitWithEnemy && isEnemyShot || isHitWithPlayer && !isEnemyShot)
		{
			// do nothing
		}
		else
		{
			needToDestroy = true;
			Instantiate(explosion, transform.position, transform.rotation);
		}

	}
	
}
