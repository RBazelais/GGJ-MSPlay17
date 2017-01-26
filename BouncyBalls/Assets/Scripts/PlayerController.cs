using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;


public class PlayerController : MonoBehaviour {

	public string verticalName;
	public string horizontalName;
	public string actionName;
	public int playerId;

	public bool canSlam;
	public float heavyRippleAmount;

	private Player player; // The Rewired Player

	void Awake() {
		// Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
		player = ReInput.players.GetPlayer(playerId);
	}


	// Use this for initialization
	void Start () {
		foreach (string names in Input.GetJoystickNames()) {
			Debug.Log (names);

		}
	}
	
	// Update is called once per frame
	void Update () {



		if (gameObject.GetComponent<Rigidbody> ().velocity.y > 8f) {
			Debug.Log("Slow Down!");

			gameObject.GetComponent<Rigidbody>().AddForce(
				new Vector3(0f, -10f, 0f), ForceMode.Acceleration
			);
		}

		var x = player.GetAxis("MoveLeftRight") * Time.deltaTime;
		var z = player.GetAxis("MoveForwardBack") * Time.deltaTime;

		//transform.Rotate (0, x, 0);
		gameObject.GetComponent<Rigidbody>().AddForce(
			new Vector3(x * 500.0f, 0f, z * 500.0f), ForceMode.Acceleration
		);

		if (player.GetButtonDown("Slam") && canSlam) {
			float downForce = (gameObject.transform.position.y - 3f) * -1.5f;
			heavyRippleAmount = (gameObject.transform.position.y - 8.5f) * 0.5f;
			gameObject.GetComponent<Rigidbody>().AddForce(
				new Vector3(x * 100f, downForce, z * 100f), ForceMode.Impulse
			);
			canSlam = false; //Set to true again when ball hits ground
			Debug.Log(gameObject.transform.position.y);
		}
		//transform.Translate (x, 0, z);
	} 
}
//gameObject.transform.InverseTransformDirection()