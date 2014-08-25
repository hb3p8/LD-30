using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameControllerScript : MonoBehaviour {

	// Global variables
	static public float Bottom = 10.0f;
	static public float Up = 1000.0f;
	static public float Left = 23.0f;
	static public float Right = 105.0f;

	static public Vector3 GlobalTarget;

	static public bool IsFailed = false;
	static public bool IsWin = false;

	static public List<GameObject> biboraLightFighters = new List<GameObject>();
	static public List<GameObject> azamathLightFighters = new List<GameObject>();

	static public List<GameObject> biboraHeavyFighters = new List<GameObject>();
	static public List<GameObject> azamathHeavyFighters = new List<GameObject>();

	// Global variables end

	private int MaxLightFighters = 30;
	private int MaxHeavyFighters = 3;

	public GameObject AsteroidPrefab;
	public GameObject PlanetPrefab;
	public GameObject TurretPrefab;

	public GameObject lightFighterPrefab;
	public GameObject heavyFighterPrefab;

	private float nextSpawn;	
	private float spawnRate = 3.5f;

	private GameObject playerObject;
	private GameObject cameraObject;
	private Camera camera;

    void Start() {
		IsFailed = false;
		IsWin = false;
		GameObject asteroidContainer = new GameObject ("Asteroids");

		{
			int gridX = 7;
			int gridY = 6;
			float spacing = 20.0f;
			Vector3 gridOrigin = new Vector3 (0f, 80f, 0.0f);
			for (int y = 0; y < gridY; y++) {
				for (int x = 0; x < gridX; x++) {
					Vector3 pos = gridOrigin // grid offset
						+ new Vector3 (x, y, 0.0f) * spacing // strict grid
							+ new Vector3 (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), 0.0f) * spacing; // random offset
					GameObject aNewObject = (GameObject)Instantiate (AsteroidPrefab, pos, Quaternion.identity);
					aNewObject.rigidbody2D.angularVelocity = Random.Range (-30.5f, 30.5f);
					aNewObject.rigidbody2D.velocity = new Vector3 (Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f));
					AsteroidScript asteroid = aNewObject.GetComponent<AsteroidScript>();
					float brightnessBoost = Random.Range (-0.1f, 0f);
					asteroid.TerrainColor += new Color (brightnessBoost, brightnessBoost, brightnessBoost);
					asteroid.TerrainColor.r = asteroid.TerrainColor.r + Random.Range (0.0f, 0.05f);

					aNewObject.transform.parent = asteroidContainer.transform;
				}
			}
		}

		{
			int gridX = 5;
			int gridY = 7;
			float spacing = 33.0f;
			Vector3 gridOrigin = new Vector3 (0f, 220f, 0.0f);
			for (int y = 0; y < gridY; y++) {
				for (int x = 0; x < gridX; x++) {
					Vector3 pos = gridOrigin // grid offset
						+ new Vector3 (x, y, 0.0f) * spacing // strict grid
							+ new Vector3 (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), 0.0f) * spacing; // random offset
					GameObject aNewObject = (GameObject)Instantiate (AsteroidPrefab, pos, Quaternion.identity);
					aNewObject.rigidbody2D.angularVelocity = Random.Range (-30.5f, 30.5f);
					aNewObject.rigidbody2D.velocity = new Vector3 (Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f));
					AsteroidScript asteroid = aNewObject.GetComponent<AsteroidScript>();
					float brightnessBoost = Random.Range (-0.15f, 0f);
					asteroid.TerrainColor += new Color (brightnessBoost, brightnessBoost, brightnessBoost);
					asteroid.TerrainColor.r = asteroid.TerrainColor.r + Random.Range (0.0f, 0.05f);
					
					aNewObject.transform.parent = asteroidContainer.transform;

					// place turret
					if (Random.Range (0f, 1f) > 0.5f)
					{
						asteroid.Turret = TurretPrefab;
						asteroid.placeTurret = true;
					}
				}
			}
		}

		{
			int gridX = 3;
			int gridY = 2;
			float spacing = 65.0f;
			Vector3 gridOrigin = new Vector3 (0f, 450, 0.0f);
			for (int y = 0; y < gridY; y++) {
				for (int x = 0; x < gridX; x++) {
					Vector3 pos = gridOrigin // grid offset
						+ new Vector3 (x, y, 0.0f) * spacing // strict grid
							+ new Vector3 (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), 0.0f) * spacing; // random offset
					GameObject aNewObject = (GameObject)Instantiate (AsteroidPrefab, pos, Quaternion.identity);
					aNewObject.rigidbody2D.angularVelocity = Random.Range (-30.5f, 30.5f);
					aNewObject.rigidbody2D.velocity = new Vector3 (Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f), Random.Range (-0.1f, 0.1f));
					AsteroidScript asteroid = aNewObject.GetComponent<AsteroidScript>();
					float brightnessBoost = Random.Range (-0.15f, 0f);
					asteroid.TerrainColor += new Color (brightnessBoost, brightnessBoost, brightnessBoost);
					asteroid.TerrainColor.r = asteroid.TerrainColor.r + Random.Range (0.0f, 0.05f);
					
					aNewObject.transform.parent = asteroidContainer.transform;
				}
			}
		}



		// another planet
		GameObject aNewPlanet = (GameObject)Instantiate (PlanetPrefab, new Vector3 (128f, 600f, 0f), Quaternion.AngleAxis (180, new Vector3 (0f, 0f, 1f)));

		// Define game target position

		GlobalTarget.x = Random.Range (20f, 108f);
		GlobalTarget.y = 600.0f;


		playerObject = GameObject.Find ("destroyer");
		cameraObject = GameObject.Find ("Main Camera");
		camera = (Camera)cameraObject.GetComponent<Camera> ();

		//Spawn Heavy Fighters
		UnityEngine.Object tempFighter;

		for( int i = 0 ; i < MaxHeavyFighters; i++)
		{
			Vector2 startVec = new Vector2(-0.0f, 0.0f);
			Vector3 startPos = new Vector3( Random.Range(-5.0f, 130.0f)  , Random.Range(440.0f, 560.0f) , -2.0f); 

			tempFighter = Instantiate(heavyFighterPrefab, startPos,
                                           (Quaternion.AngleAxis(180.0f, new Vector3(0.0f,0.0f,1.0f))));

			tempFighter.name = "HF_bib";
			((HeavyFighterScript)(((GameObject)tempFighter).GetComponent<HeavyFighterScript>())).SetVelocity(startVec);

			biboraHeavyFighters.Add((GameObject)tempFighter);

		}

		for( int i = 0 ; i < MaxHeavyFighters; i++)
		{
			Vector2 startVec = new Vector2(0.0f, 0.0f);
			Vector3 startPos = new Vector3( Random.Range(-5.0f, 130.0f)  , Random.Range(440.0f, 560.0f) , -2.0f); 

			tempFighter = Instantiate(heavyFighterPrefab, startPos,
                                           (Quaternion.AngleAxis(0.0f, new Vector3(0.0f,0.0f,1.0f))));

			tempFighter.name = "HF_aza";
			((HeavyFighterScript)(((GameObject)tempFighter).GetComponent<HeavyFighterScript>())).SetVelocity(startVec);

			azamathHeavyFighters.Add((GameObject)tempFighter);

		}



	}

	// Update is called once per frame
	void Update () {
		const float halfDist = 300.0f;
		if (playerObject.transform.position.y < halfDist)
		{
			float factor = Mathf.Clamp01 (playerObject.transform.position.y / halfDist);
			camera.backgroundColor = new Color (0.5f, 0.9f, 0.95f) * (1 - factor) + new Color (0.1f, 0.05f, 0.3f) * factor; 
		}
		else
		{
			float factor = Mathf.Clamp01 (2.0f - playerObject.transform.position.y / halfDist);
			camera.backgroundColor = new Color (0.6f, 0.02f, 0.1f) * (1 - factor) + new Color (0.1f, 0.05f, 0.3f) * factor; 
		}

		if ((playerObject.transform.position - GlobalTarget).magnitude < 1.8f)
		{
			if (playerObject.rigidbody2D.velocity.magnitude < 0.1f)
			{
				if (Mathf.Abs (Vector3.Dot (playerObject.transform.up, new Vector3 (1f, 0f, 0f))) < 0.1f)
				{
					IsWin = true;
				}
			}
		}

		// Control enemies population

		/*
		 * 
		 * 	static public List<GameObject> biboraLightFighters = new List<GameObject>();
	static public List<GameObject> azamathLightFighters = new List<GameObject>();

	static public List<GameObject> biboraHeavyFighters = new List<GameObject>();
	static public List<GameObject> azamathHeavyFighters = new List<GameObject>();
		 * */

		// reanimate light enemies
		UnityEngine.Object tempFighter;

		if ( Time.time > nextSpawn) 
		{
			nextSpawn = Time.time + spawnRate;

			if( biboraLightFighters.Count <=  MaxLightFighters - 3)
			{
				Vector3 startVec = new Vector3(1.5f, 0.0f,0.0f);
				Vector3 startVecPerp = new Vector3(0.0f, 1.5f,0.0f);
				Vector3 startPos = new Vector3( 0.0f , Random.Range(300.0f, 520.0f) , -2.0f);


				float randomAngle = Random.Range(-30.0f, 30.0f);	
				Quaternion rotQuat = Quaternion.AngleAxis(randomAngle, new Vector3(0.0f,0.0f,1.0f));

				startVec = rotQuat * startVec;
				startVecPerp  = rotQuat * startVecPerp;

				tempFighter = Instantiate(lightFighterPrefab, startPos,
	                                           (Quaternion.AngleAxis(180.0f+randomAngle, new Vector3(0.0f,0.0f,1.0f))));
				tempFighter.name = "LF_bib";
				((Rigidbody2D)(((GameObject)tempFighter).GetComponent<Rigidbody2D>())).velocity = startVec*3.0f;
				biboraLightFighters.Add((GameObject)tempFighter);

				tempFighter = Instantiate(lightFighterPrefab, startPos - startVec + startVecPerp,
	                                           (Quaternion.AngleAxis(180.0f+randomAngle, new Vector3(0.0f,0.0f,1.0f))));
				tempFighter.name = "LF_bib";
				((Rigidbody2D)(((GameObject)tempFighter).GetComponent<Rigidbody2D>())).velocity = startVec*3.0f;
				biboraLightFighters.Add((GameObject)tempFighter);


				tempFighter = Instantiate(lightFighterPrefab, startPos - startVec - startVecPerp,
	                                           (Quaternion.AngleAxis(180.0f+randomAngle, new Vector3(0.0f,0.0f,1.0f))));
				tempFighter.name = "LF_bib";
				((Rigidbody2D)(((GameObject)tempFighter).GetComponent<Rigidbody2D>())).velocity = startVec*3.0f;
				biboraLightFighters.Add((GameObject)tempFighter);
			}

			if( azamathLightFighters.Count < MaxLightFighters - 3)
			{
				Vector3 startVec = new Vector3(-1.5f, 0.0f,0.0f);
				Vector3 startVecPerp = new Vector3(0.0f, -1.5f,0.0f);
				Vector3 startPos = new Vector3( 130.0f , Random.Range(300.0f, 520.0f) , -2.0f);


				float randomAngle = Random.Range(-30.0f, 30.0f);	
				Quaternion rotQuat = Quaternion.AngleAxis(randomAngle, new Vector3(0.0f,0.0f,1.0f));

				startVec = rotQuat * startVec;
				startVecPerp  = rotQuat * startVecPerp;

				tempFighter = Instantiate(lightFighterPrefab, startPos,
	                                           (Quaternion.AngleAxis(0.0f+randomAngle, new Vector3(0.0f,0.0f,1.0f))));
				tempFighter.name = "LF_aza";
				((Rigidbody2D)(((GameObject)tempFighter).GetComponent<Rigidbody2D>())).velocity = startVec*3.0f;
				azamathLightFighters.Add((GameObject)tempFighter);

				tempFighter = Instantiate(lightFighterPrefab, startPos - startVec + startVecPerp,
	                                           (Quaternion.AngleAxis(0.0f+randomAngle, new Vector3(0.0f,0.0f,1.0f))));
				tempFighter.name = "LF_aza";
				((Rigidbody2D)(((GameObject)tempFighter).GetComponent<Rigidbody2D>())).velocity = startVec*3.0f;
				azamathLightFighters.Add((GameObject)tempFighter);


				tempFighter = Instantiate(lightFighterPrefab, startPos - startVec - startVecPerp,
	                                           (Quaternion.AngleAxis(0.0f+randomAngle, new Vector3(0.0f,0.0f,1.0f))));
				tempFighter.name = "LF_aza";
				((Rigidbody2D)(((GameObject)tempFighter).GetComponent<Rigidbody2D>())).velocity = startVec*3.0f;
				azamathLightFighters.Add((GameObject)tempFighter);
			}
		}
/*
		if( biboraHeavyFighters.Count <  MaxHeavyFighters )
		{
			Vector2 startVec = new Vector2(-1.0f, 0.0f);
			Vector3 startPos = new Vector3( 130.0f , Random.Range(470.0f, 530.0f) , -14.0f); 

		}

		if( azamathHeavyFighters.Count <  MaxHeavyFighters )
		{
			Vector2 startVec = new Vector2(1.0f, 0.0f);
			Vector3 startPos = new Vector3( -2.0f , Random.Range(470.0f, 530.0f) , -14.0f); 

		}
*/
		// kill enemies
		for(int i = 0;  i < biboraLightFighters.Count; i++)
		{
			if( biboraLightFighters[i] )
			{
				if( biboraLightFighters[i].transform.position.x > 135.0f ||  biboraLightFighters[i].transform.position.x < -7.0f   )
				{
					UnityEngine.Object.Destroy (biboraLightFighters[i]);
					biboraLightFighters.RemoveAt(i);
				}
			}
			else
			{
				biboraLightFighters.RemoveAt(i);
			}

		}

		for(int i = 0;  i < azamathLightFighters.Count; i++)
		{
			if( azamathLightFighters[i] )
			{
				if( azamathLightFighters[i].transform.position.x > 135.0f ||  azamathLightFighters[i].transform.position.x < -7.0f   )
				{
					UnityEngine.Object.Destroy (azamathLightFighters[i]);
					azamathLightFighters.RemoveAt(i);
				}
			}
			else
			{
				azamathLightFighters.RemoveAt(i);
			}
			
		}

/*
		for(int i = 0;  i < biboraHeavyFighters.Count; i++)
		{
			if( biboraHeavyFighters[i] )
			{
				if( biboraHeavyFighters[i].transform.position.x > 135.0f &&  biboraHeavyFighters[i].transform.position.x < -7.0f   )
				{
					UnityEngine.Object.Destroy (biboraHeavyFighters[i]);
					biboraHeavyFighters.RemoveAt(i);
				}
			}
			else
			{
				biboraHeavyFighters.RemoveAt(i);
			}
			
		}

		for(int i = 0;  i < azamathHeavyFighters.Count; i++)
		{
			if( azamathHeavyFighters[i] )
			{
				if( azamathHeavyFighters[i].transform.position.x > 135.0f &&  azamathHeavyFighters[i].transform.position.x < -7.0f   )
				{
					UnityEngine.Object.Destroy (azamathHeavyFighters[i]);
					azamathHeavyFighters.RemoveAt(i);
				}
			}
			else
			{
				azamathHeavyFighters.RemoveAt(i);
			}
			
		}
*/


	}
}
