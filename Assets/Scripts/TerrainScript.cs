using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TerrainScript : MonoBehaviour {
	
	// This first list contains every vertex of the mesh that we are going to render
	public List<Vector3> terrainVertices = new List<Vector3>();
	
	public List<Color> terrainColors = new List<Color>();
	
	// The triangles tell Unity how to build each section of the mesh joining
	// the vertices
	public List<int> terrainTriangles = new List<int>();
	
	// The UV list is unimportant right now but it tells Unity how the texture is
	// aligned on each polygon
	public List<Vector2> terrainUV = new List<Vector2>();	
	
	// A mesh is made up of the vertices, triangles and UVs we are going to define,
	// after we make them up we'll save them as this mesh
	private Mesh mesh;

	void Start () {
		
		List<float> heights = new List<float>();
		
		float width = 100.0f;
		float height = 6.0f;
		float roughness = 0.7f;
		int power = (int)Math.Pow (2.0f, (float)Math.Ceiling (Math.Log (width) / Math.Log (2.0f)));
		
		Debug.Log (power);
		float[] arr = new float[power + 1];
		heights = new List<float>(arr);
		
		float displace = height - 0.5f;
		// Set the initial left point
		heights[0] = UnityEngine.Random.Range (-displace, displace);
		// set the initial right point
		heights[power] = UnityEngine.Random.Range (-displace, displace);
		displace *= roughness;
		
		// Increase the number of segments
		for (int i = 1; i < power; i *= 2) {
			// Iterate through each segment calculating the center point
			for (int j = (power / i) / 2; j < power; j += power / i) {
				heights[j] = ((heights[j - (power / i) / 2] + heights[j + (power / i) / 2]) / 2);
				heights[j] += UnityEngine.Random.Range (-displace, displace);
				//Debug.Log (j);
			}
			// reduce our random range
			displace *= roughness;
		}
		
		mesh = GetComponent<MeshFilter> ().mesh;
		
		for (int x = 0; x < power; x++) {

			terrainVertices.Add (new Vector3 (x, height + heights[x], 0.0f));
			terrainColors.Add (new Color (0.1f, 0.2f, 0.5f));
			terrainVertices.Add (new Vector3 (x, -1.0f, 0.0f));
			terrainColors.Add (new Color (0.78f, 0.55f, 0.24f));
			
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
		
		List<Vector2> colliderPoints = new List<Vector2>();
		
		for (int x = 0; x < power; x++) {
			colliderPoints.Add (new Vector2 (x, heights[x] + height));
		}
		
		EdgeCollider2D collider = GetComponent<EdgeCollider2D>();
		collider.points = colliderPoints.ToArray ();
	}

	
	// Update is called once per frame
	void Update () {
		
	}

}














