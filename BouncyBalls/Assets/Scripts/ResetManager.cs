using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetManager : MonoBehaviour {

	private Vector3 startPos;
	private Rigidbody myRigidbody;


	// Use this for initialization
	void Start () {
		startPos = gameObject.transform.position;
		myRigidbody = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetMe () {
		gameObject.transform.position = startPos;
	
		if (myRigidbody.isKinematic == false) {
			myRigidbody.velocity = Vector3.zero;
			myRigidbody.angularVelocity = Vector3.zero;
		}
		myRigidbody.useGravity = false;
		myRigidbody.isKinematic = true;
		gameObject.active = false;
	}

	public void RestartMe () {
		gameObject.active = true;
		myRigidbody.useGravity = true;

		myRigidbody.isKinematic = false;
	}
}
