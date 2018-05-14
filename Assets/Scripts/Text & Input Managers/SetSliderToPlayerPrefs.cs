using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSliderToPlayerPrefs : MonoBehaviour {

	[SerializeField] Slider slider;
	[SerializeField] string key;
	[SerializeField] float defaultValue;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey(key)){
			slider.value = PlayerPrefs.GetFloat(key);
		} else {
			PlayerPrefs.SetFloat(key, defaultValue);
			slider.value = defaultValue;
		}
	}
}
