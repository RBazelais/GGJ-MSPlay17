﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterCanvasControl : MonoBehaviour {

	[SerializeField] 
	private CanvasGroup startUI;
	[SerializeField] 
	private CanvasGroup redWinUI;
	[SerializeField] 
	private CanvasGroup blueWinUI;
	[SerializeField] 
	private Button startButton;
	[SerializeField] 
	private Button redWinButton;
	[SerializeField] 
	private Button blueWinButton;
	[SerializeField] 
	private Camera cameraUI3D;

	//called even if the script or object is not active
	void Awake(){
		redWinUI.alpha = 0f; //invisible
		redWinUI.interactable = false; 
		redWinUI.blocksRaycasts = false; 

		blueWinUI.alpha = 0f; //invisible
		blueWinUI.interactable = false; 
		blueWinUI.blocksRaycasts = false;

		cameraUI3D.enabled = false;

	}

	public void showRedWin(){
		redWinUI.alpha = 1f; //Make Red Win Visible
		redWinUI.interactable = true; 
		redWinUI.blocksRaycasts = true;
		Debug.Log ("Show Red Win Screen");

		startUI.alpha = 0f; //start menu now invisible
		startUI.interactable = false; 
		startUI.blocksRaycasts = false; 

		blueWinUI.alpha = 0f; //Blue Win is invisible
		blueWinUI.interactable = false; 
		blueWinUI.blocksRaycasts = false; 

		redWinButton.Select ();

	}

	public void showBlueWin(){
		blueWinUI.alpha = 1f; //Make Blue Win visible
		blueWinUI.interactable = true; 
		blueWinUI.blocksRaycasts = true; 
		Debug.Log ("Show blue Win Screen"); 

		startUI.alpha = 0f; //start screen is invisible
		startUI.interactable = false; 
		startUI.blocksRaycasts = false; 

		redWinUI.alpha = 0f; //Red Win is invisible
		redWinUI.interactable = false; 
		redWinUI.blocksRaycasts = false; 

		blueWinButton.Select ();


	}

	public void showStartUI(){
		startUI.alpha = 1f; //start menu now visible
		startUI.interactable = true; 
		startUI.blocksRaycasts = true; 

		blueWinUI.alpha = 0f; //Blue Win is invisible
		blueWinUI.interactable = false; 
		blueWinUI.blocksRaycasts = false; 

		redWinUI.alpha = 0f; //Red Win is invisible
		redWinUI.interactable = false; 
		redWinUI.blocksRaycasts = false; 

		cameraUI3D.enabled = false;

		startButton.Select ();

	}

	public void hideStartUI(){
		startUI.alpha = 0f; //start menu now invisible
		startUI.interactable = false; 
		startUI.blocksRaycasts = false; 

		blueWinUI.alpha = 0f; //Blue Win is invisible
		blueWinUI.interactable = false; 
		blueWinUI.blocksRaycasts = false; 

		redWinUI.alpha = 0f; //Red Win is invisible
		redWinUI.interactable = false; 
		redWinUI.blocksRaycasts = false; 

		cameraUI3D.enabled = true;

	}


	public void StartLevel(){
		Debug.Log("Start the game");
	}
}
