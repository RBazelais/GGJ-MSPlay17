using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroundCollide : MonoBehaviour {

	public float collideForce;
	public int forceWidth;
	public float minimumSpeed;
	public float offPlatformRadius;
	public GameObject[] ballArray;
	public GameObject[] ballPrefabs;

	// Use this for initialization
	void Start () {
//		this.GetComponent<Rigidbody>().

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {

		float impactSpeed = Mathf.Abs( collision.relativeVelocity.y);
		float speedModifier = 1f;
		int radiusModifier = 0;
		float bonusRipple = collision.collider.GetComponent<PlayerController> ().heavyRippleAmount;


		bool isOutOfBounds = collision.collider.transform.position.x * collision.collider.transform.position.x
		                     + collision.collider.transform.position.z * collision.collider.transform.position.z
		                     > offPlatformRadius * offPlatformRadius;

		if (isOutOfBounds) {
			speedModifier = 0.25f;
			radiusModifier = 2;
		} else {
			speedModifier = 0f;
			radiusModifier = Mathf.FloorToInt (bonusRipple);
		}
			
		//gameObject.GetComponent<WaveTerrain> ().pushDown (3, -0.5f, 32, 32);
		if (impactSpeed > minimumSpeed) {
			//float bonusSlam = (collision.collider.GetComponent<PlayerController> ().canSlam) ? 1f : 2f;
				
			Vector3 relativePixelPos = gameObject.transform.InverseTransformPoint (collision.contacts [0].point);
			gameObject.GetComponent<WaveTerrain> ().pushDownPos (
				forceWidth + radiusModifier, 
				(-1f * impactSpeed * collideForce * collideForce) - speedModifier, 
				relativePixelPos.x, 
				relativePixelPos.z
			);
		
		}

		if (isOutOfBounds)
		{
			Debug.Log("Out of Bounds!");
			//SceneManager.LoadScene(SceneManager.GetActiveScene ().name);

			collision.collider.GetComponent<MeshExploder> ().Explode ();
			ResetAll();

		}

		collision.collider.GetComponent<PlayerController> ().canSlam = true;
		collision.collider.GetComponent<PlayerController> ().heavyRippleAmount = 0f;
	}

	void ResetAll () {
		for (int i = 0; i < ballArray.Length; ++i) {
			//				ResetManager resetManager = thisObject.GetComponent<ResetManager> ();
			//				resetManager.ResetMe ();
			//				resetManager.RestartMe ();
			Destroy(ballArray[i]);

			ballArray[i] = Instantiate(ballPrefabs[i]);
			 
		}
	}
}
