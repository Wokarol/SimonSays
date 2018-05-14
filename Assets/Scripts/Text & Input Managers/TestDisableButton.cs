using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestDisableButton : MonoBehaviour {

	public Button button;

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)) {
			button.interactable = false;
		} else {
			button.interactable = true;
		}
	}
}
