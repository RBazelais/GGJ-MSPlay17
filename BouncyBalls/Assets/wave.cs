using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour {

	public Terrain thisTerrain;

	private float[,] velocityMap;
	private float[,] heightMap;
	private float elevation = 1f;

	// Use this for initialization
	void Start () {
		//heightMap = thisTerrain.terrainData.GetHeights(0, 0, thisTerrain.terrainData.heightmapWidth, thisTerrain.terrainData.heightmapHeight);
		velocityMap = new float[thisTerrain.terrainData.heightmapWidth, thisTerrain.terrainData.heightmapHeight];
		heightMap = new float[thisTerrain.terrainData.heightmapWidth, thisTerrain.terrainData.heightmapHeight];

		for (int x = 0; x < velocityMap.GetLength(0); x++) {
			for (int y = 0; y < velocityMap.GetLength(1); y++) {
				velocityMap [x, y] = 0f;
				heightMap [x, y] = elevation;
			}
		}
		//heightMap = velocityMap;
		thisTerrain.terrainData.SetHeights(0, 0, heightMap);

		velocityMap [16, 16] = -2f;
	}
	
	// Update is called once per frame
	void Update () {
		
		for (int x = 0; x < velocityMap.GetLength(0); x++) {
			for (int y = 0; y < velocityMap.GetLength(1); y++) {
//				v[i,j] += (u[i-1,j] + u[i+1,j] + u[i,j-1] + u[i,j+1])/4 – u[i,j]
//				v[i,j] *= 0.99
//				u[i,j] += v[i,j]
				float neighborLeft = (x == 0) ? elevation : heightMap[x-1, y];
				float neighborTop = (y == 0) ? elevation : heightMap[x, y-1];
				float neighborRight = (x == heightMap.GetLength(0) - 1) ? elevation : heightMap[x+1, y];
				float neighborBottom = (y == heightMap.GetLength(1) - 1) ? elevation : heightMap[x, y+1];

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

				velocityMap[x,y] += (neighborBottom + neighborLeft + neighborRight + neighborTop) / 4f - heightMap[x,y];
				velocityMap[x,y] *= 0.98f;
//				Debug.Log (velocityMap [x, y]);
			}
		}
		for (int x = 0; x < velocityMap.GetLength(0); x++) {
			for (int y = 0; y < velocityMap.GetLength(1); y++) {
				heightMap[x,y] += velocityMap[x,y];
			}
		}
		this.thisTerrain.terrainData.SetHeights(0, 0, heightMap);
		//Debug.Log (velocityMap [16, 16]);

	
	}
}
