using UnityEngine;
using System.Collections;


public class GameControllerScript : MonoBehaviour {

	// Global variables
	static public float Bottom = 10.0f;
	static public float Up = 1000.0f;
	static public float Left = 23.0f;
	static public float Right = 105.0f;

	static public Vector2 GlobalTarget;


	// Global variables end

	public GameObject AsteroidPrefab;

    void Start() {
		GameObject asteroidContainer = new GameObject ("Asteroids");

		int gridX = 9;
		int gridY = 6;
		float spacing = 15.0f;
		Vector3 gridOrigin = new Vector3 (0f, 40f, 0.0f);
		for (int y = 0; y < gridY; y++) {
			for (int x = 0; x < gridX; x++) {
				Vector3 pos = gridOrigin // grid offset
					+ new Vector3 (x, y, 0.0f) * spacing // strict grid
						+ new Vector3 (Random.Range (-0.3f, 0.3f), Random.Range (-0.3f, 0.3f), 0.0f) * spacing; // random offset
				GameObject aNewObject = (GameObject)Instantiate (AsteroidPrefab, pos, Quaternion.identity);
				aNewObject.transform.parent = asteroidContainer.transform;
			}
		}

		// Define game target position

		GlobalTarget.x = 50.0f;
		GlobalTarget.y = 300.0f;
	}

	// Update is called once per frame
	void Update () {
	
		//GameObject cameraObject = GameObject.Find("MainCamera");
		//Camera camera = (Camera)cameraObject.GetComponent<Camera> ();
//		camera.backgroundColor = Color (1.0f, 0.0f, 0.0f); 
	}
}
