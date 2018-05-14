using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MenuController {

	public Button pauseTriggerButton;
	bool isPauseActive;

	GameplayController gameplayController;

	private void Start () {
		gameplayController = GameplayController.instance;
	}

	public void GameOver () {
		gameplayController.ExitGameTest();
	}

	public void ActivateMenu () {
		pauseTriggerButton.interactable = false;
		Time.timeScale = 0f;
	}
	public void DeactivateMenu () {
		pauseTriggerButton.interactable = true;
		Time.timeScale = 1f;
	}

}
