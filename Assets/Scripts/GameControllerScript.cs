using UnityEngine;
using System.Collections;


public class GameControllerScript : MonoBehaviour {

	// Global variables
	static public float Bottom = 10.0f;
	static public float Up = 1000.0f;
	static public float Left = 23.0f;
	static public float Right = 105.0f;

	static public Vector3 GlobalTarget;

	static public bool IsFailed = false;
	static public bool IsWin = false;


	// Global variables end

	public GameObject AsteroidPrefab;
	public GameObject PlanetPrefab;
	public GameObject TurretPrefab;

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
	}
}
