using UnityEngine;
using System.Collections;

public class HeavyFighterScript : MonoBehaviour {

	
	public GameObject explosion;
	public GameObject shot;
	
	private float nextFire;
	private float fireRate = 0.17f;
	
	private float laserShotVelocity = 27.0f;
	
	private int HP = 1000;
	
	private bool isDestroyed = false;

	private GameObject HFTurret1;
	private GameObject HFTurret2;

	private float maxTurretAngle = 80.0f;
	private float curTurretAngle = 0.0f;
	private float curRotTurretDir = 1.0f;
	private float turretRotStep = 1.0f;

	private Vector2 velocity;
	
	// Use this for initialization
	void Start () {
		
		HFTurret1 = GameObject.Find ("HFturret1");
		HFTurret2 = GameObject.Find ("HFturret2");
		
		UpdateColor ();

		//SetVelocity (new Vector2 (-0.02f, 0.0f));

		//transform.rotation *= Quaternion.AngleAxis(90.0f*2, new Vector3(0.0f,0.0f,1.0f)); 

		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (Time.time > nextFire &&
		    (transform.position - new Vector3(Camera.main.transform.position.x , Camera.main.transform.position.y, 0.0f)).magnitude < 30.0f ) 
		{
			nextFire = Time.time + fireRate;

			Vector3 shotSpawnPos = HFTurret1.transform.position;
			shotSpawnPos.z += 0.05f;

			UnityEngine.Object temp_shot = Instantiate(shot, shotSpawnPos,
			                                           HFTurret1.transform.rotation * (Quaternion.AngleAxis(90.0f+Random.Range(-10.0F, 10.0F), new Vector3(0.0f,0.0f,1.0f))));

			Vector3 turretDirection3 = HFTurret1.transform.rotation * Vector3.up;
			Vector2 turretDirection2 = new Vector2(turretDirection3.x, turretDirection3.y);

			((GameObject)temp_shot).rigidbody2D.velocity = laserShotVelocity*turretDirection2;

			if( name == "HF_bib" )
			{
				((GameObject)temp_shot).name = "Bul_bib";
			}
			else if( name == "HF_aza" )
			{
				((GameObject)temp_shot).name = "Bul_aza";
			}

			shotSpawnPos = HFTurret2.transform.position;
			shotSpawnPos.z += 0.05f;

			temp_shot = Instantiate(shot, shotSpawnPos,
			                                           HFTurret2.transform.rotation * (Quaternion.AngleAxis(90.0f+Random.Range(-10.0F, 10.0F), new Vector3(0.0f,0.0f,1.0f))));

			turretDirection3 = HFTurret2.transform.rotation * Vector3.up;
			turretDirection2 = new Vector2(turretDirection3.x, turretDirection3.y);

			((GameObject)temp_shot).rigidbody2D.velocity = laserShotVelocity*turretDirection2;

			if( name == "HF_bib" )
			{
				((GameObject)temp_shot).name = "Bul_bib";
			}
			else if( name == "HF_aza" )
			{
				((GameObject)temp_shot).name = "Bul_aza";
			}

		}

		//rotate turrets
		HFTurret1.transform.rotation *= (Quaternion.AngleAxis(-turretRotStep* curRotTurretDir, new Vector3(0.0f,0.0f,1.0f)));
		HFTurret2.transform.rotation *= (Quaternion.AngleAxis(turretRotStep * curRotTurretDir, new Vector3(0.0f,0.0f,1.0f)));

		curTurretAngle += turretRotStep * curRotTurretDir;

		if( curTurretAngle > maxTurretAngle || curTurretAngle < -maxTurretAngle)
		{
			curRotTurretDir =-curRotTurretDir;
		}

	}
	
	void FixedUpdate()
	{
		if(isDestroyed)
			Object.Destroy (this.gameObject);

		transform.position = new Vector3(transform.position.x + velocity.x,transform.position.y + velocity.y,transform.position.z);
	}
	
	
	void UpdateColor()
	{
		if( name == "HF_bib" )
		{
			GetComponent<SpriteRenderer>().color = new Color(1.0f,0.1f,0.1f);

			HFTurret1.GetComponent<SpriteRenderer>().color = new Color(2.0f,2.0f,2.0f);
			HFTurret2.GetComponent<SpriteRenderer>().color = new Color(2.0f,2.0f,2.0f);
		}
		else if( name == "HF_aza" )
		{
			GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f,1.0f);
		}
	}

	void SetVelocity( Vector2 newVelocity)
	{
		velocity = newVelocity;
	}
	
	void processHit()
	{
		/*
		HP -= 5;
		
		
		
		if (HP < 0)
		{	
			Vector3 pos = transform.position;
			pos.z -= 1.0f;		
			Instantiate(explosion, pos, transform.rotation);
			isDestroyed = true;
		}
		*/
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		/*
		if ( other.gameObject.name == "destroyer")
		{
			Instantiate(explosion, transform.position, transform.rotation);
			Object.Destroy (this.gameObject);
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
		*/
		
	}
	
	void OnCollisionEnter2D(Collision2D coll)
	{
		/*
		Instantiate(explosion, transform.position, transform.rotation);
		Object.Destroy (this.gameObject);
		*/
	}
}
