using UnityEngine;
using System.Collections;

public class ParticleAutoDestrShortTimeScript : MonoBehaviour {

	private float lifeTime;
	private float maxLifeTime;
	
	// Use this for initialization
	void Start () {
		maxLifeTime = 0.15f;
	}
	
	// Update is called once per frame
	void Update () {
		
		lifeTime += Time.deltaTime;
		
		if (lifeTime > maxLifeTime)
			Object.Destroy (this.gameObject);
	}

}
