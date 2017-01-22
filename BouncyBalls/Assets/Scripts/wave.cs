using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour {

	public Terrain thisTerrain;
	public float damping;

	private float[,] velocityMap;
	private float[,] heightMap;
	private float[,] displayMap;
	private float elevation = 0.25f;

	// Use this for initialization
	void Start () {
		//heightMap = thisTerrain.terrainData.GetHeights(0, 0, thisTerrain.terrainData.heightmapWidth, thisTerrain.terrainData.heightmapHeight);
		velocityMap = new float[thisTerrain.terrainData.heightmapWidth * 2, thisTerrain.terrainData.heightmapHeight * 2];
		heightMap = new float[thisTerrain.terrainData.heightmapWidth * 2, thisTerrain.terrainData.heightmapHeight * 2];
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
		pushDown(3, -0.5f, velocityMap.GetLength(0) / 2, velocityMap.GetLength(1) / 2);
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

//				if (x == 16 && y == 16) {
//					Debug.Log (neighborBottom);
//					Debug.Log (neighborLeft);
//					Debug.Log (heightMap[x-1, y]);
//					Debug.Log (heightMap[x-2, y]);
//					Debug.Log (heightMap[x, y]);
//
//
//					Debug.Log (neighborRight);
//					Debug.Log (neighborTop);
//					Debug.Log (heightMap[x, y-1]);
//
//					Debug.Log ((neighborBottom + neighborLeft + neighborRight + neighborTop) / 4);
//					Debug.Log (heightMap [x, y]);
//				}

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
		for (x = startX; x < endX; ++x) {
			for (y = startY; y < endY; ++y) {
				velocityMap [x, y] += force / Mathf.Sqrt( x * x + y * y);

			}
		}
	}

	void updateWithSection() {
		for (int x = 0; x < displayMap.GetLength (0); x++) {
			for (int y = 0; y < displayMap.GetLength (1); y++) {
				displayMap [x, y] = heightMap [x + displayMap.GetLength (0) / 2, y + displayMap.GetLength (1) / 2];
			}
		}
		 
		this.thisTerrain.terrainData.SetHeights(0, 0, displayMap);
	}
}
