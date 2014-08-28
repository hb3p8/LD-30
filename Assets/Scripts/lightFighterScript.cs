using UnityEngine;
using System.Collections;

public class lightFighterScript : MonoBehaviour {


	public GameObject explosion;
	public GameObject shot;

	private float nextFire;
	private float fireRate = 1.1f;

	private float laserShotVelocity = 30.0f;

	private int HP = 9;

	private bool isDestroyed = false;

	// Use this for initialization
	void Start () {

		//rigidbody2D.velocity = new Vector2(-5.0f, -1.0f);

		UpdateColor ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time > nextFire &&
		    (rigidbody2D.position - new Vector2(Camera.main.transform.position.x , Camera.main.transform.position.y)).magnitude < 30.0f ) 
		{
			nextFire = Time.time + fireRate;
			
			Vector3 shotSpawnPos = transform.position;
			shotSpawnPos.z += 0.05f;
			
			UnityEngine.Object temp_shot = Instantiate(shot, shotSpawnPos,
			                                           transform.rotation );
			
			Vector3 shotDirection3 = transform.rotation * Vector3.left;
			Vector2 shotDirection2 = new Vector2(shotDirection3.x, shotDirection3.y);
			
			((GameObject)temp_shot).rigidbody2D.velocity = laserShotVelocity*shotDirection2;

			if( name == "LF_bib" )
			{
				((GameObject)temp_shot).name = "Bul_bib";
			}
			else if( name == "LF_aza" )
			{
				((GameObject)temp_shot).name = "Bul_aza";
			}

		}
	
	}

	void FixedUpdate()
	{
		if(isDestroyed)
			Object.Destroy (this.gameObject);
	}


	public void UpdateColor()
	{
		if( name == "LF_bib" )
		{
			GetComponent<SpriteRenderer>().color = new Color(1.0f,0.4f,0.4f);
		}
		else if( name == "LF_aza" )
		{
			GetComponent<SpriteRenderer>().color = new Color(0.4f,0.4f,1.0f);
		}
	}

	void processHit()
	{
		HP -= 5;



		if (HP < 0)
		{	
			Vector3 pos = transform.position;
			pos.z -= 1.0f;		
			
			isDestroyed = true;

			if( (rigidbody2D.position - new Vector2(Camera.main.transform.position.x , Camera.main.transform.position.y)).magnitude < 30.0f )
			{
				Instantiate(explosion, pos, transform.rotation);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{

		if ( other.gameObject.name == "destroyer")
		{
			Instantiate(explosion, transform.position, transform.rotation);
			//Object.Destroy (this.gameObject);
			isDestroyed = true;
		}
		else if( other.gameObject.name == "Bul_bib" && name == "LF_aza" )
		{
			processHit();
		}
		else if( other.gameObject.name == "Bul_aza" && name == "LF_bib" )
		{
			processHit();
		}
		else if( other.gameObject.name == "Bul_destr" )
		{
			//Debug.Log (HP);
			processHit();
		}
		
		
	}

	void OnCollisionEnter2D(Collision2D coll)
	{

		Instantiate(explosion, transform.position, transform.rotation);
		//Object.Destroy (this.gameObject);
		isDestroyed = true;
	}
}
