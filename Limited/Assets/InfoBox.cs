﻿using UnityEngine;
using UnityEngine.EventSystems;

public class InfoBox : MonoBehaviour
{
	public GameObject topBar;
	public GameObject gameController;
	public CameraController cameraController;
	public Animator animator;

	// Update is called once per frame
	void Update()
	{
		// close info box on first click since it has been awaken
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
		{

			// start game by activating the gamecontroller
			gameController.SetActive(true);
			// show top bar
			topBar.SetActive(true);
			// allow camera movement
			cameraController.enabled = true;

			// trigger closing animation
			animator.SetTrigger("CloseTrigger");
		}
	}
}
