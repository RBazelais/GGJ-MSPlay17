using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GroundCollide : MonoBehaviour {

	public float collideForce;
	public int forceWidth;
	public float minimumSpeed;
	public float offPlatformRadius;
	public GameObject[] ballArray;
	public GameObject[] ballPrefabs;
	public GameObject scoreManagerObject;
	public CanvasGroup blueWinUI; 
	public CanvasGroup redWinUI; 


	private GameObject blueBall;
	private GameObject redBall;

	// Use this for initialization
	void Start () {
//		this.GetComponent<Rigidbody>().
//		blueBall = Instantiate(ballPrefabs[0]);
//		redBall = Instantiate(ballPrefabs[1]);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		collideWith (collision);
	}

	void ResetAll () {
		for (int i = 0; i < ballArray.Length; ++i) {
			//			ResetManager resetManager = thisObject.GetComponent<ResetManager> ();
			//				resetManager.ResetMe ();
			//				resetManager.RestartMe ();
			Destroy(ballArray[i]);

			ballArray[i] = Instantiate(ballPrefabs[i]);
			 
		}
	}

	public void clearBalls() {
		Destroy(blueBall);
		Destroy(redBall);	
	}

	public void setupBalls() {
		blueBall = Instantiate(ballPrefabs[0]);
		redBall = Instantiate(ballPrefabs[1]);
	}

	public void collideWith(Collision collision) {
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
			//gameObject.GetComponent<AudioSource> ().Play ();

		}

		if (isOutOfBounds)
		{
			Debug.Log("Out of Bounds!");
			//SceneManager.LoadScene(SceneManager.GetActiveScene ().name);

			collision.collider.GetComponent<MeshExploder> ().Explode ();
			//ResetAll();
			if (collision.collider.tag == "blue") {
				bool blueHasLives = scoreManagerObject.GetComponent<ScoreManager> ().loseBlueLife();
				Destroy(blueBall);
				if (blueHasLives) {					
					blueBall = Instantiate (ballPrefabs [0]);
				} else {
					//Get Rigitbody an then freeze it's X and Y position when blueBall wins
					redBall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

					redWinUI.alpha = 1f; //Make Red Win Visible
					redWinUI.interactable = true; 
					redWinUI.blocksRaycasts = true;
					Debug.Log ("Show Red Win Screen");


				}
			} else if (collision.collider.tag == "red") {
				bool redHasLives = scoreManagerObject.GetComponent<ScoreManager>().loseRedLife();
				Destroy(redBall);
				if (redHasLives) {
					redBall = Instantiate (ballPrefabs [1]);
				}else {
					blueBall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

					blueWinUI.alpha = 1f; //Make Blue Win visible
					blueWinUI.interactable = true; 
					blueWinUI.blocksRaycasts = true; 
					Debug.Log ("Show Red Win Screen"); 
				}
			}
			//collision.collider.tag
		}

		collision.collider.GetComponent<PlayerController> ().canSlam = true;
		collision.collider.GetComponent<PlayerController> ().heavyRippleAmount = 0f;
	}
}


