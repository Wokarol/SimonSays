using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeButtonsManager : MonoBehaviour {

	[SerializeField] GameModeButton[] buttonsToRefresh;
	[SerializeField] string key;
	[SerializeField] string defaultValue;
	[SerializeField] PointsRecordTextController pointsRecordText;

	// Use this for initialization
	private void Start () {
		string value;
		if (PlayerPrefs.HasKey(key)) {
			value = PlayerPrefs.GetString(key);
		} else {
			PlayerPrefs.SetString(key, defaultValue);
			value = defaultValue;
		}
		for (int i = 0; i < buttonsToRefresh.Length; i++) {
			buttonsToRefresh[i].Refresh(value);
		}
	}

	public void SetValue (string value) {
		PlayerPrefs.SetString(key, value);
		for (int i = 0; i < buttonsToRefresh.Length; i++) {
			buttonsToRefresh[i].Refresh(value);
		}
		pointsRecordText.RefreshRecord();
	}
}
