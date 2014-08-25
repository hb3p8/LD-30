using UnityEngine;
using System.Collections;

public class TurretCollision : MonoBehaviour {

	public GameObject Explosion;

	private int HP = 16;
	
	private bool isDestroyed = false;

	void OnTriggerEnter2D (Collider2D other)
	{
		if( other.gameObject.name == "Bul_aza" )
			return;



		HP -= 5;			
		
		if (HP < 0)
		{	
			Vector3 pos = transform.position;
			pos.z -= 1.0f;		
			Instantiate(Explosion, other.transform.position, transform.rotation);
			isDestroyed = true;
		}
	}

	void Update()
	{
		if (isDestroyed)
			UnityEngine.Object.Destroy (this.gameObject);
	}
}
