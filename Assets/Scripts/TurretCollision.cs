using UnityEngine;
using System.Collections;

public class TurretCollision : MonoBehaviour {

	public GameObject Explosion;

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log (other.gameObject.name);
		if( other.gameObject.name == "eshot" )
			return;

		Instantiate(Explosion, other.transform.position, transform.rotation);

		UnityEngine.Object.Destroy (this.gameObject);
	}
}
