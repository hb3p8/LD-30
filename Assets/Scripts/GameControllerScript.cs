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

	private GameObject playerObject;
	private GameObject cameraObject;
	private Camera camera;

    void Start() {
		GameObject asteroidContainer = new GameObject ("Asteroids");

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
				aNewObject.transform.parent = asteroidContainer.transform;
			}
		}


		// Define game target position

		GlobalTarget.x = 50.0f;
		GlobalTarget.y = 300.0f;

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
			Debug.Log (factor);
			camera.backgroundColor = new Color (0.6f, 0.02f, 0.1f) * (1 - factor) + new Color (0.1f, 0.05f, 0.3f) * factor; 
		}
	}
}
