using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SequenceLenghtTextController : MonoBehaviour {
	PointsManager pointsManager;
	private void Awake () {
		pointsManager = PointsManager.instance;
		pointsManager.onSequenceLenghtChange += OnSequenceLenghtChange;
		textMesh = GetComponent<TextMeshProUGUI>();
	}

	TextMeshProUGUI textMesh;

	void OnSequenceLenghtChange (int sequenceLenght) {
		textMesh.text = (sequenceLenght).ToString();
		Debug.Log("DBL: SLT: Updated sequence lengh to " + (sequenceLenght));
	}
}
