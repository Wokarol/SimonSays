using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public static MenuController instance;
	private void Awake () {
		instance = this;
	}

	[SerializeField] Animator menuAnimator;
	[SerializeField] GameObject loadingScreen;
	[SerializeField] DebugPanelManager debugManager;

	AudioManager audioManager;

	private void Start () {
		audioManager = AudioManager.instance;
		Time.timeScale = 1f;
	}

	private void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			menuAnimator.SetTrigger("BackTrigger");
		}
	}

	public void StartScene (string sceneName) {
		Time.timeScale = 0f;
		loadingScreen.SetActive(true);
		SceneManager.LoadScene(sceneName);
	}

	public void ExitGame () {
		Application.Quit();
	}

	public void ClickSound () {
		AudioManager.instance.PlaySound("Click Standard");
	}

	public void SetPattern (int patternIndex) {
		PlayerPrefs.SetInt("SelectedPattern", patternIndex);
	}

	public void Trigger (string trigger) {
		menuAnimator.SetTrigger(trigger);
	}

	// Toggles
	public void SlowerFlashing(bool state) {
		PlayerPrefs.SetInt("IsSlowerFlashingAfterMistake", ((state) ? 1 : 0));
	}
	public void IsSoundOn (bool state) {
		PlayerPrefs.SetInt("IsSoundOn", ((state) ? 1 : 0));
		audioManager.EnabledSound = state;
		ClickSound();
	}
	public void IsDebugOn (bool state) {
		PlayerPrefs.SetInt("IsDebugOn", ((state) ? 1 : 0));
		debugManager.CheckActive();
	}
	public void SetNewDebugCodeValue (string value) {
		if(value == "") {
			PlayerPrefs.DeleteKey("DebugCodeValue");
		} else {
			PlayerPrefs.SetInt("DebugCodeValue", int.Parse(value));
		}
	}

	// Sliders
	public void SetVolume (float volume) {
		if(audioManager == null) {
			return;
		}
		audioManager.Volume = volume;
	}

	[ContextMenu("Delete all keys")]
	public void DeleteAllKeys () {
		PlayerPrefs.DeleteAll();
	}

}
