using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTerrain : MonoBehaviour {

	public float damping;
	public int bufferZone;

	private Terrain thisTerrain;
	private float[,] velocityMap;
	private float[,] heightMap;
	private float[,] displayMap;
	private float elevation = 0.25f;

	// Use this for initialization
	void Start () {
		thisTerrain = gameObject.GetComponent<Terrain> ();

		//heightMap = thisTerrain.terrainData.GetHeights(0, 0, thisTerrain.terrainData.heightmapWidth, thisTerrain.terrainData.heightmapHeight);
		velocityMap = new float[thisTerrain.terrainData.heightmapWidth * (1 + bufferZone), thisTerrain.terrainData.heightmapHeight * (1 + bufferZone)];
		heightMap = new float[thisTerrain.terrainData.heightmapWidth * (1 + bufferZone), thisTerrain.terrainData.heightmapHeight * (1 + bufferZone)];
		displayMap = new float[thisTerrain.terrainData.heightmapWidth, thisTerrain.terrainData.heightmapHeight];

		for (int x = 0; x < velocityMap.GetLength(0); x++) {
			for (int y = 0; y < velocityMap.GetLength(1); y++) {
				velocityMap [x, y] = 0f;
				heightMap [x, y] = elevation;
			}
		}

		for (int x = 0; x < displayMap.GetLength(0); x++) {
			for (int y = 0; y < displayMap.GetLength(1); y++) {
				displayMap [x, y] = 0f;
			}
		}
		//heightMap = velocityMap;
		thisTerrain.terrainData.SetHeights(0, 0, displayMap);

		//velocityMap [16, 16] = -4f;
		//pushDown(3, -0.5f, velocityMap.GetLength(0) / 2, velocityMap.GetLength(1) / 2);
	}

	// Update is called once per frame
	void Update () {

		for (int x = 0; x < velocityMap.GetLength(0); x++) {
			for (int y = 0; y < velocityMap.GetLength(1); y++) {
				//				v[i,j] += (u[i-1,j] + u[i+1,j] + u[i,j-1] + u[i,j+1])/4 – u[i,j]
				//				v[i,j] *= 0.99
				//				u[i,j] += v[i,j]
				float defaultValue = elevation;
//				float defaultValue = heightMap[x, y];

				float neighborLeft = (x == 0) ? defaultValue : heightMap[x-1, y];
				float neighborTop = (y == 0) ? defaultValue : heightMap[x, y-1];
				float neighborRight = (x == heightMap.GetLength(0) - 1) ? defaultValue : heightMap[x+1, y];
				float neighborBottom = (y == heightMap.GetLength(1) - 1) ? defaultValue : heightMap[x, y+1];

				float neighborTopLeft = (x == 0) ? defaultValue : (y == 0) ? defaultValue : heightMap[x-1, y-1];
				float neighborTopRight = (y == 0) ? defaultValue : (x == heightMap.GetLength(0) - 1) ? defaultValue : heightMap[x+1, y-1];
				float neighborBottomRight = (y == heightMap.GetLength(1) - 1) ? defaultValue :(x == heightMap.GetLength(0) - 1) ? defaultValue : heightMap[x+1, y+1];
				float neighborBottomLeft = (y == heightMap.GetLength(1) - 1) ? defaultValue : (x == 0) ? defaultValue : heightMap[x-1, y+1];

				velocityMap[x,y] += (((neighborBottom + neighborLeft + neighborRight + neighborTop)
					+ (neighborTopLeft + neighborTopRight + neighborBottomLeft + neighborBottomRight) * 0.66f) / 6.64f
					- heightMap[x,y]) * 0.25f;
				velocityMap[x,y] *= damping;
				//				Debug.Log (velocityMap [x, y]);
			}
		}
		for (int x = 0; x < velocityMap.GetLength(0); x++) {
			for (int y = 0; y < velocityMap.GetLength(1); y++) {
				heightMap[x,y] += velocityMap[x,y];
			}
		}

		updateWithSection ();
		//this.thisTerrain.terrainData.SetHeights(0, 0, heightMap);
		//Debug.Log (velocityMap [16, 16]);


	}

	public void pushDown (int radius, float force, int xPos, int yPos) {
		int startX = (xPos - radius < 0) ? 0 : xPos - radius;
		int startY = (yPos - radius < 0) ? 0 : yPos - radius;
		int endX = (xPos + radius > velocityMap.GetLength (0) - 1) ? velocityMap.GetLength (0) - 1 : xPos + radius;
		int endY = (yPos + radius > velocityMap.GetLength (1) - 1) ? velocityMap.GetLength (1) - 1 : yPos + radius;

		int x;
		int y;
		float totalVelocity = 0;
		float velocityChange = 0;
		for (x = startX; x < endX; ++x) {
			for (y = startY; y < endY; ++y) {
				velocityChange = force / Mathf.Sqrt (x * x + y * y);
				velocityMap [x, y] += velocityChange;
				totalVelocity += velocityChange;
			}
		}
		float counterVelocity = velocityChange / velocityMap.Length;
		for (x = 0; x <  velocityMap.GetLength (0); ++x) {
			for (y = 0; y <  velocityMap.GetLength (1); ++y) {
				velocityMap [x, y] -= counterVelocity;
			}
		}
	}

	public void pushDownPos (int radius, float force, float xPos, float yPos) {
		Vector3 terrainSize = thisTerrain.terrainData.bounds.size;
		int yIntPos = (bufferZone * displayMap.GetLength (0) / 2) + Mathf.FloorToInt ((xPos / terrainSize.x) * displayMap.GetLength (0));
		int xIntPos = (bufferZone * displayMap.GetLength (1) / 2) + Mathf.FloorToInt ((yPos / terrainSize.z) * displayMap.GetLength (1));
		pushDown (radius, force, xIntPos, yIntPos);
	}

	void updateWithSection() {
		for (int x = 0; x < displayMap.GetLength (0); x++) {
			for (int y = 0; y < displayMap.GetLength (1); y++) {
				displayMap [x, y] = heightMap [x + bufferZone * displayMap.GetLength (0) / 2, y + bufferZone * displayMap.GetLength (1) / 2];
			}
		}

		thisTerrain.terrainData.SetHeights(0, 0, displayMap);
	}
}