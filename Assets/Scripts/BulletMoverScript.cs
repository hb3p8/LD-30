using UnityEngine;
using System.Collections;

public class BulletMoverScript : MonoBehaviour
{
	private float speed; 

	public GameObject explosion;

	private bool isDestriyed = false;

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
		if(isDestriyed)
			Object.Destroy (this.gameObject);

		if( (rigidbody2D.position - new Vector2(Camera.main.transform.position.x , Camera.main.transform.position.y)).magnitude > 30.0f )
		{
			Object.Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{

		if (/*other.gameObject.name != "destroyer"*/
		    ( other.gameObject.name == "Asteroid(Clone)" || other.gameObject.name == "TerrainObject"
			||
			name == "Bul_destr" && (other.gameObject.name == "LF_bib" || other.gameObject.name == "LF_aza" || other.gameObject.name == "HF_bib" || other.gameObject.name == "HF_aza"))
		    ||
		    (name == "Bul_bib" && (other.gameObject.name == "Bul_destr" || other.gameObject.name == "LF_aza" || other.gameObject.name == "HF_aza" || other.gameObject.name == "destroyer"))
		    ||
		    (name == "Bul_aza" && (other.gameObject.name == "Bul_destr" || other.gameObject.name == "LF_bib" || other.gameObject.name == "HF_bib" || other.gameObject.name == "destroyer"))
		    )
		{
			isDestriyed = true;
			Instantiate(explosion, transform.position, transform.rotation);
		}


	}
	
}
