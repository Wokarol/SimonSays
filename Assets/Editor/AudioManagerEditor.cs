using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor {

	public override void OnInspectorGUI () {
		AudioManager audioManager = target as AudioManager;
		base.OnInspectorGUI();

		EditorGUILayout.Space();
		audioManager.testSoundName = EditorGUILayout.TextField("Sounds name", audioManager.testSoundName);
		audioManager.testPitchMod = EditorGUILayout.FloatField("Sounds pitch mod", audioManager.testPitchMod);
		if(GUILayout.Button("Test sound")) {
			audioManager.TestSound(audioManager.testSoundName, audioManager.testPitchMod);
		}
		if(GUILayout.Button("Delete test")) {
			DestroyImmediate(audioManager.testGo);
		}
	}

}
