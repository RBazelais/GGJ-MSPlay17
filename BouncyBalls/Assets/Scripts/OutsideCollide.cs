using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideCollide : MonoBehaviour {

	public GameObject gameplayTerrain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		gameplayTerrain.GetComponent<GroundCollide>().collideWith(collision);
	}

}
