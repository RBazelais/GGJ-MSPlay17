using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollide : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		this.GetComponent<Rigidbody>().

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		Vector3 relativePixelPos = gameObject.transform.InverseTransformPoint (collision.contacts [0].point);
			
		//gameObject.GetComponent<WaveTerrain> ().pushDown (3, -0.5f, 32, 32);
		gameObject.GetComponent<WaveTerrain> ().pushDownPos (3, -0.5f, relativePixelPos.x, relativePixelPos.z);

	}
}
