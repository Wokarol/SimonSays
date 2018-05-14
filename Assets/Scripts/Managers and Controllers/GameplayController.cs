using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

	[SerializeField] private SingleMemoryButton[] buttons;
	[SerializeField] float stopTime = 0.05f;
	[SerializeField] List<int> sequence;
	[SerializeField] int buttonsCount = 2;
	[Space]
	[SerializeField] List<Color> buttonColors = new List<Color> {Color.white };
	[Space]
	[SerializeField] List<Color> additionalColors = new List<Color> {Color.black };
	[Space]
	[SerializeField] PatternPack patternPack;
	[SerializeField] int patternIndex;

	int waitingButtonIndex;

	bool triggerWin = false;
	bool triggerFail = false;

	bool ableToLose = false;

	Coroutine fakeClickCoroutine;

	int lives;

	PointsManager pointsManager;
	AudioManager audioManager;
	MessageManager messageManager;
	HearthManager hearthManager;

	public static GameplayController instance;
	private void Awake () {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		patternIndex = PlayerPrefs.GetInt("SelectedPattern");
		Time.timeScale = 1;

		pointsManager = PointsManager.instance;
		pointsManager.Score = 0;

		audioManager = AudioManager.instance;

		messageManager = MessageManager.instance;

		hearthManager = HearthManager.instance;
		SetHearths();

		// Initiation
		sequence = new List<int>();
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].ButtonActive(false);
			buttons[i].button.interactable = false;
			buttons[i].index = i;
			buttons[i].image.color = buttonColors[i];
			buttons[i].normalColor = buttonColors[i];
			buttons[i].AddPattern(patternIndex, patternPack, additionalColors[i]);
			buttons[i].RandomRotatePattern();

			// Assigning components
			if (buttons[i].GameplayController == null){
				buttons[i].GameplayController = this;
			}
			if (buttons[i].AudioManager == null) {
				buttons[i].AudioManager = audioManager;
			}

			if (i < buttonsCount) {
				buttons[i].ButtonActive(true);
				buttons[i].button.interactable = false;
			}
			else {
				buttons[i].ButtonActive(false);
				buttons[i].button.interactable = false;
			}
		}


		StartCoroutine(Gameplay());


	}

	IEnumerator Gameplay () {
		yield return InitiateNewMatch();

		// Gameplay loop
		while (true) {
			while (!(triggerWin || triggerFail)) {
				yield return null;
			}

			// Testing if player winned 
			if (triggerWin) {
				hearthManager.Regenerate();
				ableToLose = true;
				Debug.Log("DBL: G: Winner");
				pointsManager.Score++;
				pointsManager.SequenceLenght++;
				audioManager.PlaySound("Correct Answer");

				// Test if buttonsCount needs to be increased
				if (buttonsCount < 4 && pointsManager.Score >= 2) {
					buttonsCount = 4;
				}
				if (buttonsCount < 8 && pointsManager.Score >= 5) {
					buttonsCount = 8;
				}

				// Inactivating buttons
				yield return new WaitForSeconds(0.2f);
				for (int i = 0; i < buttonsCount; i++) {
					buttons[i].ButtonActive(true);
					buttons[i].button.interactable = false;
				}

				// Adding new colors to sequence
				yield return new WaitForSeconds(1);
				GenerateClick();
				AnimateSequence(1f, true);
			} 
			// Testing if player failed
			else if (triggerFail) {
				hearthManager.HearthValue--;
				if (ableToLose && (	(hearthManager.HearthValue <= 0 && !hearthManager.Regenerable)	|| (hearthManager.HearthValue < 0 && hearthManager.Regenerable))) {

					// GAME OVER
					GameOver();
					yield return new WaitForSeconds(2f);
					yield return InitiateNewMatch();
				} else {

					// Not game over
					messageManager.Message("wrong", new Color(0.93f, 0.40f, 0.35f, 0.70f));
					audioManager.PlaySound("Wrong Answer");
					// Inactivating buttons
					for (int i = 0; i < buttonsCount; i++) {
						buttons[i].ButtonActive(true);
						buttons[i].button.interactable = false;
					}

					yield return new WaitForSeconds(2f);
					AnimateSequence((PlayerPrefs.GetInt("IsSlowerFlashingAfterMistake") == 1) ?2f:1f, false);
				}
			}
			triggerWin = false;
			triggerFail = false;
		}
	}

	public void ExitGameTest () {
		string recordKey = PlayerPrefs.GetString("GameMode") + "_" + PlayerPrefs.GetString("DifficultyLevel") + "_Record";
		if (PlayerPrefs.GetInt(recordKey) < pointsManager.Score) {
			PlayerPrefs.SetInt(recordKey, pointsManager.Score);
		}
	}

	public void GameOver () {
		string recordKey = PlayerPrefs.GetString("GameMode") + "_" + PlayerPrefs.GetString("DifficultyLevel") + "_Record";
		if (PlayerPrefs.GetInt(recordKey) < pointsManager.Score) {
			PlayerPrefs.SetInt(recordKey, pointsManager.Score);
			messageManager.Message("game over\n<size=40%>!new record!<size=100%>", new Color(0.93f, 0.40f, 0.35f, 1f));
		} else {
			messageManager.Message("game over", new Color(0.93f, 0.40f, 0.35f, 1f));
		}

		pointsManager.Score = 0;
		pointsManager.SequenceLenght = 0;
		buttonsCount = 2;

		for (int i = 0; i < buttons.Length; i++) {
			if (i < buttonsCount) {
				buttons[i].ButtonActive(true);
				buttons[i].button.interactable = false;
			} else {
				buttons[i].ButtonActive(false);
				buttons[i].button.interactable = false;
			}
		}
	}

	// -----------------------------------------


	void GenerateClick () {
		for (int i = 0; i < buttonsCount; i++) {
			buttons[i].ButtonActive(true);
			buttons[i].button.interactable = true;
		}
		while (sequence.Count < (pointsManager.SequenceLenght)) {
			// Adding digit
			int digitToAdd = Random.Range(0, buttonsCount);
			for (int i = 0; i < buttonsCount; i++) {
				if (sequence.Count >= 2 && sequence[sequence.Count-1] == digitToAdd && sequence[sequence.Count - 2] == digitToAdd) {
					digitToAdd = (digitToAdd + 1) % buttonsCount;
				} else {
					break;
				}
			}
			Debug.Log("DBL: GC: Added " + digitToAdd);
			sequence.Add(digitToAdd);
		}
		waitingButtonIndex = 0;
		triggerWin = false;
		triggerFail = false;

		Debug.Log("DBL: GC: Generating click ended");
	}

	IEnumerator InitiateNewMatch () {
		SetHearths();
		pointsManager.SequenceLenght = 2;
		sequence = new List<int>();
		messageManager.Message("Start", new Color(.36f, .93f, 0.35f, 0.7f));
		yield return new WaitForSeconds(1f);
		Debug.Log("DBL: G: Starting");
		GenerateClick();
		AnimateSequence(1f, true);
	}

	void AnimateSequence (float stopTimeModifier, bool allowMemory) {
		if (fakeClickCoroutine != null)
			StopCoroutine(fakeClickCoroutine);
		fakeClickCoroutine = StartCoroutine(FakeClicking(stopTime * stopTimeModifier, allowMemory));
	}

	IEnumerator FakeClicking (float _stopTime, bool allowMemory) {
		// disbaling buttons
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].button.interactable = false;
		}
		// clicking
		int startIndex = 0;
		bool halfClicking = false;
		if (allowMemory) {
			if (PlayerPrefs.GetString("GameMode") == "Repeat") {
				startIndex = 0;
				halfClicking = false;
			} else if (PlayerPrefs.GetString("GameMode") == "Memory") {
				startIndex = Mathf.Clamp((sequence.Count - 4), 0, int.MaxValue);
				halfClicking = true;
			}
		} else {
			startIndex = 0;
		}
		for (int i = startIndex; i < sequence.Count; i++) {
			buttons[sequence[i]].PlayClick();
			if (halfClicking) {
				if (sequence.Count - 1 == i) {
					yield return StartCoroutine(buttons[sequence[i]].FakePressAnim(false));
				} else {
					yield return StartCoroutine(buttons[sequence[i]].FakePressAnim(true));
				}
			} else {
				yield return StartCoroutine(buttons[sequence[i]].FakePressAnim(false));
			}
			yield return new WaitForSeconds(_stopTime);
		}
		// enabling buttons
		for (int i = 0; i < buttonsCount; i++) {
			buttons[i].button.interactable = true;
		}

	}

	public void SetHearths () {
		if (PlayerPrefs.GetString("DifficultyLevel") == "1H") {
			hearthManager.Regenerable = false;
			hearthManager.ActiveDisplay = false;
			hearthManager.HearthValue = 1;
		} else if (PlayerPrefs.GetString("DifficultyLevel") == "3H") {
			hearthManager.Regenerable = false;
			hearthManager.ActiveDisplay = true;
			hearthManager.HearthValue = 3;
		} else if (PlayerPrefs.GetString("DifficultyLevel") == "RegH") {
			hearthManager.Regenerable = true;
			hearthManager.ActiveDisplay = true;
			hearthManager.HearthValue = 1;
		}
		hearthManager.RefreshText();
	}

	public void RegisterPress (int indexToTest) {
		Debug.Log("DBL: RP0: Registerring press");
		if(indexToTest == sequence[waitingButtonIndex]) {
			Debug.Log("DBL: RP1: Good");
			waitingButtonIndex++;
			//audioManager.PlaySound("Bonus");
		} else {
			Debug.Log("DBL: RP1: Bad");
			waitingButtonIndex = 0;
			//audioManager.PlaySound("Click");
			triggerFail = true;
		}
		if(waitingButtonIndex >= sequence.Count) {
			Debug.Log("DBL: RP2: Winner");
			triggerWin = true;
		}
		waitingButtonIndex = Mathf.Clamp(waitingButtonIndex, 0, sequence.Count-1);
	}



	private void OnValidate () {
		while(buttonColors.Count < buttons.Length) {
			buttonColors.Add(Color.white);
		}
		while (additionalColors.Count < buttons.Length) {
			additionalColors.Add(Color.black);
		}
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].image.color = buttonColors[i];
			buttons[i].AddPattern(patternIndex, patternPack, additionalColors[i]);
		}
	}

	[ContextMenu("Test sequence")]
	public void TestSequence () {
		sequence = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
	}
	[ContextMenu("Random rotate patterns")]
	public void RandRotPat () {
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].RandomRotatePattern();
		}
	}
	[ContextMenu("Reset progress")]
	void ResetPr () {
		PlayerPrefs.DeleteKey("PointsRecord");
	}


	// Debug functions
	public void TriggerWin () {
		triggerWin = true;
	}
	public void TriggerFail () {
		triggerFail = true;
	}
	public string[] GetData () {
		List<string> dataPack = new List<string> {
			"buttonsCount: " + buttonsCount.ToString(),
			"waitingButtonIndex: " + waitingButtonIndex.ToString()
		};
		var ints = sequence;
		var stringsArray = ints.Select(i => i.ToString()).ToArray();
		var values = string.Join(",", stringsArray);
		dataPack.Add(values);
		return dataPack.ToArray();
	}

	public void DebugAnimateSeq () {
		AnimateSequence(1f, true);
	}
	public void AddIndex (int index) {
		sequence.Add(index);
	}
}
