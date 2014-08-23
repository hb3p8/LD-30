using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AsteroidScript : MonoBehaviour {
	
	public bool BuildCollider = true;
	public Color TerrainColor = new Color (0.21f, 0.21f, 0.21f);
	public float Height = 6.0f;
	public int Steps = 16;
	public float Roughness = 0.7f;
	
	// This first list contains every vertex of the mesh that we are going to render
	private List<Vector3> terrainVertices = new List<Vector3>();
	
	private List<Color> terrainColors = new List<Color>();
	
	// The triangles tell Unity how to build each section of the mesh joining
	// the vertices
	private List<int> terrainTriangles = new List<int>();
	
	// The UV list is unimportant right now but it tells Unity how the texture is
	// aligned on each polygon
	private List<Vector2> terrainUV = new List<Vector2>();	
	
	// A mesh is made up of the vertices, triangles and UVs we are going to define,
	// after we make them up we'll save them as this mesh
	private Mesh mesh;

	Vector3 fromPolar (Vector2 thePolar)
	{
		return new Vector3 (thePolar.x * Mathf.Cos (thePolar.y), thePolar.x * Mathf.Sin (thePolar.y), 0.0f);
	}
	
	void Start () {
		
		List<float> heights = new List<float>();
		
		int power = (int)Mathf.Pow (2.0f, (float)Math.Ceiling (Mathf.Log (Steps) / Mathf.Log (2.0f)));
		
		Debug.Log (power);
		float[] arr = new float[power + 1];
		heights = new List<float>(arr);
		
		float displace = Height - 1.5f;
		// Set the initial left point
		heights[0] = UnityEngine.Random.Range (-displace, displace);
		// set the initial right point
		heights[power] = UnityEngine.Random.Range (-displace, displace);
		displace *= Roughness;
		
		// Increase the number of segments
		for (int i = 1; i < power; i *= 2) {
			// Iterate through each segment calculating the center point
			for (int j = (power / i) / 2; j < power; j += power / i) {
				heights[j] = ((heights[j - (power / i) / 2] + heights[j + (power / i) / 2]) / 2);
				heights[j] += UnityEngine.Random.Range (-displace, displace);
				//Debug.Log (j);
			}
			// reduce our random range
			displace *= Roughness;
		}

		float step = 2.0f * Mathf.PI / power;
		
		mesh = GetComponent<MeshFilter> ().mesh;

		terrainVertices.Add (new Vector3 (0.0f, 0.0f, 0.0f));
		terrainColors.Add (TerrainColor);
		
		for (int x = 0; x < power; x++) {
			terrainVertices.Add (fromPolar (new Vector2(Mathf.Max (Height + heights[x], 0.5f), x * step)));
			terrainColors.Add (TerrainColor);
			
			if (x != 0)
			{
				int lastVertex = terrainVertices.Count - 1;
				terrainTriangles.Add (0);
				terrainTriangles.Add (lastVertex - 0);
				terrainTriangles.Add (lastVertex - 1);
			}
		}

		terrainTriangles.Add (0);
		terrainTriangles.Add (1);
		terrainTriangles.Add (terrainVertices.Count - 1);
		
		mesh.Clear ();
		mesh.vertices = terrainVertices.ToArray();
		mesh.triangles = terrainTriangles.ToArray();
		mesh.colors = terrainColors.ToArray ();
		mesh.Optimize ();
		mesh.RecalculateNormals ();
		
		if (BuildCollider) {
			List<Vector2> colliderPoints = new List<Vector2> ();
			
			for (int x = 0; x < power; x++) {
				colliderPoints.Add (new Vector2 (x, heights [x] + Height));
			}
			
			EdgeCollider2D collider = GetComponent<EdgeCollider2D> ();
			collider.points = colliderPoints.ToArray ();
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
	
}














