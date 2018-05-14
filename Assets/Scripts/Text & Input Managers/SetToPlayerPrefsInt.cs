using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetToPlayerPrefsInt : MonoBehaviour {

	[SerializeField] string key;
	[SerializeField] bool defaultState;
	[Space]
	[SerializeField] Toggle toggle;

	void Start () {
		if (PlayerPrefs.HasKey(key)) {
			toggle.isOn = (PlayerPrefs.GetInt(key) == 1) ? true : false;
		} else {
			toggle.isOn = defaultState;
		}
	}
}
