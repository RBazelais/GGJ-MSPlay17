using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public string verticalName;
	public string horizontalName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var x = Input.GetAxis (horizontalName) * Time.deltaTime * 5.0f;
		var z = Input.GetAxis (verticalName) * Time.deltaTime * 5.0f;

		//transform.Rotate (0, x, 0);
		gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(x, 0, z));
		transform.Translate (x, 0, z);
	}
}
