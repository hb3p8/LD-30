using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TerrainScript : MonoBehaviour {

	public bool BuildCollider = true;
	public bool BuildLanding = true;
	public Color TerrainColor = new Color (0.21f, 0.21f, 0.21f);
	public float Height = 6.0f;
	public float Width = 100.0f;
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

	void Start () {
		
		List<float> heights = new List<float>();

		int power = (int)Math.Pow (2.0f, (float)Math.Ceiling (Math.Log (Width) / Math.Log (2.0f)));
		
		Debug.Log (power);
		float[] arr = new float[power + 1];
		heights = new List<float>(arr);
		
		float displace = Height - 0.5f;
		// Set the initial left point
		heights[0] = Height / 2.0f;
		// set the initial right point
		heights[power] = Height / 2.0f;
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

		if (BuildLanding)
		{
			// landing
			for (int x = power / 2 - 2; x <= power / 2 + 2; x++) {
				heights[x] = 3.0f;
			}

			// the dome
			for (int x = power / 2 - 18; x <= power / 2 - 6; x++) {
				heights[x] = 4.5f + UnityEngine.Random.Range (-0.5f, 0.5f);
			}
		}
		
		mesh = GetComponent<MeshFilter> ().mesh;
		
		for (int x = 0; x < power; x++) {

			terrainVertices.Add (new Vector3 (x, Height + heights[x], 0.0f));
			terrainVertices.Add (new Vector3 (x, -1.0f, 0.0f));
			terrainColors.Add (TerrainColor);
			terrainColors.Add (TerrainColor);
			//terrainColors.Add (new Color (0.1f, 0.2f, 0.5f));
			//terrainColors.Add (new Color (0.78f, 0.55f, 0.24f));
			
			if (x != 0)
			{
				int lastVertex = terrainVertices.Count - 1;
				terrainTriangles.Add (lastVertex - 2);
				terrainTriangles.Add (lastVertex - 3);
				terrainTriangles.Add (lastVertex - 1);
				
				terrainTriangles.Add (lastVertex - 2);
				terrainTriangles.Add (lastVertex - 1);
				terrainTriangles.Add (lastVertex - 0);
			}
			
		}
		
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














