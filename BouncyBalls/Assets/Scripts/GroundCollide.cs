using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollide : MonoBehaviour {

	public float collideForce;
	public int forceWidth;
	public float minimumSpeed;

	// Use this for initialization
	void Start () {
//		this.GetComponent<Rigidbody>().

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {

		float impactSpeed = Mathf.Abs( collision.relativeVelocity.y);

			
		//gameObject.GetComponent<WaveTerrain> ().pushDown (3, -0.5f, 32, 32);
		if (impactSpeed > minimumSpeed) {
			//float bonusSlam = (collision.collider.GetComponent<PlayerController> ().canSlam) ? 1f : 2f;
			float bonusRipple = collision.collider.GetComponent<PlayerController> ().heavyRippleAmount;
				
			Vector3 relativePixelPos = gameObject.transform.InverseTransformPoint (collision.contacts [0].point);
			gameObject.GetComponent<WaveTerrain> ().pushDownPos (
				forceWidth + Mathf.FloorToInt(bonusRipple), 
				-1f * impactSpeed * collideForce * collideForce, 
				relativePixelPos.x, 
				relativePixelPos.z
			);
		
		}

		collision.collider.GetComponent<PlayerController> ().canSlam = true;
		collision.collider.GetComponent<PlayerController> ().heavyRippleAmount = 0f;
	}
}
