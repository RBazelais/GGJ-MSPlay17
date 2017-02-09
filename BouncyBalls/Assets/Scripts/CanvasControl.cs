using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour {

	[SerializeField] 
	private CanvasGroup startUI;
	[SerializeField]
	private CanvasGroup QuitMenu;

	//called even if the script or object is not active
	void Awake(){
		QuitMenu.alpha = 0f; //invisible
		QuitMenu.interactable = false; 
		QuitMenu.blocksRaycasts = false; 

	}

	public void hideStartUIOnClick(){
		startUI.alpha = 0f; //start menu now invisible
		startUI.interactable = false; 
		startUI.blocksRaycasts = false; 

		QuitMenu.alpha = 1f; //now visible
		QuitMenu.interactable = true; 
		QuitMenu.blocksRaycasts = true;
		Debug.Log ("Show Quit Menu");


	}

	public void showStartUIOnClick(){
		startUI.alpha = 1f; //start menu now invisible
		startUI.interactable = true; 
		startUI.blocksRaycasts = true; 

		QuitMenu.alpha = 0f; //now visible
		QuitMenu.interactable = false; 
		QuitMenu.blocksRaycasts = false;
		Debug.Log ("Show Quit Menu");


	}
}
