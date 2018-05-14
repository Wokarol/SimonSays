using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugPanelManager : MonoBehaviour {
	[SerializeField] protected GameObject toggle;
	[SerializeField] protected GameObject panelHolder;
	[SerializeField] protected TextMeshProUGUI debugInfoText;
	protected bool isOpened;

	protected int newPatInd;
	protected int newRec;

	protected const int correctDebugCode = 123789;

	private void Start () {
		CheckActive();
	}

	public void CheckActive () {
		if (PlayerPrefs.GetInt("DebugCodeValue") == correctDebugCode && PlayerPrefs.GetInt("IsDebugOn") == 1) {
			Activate();
		} else {
			Deactivate();
		}
	}

	void Activate () {
		toggle.SetActive(true);
		panelHolder.SetActive(false);
		isOpened = false;
		debugInfoText.gameObject.SetActive(true);
		debugInfoText.text = "Debug " + Application.version.ToString() + " (" + System.DateTime.Now.Day + "." + System.DateTime.Now.Month + "." + System.DateTime.Now.Year + ")";
	}

	void Deactivate () {
		toggle.SetActive(false);
		panelHolder.SetActive(false);
		debugInfoText.gameObject.SetActive(false);
		isOpened = false;
	}

	public void TogglePanel () {
		panelHolder.SetActive(!panelHolder.activeSelf);
		isOpened = !isOpened;
	}

	// -------------------------

	public void Df_SetPattern(int index) {
		PlayerPrefs.SetInt("SelectedPattern", index);
	}

	public void Df_DeleteRecords () {
		PlayerPrefs.DeleteKey("PointsRecord");
	}

	public void Df_SetNewPatInd (string index) {
		newPatInd = int.Parse(index);
	}

	public void Df_SubmitNewPatInd () {
		Df_SetPattern(newPatInd);
	}

	public void Df_SetNewRec (string _newRec) {
		newRec = int.Parse(_newRec);
	}

	public void Df_SubmitNewRec () {
		string recordKey = PlayerPrefs.GetString("GameMode") + "_" + PlayerPrefs.GetString("DifficultyLevel") + "_Record";
		PlayerPrefs.SetInt(recordKey, newRec);
	}

	public void Df_DeleteAllKeys () {
		PlayerPrefs.DeleteAll();
	}
}
