using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public string verticalName;
	public string horizontalName;
	public string actionName;

	public bool canSlam;
	public float heavyRippleAmount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<Rigidbody> ().velocity.y > 8f) {
			Debug.Log("Slow Down!");

			gameObject.GetComponent<Rigidbody>().AddForce(
				new Vector3(0f, -10f, 0f), ForceMode.Acceleration
			);
		}

		var x = Input.GetAxis (horizontalName) * Time.deltaTime * 500.0f;
		var z = Input.GetAxis (verticalName) * Time.deltaTime * 500.0f;

		//transform.Rotate (0, x, 0);
		gameObject.GetComponent<Rigidbody>().AddForce(
			new Vector3(x, 0f, z), ForceMode.Acceleration
		);

		if (Input.GetButtonDown(actionName) && canSlam) {
			float downForce = (gameObject.transform.position.y - 3f) * -1.5f;
			heavyRippleAmount = (gameObject.transform.position.y - 8.5f) * 0.5f;
			gameObject.GetComponent<Rigidbody>().AddForce(
				new Vector3(0f, downForce, 0f), ForceMode.Impulse
			);
			canSlam = false; //Set to true again when ball hits ground
			Debug.Log(gameObject.transform.position.y);
		}
		//transform.Translate (x, 0, z);
	} 
}
//gameObject.transform.InverseTransformDirection()