using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeButton : MonoBehaviour {

	[SerializeField] Button button;
	[SerializeField] string myValue;

	public void Refresh (string value) {
		if(value == myValue) {
			button.interactable = false;
		} else {
			button.interactable = true;
		}
	}

	private void OnValidate () {
		if (button == null)
			button = GetComponent<Button>();
	}
}
