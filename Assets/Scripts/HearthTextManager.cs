using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HearthTextManager : MonoBehaviour {
	TextMeshProUGUI textMesh;

	HearthManager hearthManager;

	void Start () {
		hearthManager = HearthManager.instance;
		hearthManager.onHearthValueChange = OnHearthValueChange;

		textMesh = GetComponent<TextMeshProUGUI>();
		if(PlayerPrefs.GetString("DifficultyLevel") == "1H") {
			textMesh.text = "";
		} else if (PlayerPrefs.GetString("DifficultyLevel") == "3H") {
			textMesh.text = "3<sprite=1>";
		} else if(PlayerPrefs.GetString("DifficultyLevel") == "RegH") {
			textMesh.text = "<sprite=1>";
		}
	}

	void OnHearthValueChange (int value, bool regenable) {
		if (!hearthManager.ActiveDisplay) {
			return;
		}

		if (regenable && value > 0) {
			textMesh.text = "<sprite=1>";
		} else if (regenable && value <= 0) {
			textMesh.text = "<sprite=0>";
		} else {
			textMesh.text = value + "<sprite=1>";
		}
	}
}
