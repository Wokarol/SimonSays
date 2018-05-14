using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreTextController : MonoBehaviour {
	PointsManager pointsManager;
	private void Awake () {
		pointsManager = PointsManager.instance;
		pointsManager.onScoreChange += OnScoreChange;
		textMesh = GetComponent<TextMeshProUGUI>();
	}

	TextMeshProUGUI textMesh;

	void OnScoreChange (int score) {
		textMesh.text = score.ToString();
		Debug.Log("DBL: ST: Updated score to " + score);
	}
}
