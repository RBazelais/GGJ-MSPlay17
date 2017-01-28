using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public GameObject[] ballIconPrefabs = new GameObject[2];
	public int numLives;

	private GameObject[] blueLifeIcons;
	private GameObject[] redLifeIcons;
	private int blueLivesRemaining;
	private int redLivesRemaining;


	// Use this for initialization
	void Start () {
		blueLifeIcons = new GameObject[numLives];
		blueLivesRemaining = numLives;
		redLifeIcons = new GameObject[numLives];
		redLivesRemaining = numLives;

		for (int i = 0; i < numLives; ++i) {
			blueLifeIcons [i] = Instantiate (ballIconPrefabs [0], gameObject.transform);
			blueLifeIcons [i].transform.localPosition = new Vector3 (-7f, i * -0.75f + 4.25f, 1f);
			blueLifeIcons[i].transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
			blueLifeIcons [i].transform.rotation = Random.rotation;
			blueLifeIcons [i].layer = 8;
			//blueLifeIcons [i].
			redLifeIcons [i] = Instantiate (ballIconPrefabs [1], gameObject.transform);
			redLifeIcons [i].transform.localPosition = new Vector3 (7f, i * -0.75f + 4.25f, 1f);
			redLifeIcons[i].transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
			redLifeIcons [i].transform.rotation = Random.rotation;
			redLifeIcons [i].layer = 8;



		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < numLives; ++i) {
			blueLifeIcons [i].transform.Rotate(new Vector3 (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
			redLifeIcons [i].transform.Rotate(new Vector3 (Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
		}
	}

	public void loseBlueLife() {
		if (blueLivesRemaining > 0) {
			--blueLivesRemaining;
			blueLifeIcons [blueLivesRemaining].GetComponent<MeshExploder> ().Explode ();
			blueLifeIcons [blueLivesRemaining].GetComponent<MeshRenderer> ().enabled = false;
		}
	}

	public void loseRedLife() {
		if (redLivesRemaining > 0) {
			--redLivesRemaining;
			redLifeIcons [redLivesRemaining].GetComponent<MeshExploder> ().Explode ();
			redLifeIcons [redLivesRemaining].GetComponent<MeshRenderer> ().enabled = false;
		}
	}
}
