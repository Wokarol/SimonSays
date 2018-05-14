using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetInputFieldToPlayerPrefs : MonoBehaviour {

	[SerializeField] string key;
	[SerializeField] TMP_InputField inputField;
	[SerializeField] PlayerPrefsType type;
	
	enum PlayerPrefsType { Int, Float, String}
	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey(key)) {
			inputField.text = "";
			return;
		}

		switch (type) {
			case PlayerPrefsType.Int:
				inputField.text = PlayerPrefs.GetInt(key).ToString();
				break;
			case PlayerPrefsType.Float:
				inputField.text = PlayerPrefs.GetFloat(key).ToString();
				break;
			case PlayerPrefsType.String:
				inputField.text = PlayerPrefs.GetString(key);
				break;
			default:
				break;
		}
	}
}
