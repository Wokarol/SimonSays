using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour {
	public static PointsManager instance;
	private void Awake () {
		if(instance == null) {
			instance = this;
		}
	}

	public System.Action<int> onScoreChange;
	public System.Action<int> onSequenceLenghtChange;

	private int score = 0;
	public int Score {
		set {
			score = Mathf.Clamp(value, 0, int.MaxValue);
			if (onScoreChange != null)
				onScoreChange(score);
		}
		get { return score; }
	}

	private int sequenceLenght = 0;
	public int SequenceLenght {
		set {
			sequenceLenght = Mathf.Clamp(value, 0, int.MaxValue);
			if (onSequenceLenghtChange != null)
				onSequenceLenghtChange(sequenceLenght);
		}
		get { return sequenceLenght; }
	}

	private void Start () {
		if (onScoreChange != null)
			onScoreChange(score);
		if (onSequenceLenghtChange != null)
			onSequenceLenghtChange(sequenceLenght);
	}
}
