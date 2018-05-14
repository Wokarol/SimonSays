using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternButton : MonoBehaviour {
	[HideInInspector] public int index;
	private static MenuController menuController;
	private static PatternButtonsManager patternButtonsManager;

	public Image patternImage;
	public Button button;

	public void ReggisterPress () {
		menuController.ClickSound();
		menuController.SetPattern(index);
		patternButtonsManager.RefreshStates();
	}

	public void RefreshState () {
			if (PlayerPrefs.GetInt("SelectedPattern") == index) {
				button.interactable = false;
			} else {
				button.interactable = true;
			}	
	}

	public void SetManagers(MenuController _menu, PatternButtonsManager _pattern) {
		menuController = _menu;
		patternButtonsManager = _pattern;
	}

	public void AddPattern (int index, PatternPack patternPack) {
		if (index == -1 || !(index >= 0 && index < patternPack.patterns.Length)) {
			patternImage.sprite = null;
			patternImage.color = Color.clear;
		}
		// Pattern accesible
		else if (index >= 0 && index < patternPack.patterns.Length) {
			patternImage.sprite = patternPack.patterns[index];
			patternImage.color = Color.white;
		}
		// Starnge situation 
		else {
			Debug.LogError("DBW: GC: What the hell?");
		}
	}
}
