using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDebugPanelManager : DebugPanelManager {

	[SerializeField] TextMeshProUGUI textMesh;
	GameplayController gameplayController;
	private void Start () {
		gameplayController = GameplayController.instance;
		CheckActive();
	}
	private void Update () {
		if (isOpened) {
			string[] textData = gameplayController.GetData();
			textMesh.text = string.Join("\n", textData);
		}
	}
	// ------------------- FUNCTIONS

	public void Df_TriggerWin () {
		gameplayController.TriggerWin();
	}
	public void Df_TriggerFail () {
		gameplayController.TriggerFail();
	}
	public void Df_RepeatSeq () {
		gameplayController.DebugAnimateSeq();
	}
	public void Df_TestSeq () {
		gameplayController.TestSequence();
	}
	public void Df_AddToSeq (int index) {
		gameplayController.AddIndex(index);
	}
}
