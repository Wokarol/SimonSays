using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SingleMemoryButton : MonoBehaviour , IPointerDownHandler {

	public CanvasGroup canvasGroup;
	public Button button;
	public Image image;
	public Image patternImage;
	private static GameplayController gameplayController;
	public GameplayController GameplayController {
		get { return gameplayController; }
		set { gameplayController = value; }
	}
	private static AudioManager audioManager;
	public AudioManager AudioManager {
		get { return audioManager; }
		set { audioManager = value; }
	}
	[HideInInspector] public Color normalColor;
	[HideInInspector] public Color normalPatternColor;
	[HideInInspector] public int index;

	private void OnValidate () {
		if (canvasGroup == null) {
			canvasGroup = GetComponent<CanvasGroup>();
		}
		if (button == null) {
			button = GetComponent<Button>();
		}
	}

	public IEnumerator FakePressAnim (bool isHalfFlash) {
		if (isHalfFlash) {
			image.color = Color.Lerp(Color.white, normalColor, 0.5f);
			patternImage.color = Color.Lerp(Color.white, normalPatternColor, 0.5f);
		} else {
			image.color = Color.white;
			patternImage.color = Color.white;
		}
		yield return new WaitForSeconds(0.2f);
		image.color = normalColor;
		patternImage.color = normalPatternColor;
		yield return null;
	}

	public void ButtonActive(bool isActive) {
		if (isActive) {
			image.color = normalColor;
			patternImage.color = normalPatternColor;
		} else {
			image.color = Color.grey;
			patternImage.color = new Color(0.6f, 0.6f, 0.6f);
		}
	}

	public void OnPointerDown (PointerEventData eventData) {
		if (button.interactable) {
			PlayClick();
			gameplayController.RegisterPress(index);
		}
	}

	public void PlayClick () {
		//float pitchModifier = 0.8f + (index * 0.05f);
		audioManager.PlaySound("Click " + index);
	}

	public void AddPattern(int index, PatternPack patternPack, Color color) {
			// No pattern
		if (index == -1 || !(index >= 0 && index < patternPack.patterns.Length)) {
			patternImage.sprite = null;
			patternImage.color = Color.clear;
			normalPatternColor = Color.clear;
		}
			// Pattern accesible
		else if (index >= 0 && index < patternPack.patterns.Length) {
			patternImage.sprite = patternPack.patterns[index];
			patternImage.color = color;
			normalPatternColor = color;
		}
			// Starnge situation 
		else {
			Debug.LogError("DBW: GC: What the hell?");
		}
	}

	public void RandomRotatePattern () {
		float zRotation = Random.Range(0, 360);
		patternImage.rectTransform.rotation = Quaternion.Euler(0, 0, zRotation);
	}

}
