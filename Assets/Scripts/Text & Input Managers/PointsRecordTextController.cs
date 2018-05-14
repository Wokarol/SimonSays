using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PointsRecordTextController : MonoBehaviour {

	TextMeshProUGUI textMesh;

	void Start () {
		RefreshRecord();
	}

	public void RefreshRecord () {
		textMesh = GetComponent<TextMeshProUGUI>();
		string preText = "Record: ";
		string recordKey = PlayerPrefs.GetString("GameMode") + "_" + PlayerPrefs.GetString("DifficultyLevel") + "_Record";
		textMesh.text = preText + PlayerPrefs.GetInt(recordKey);
	}
}
