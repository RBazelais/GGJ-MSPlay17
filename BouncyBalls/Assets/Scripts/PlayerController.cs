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
		var x = Input.GetAxis (horizontalName) * Time.deltaTime * 3.0f;
		var z = Input.GetAxis (verticalName) * Time.deltaTime * 3.0f;

		//transform.Rotate (0, x, 0);
		transform.Translate (x, 0, z);
	}
}
