using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class VersionTextController : MonoBehaviour {
	TextMeshProUGUI textMesh;
	private void Start () {
		textMesh = GetComponent<TextMeshProUGUI>();
		textMesh.text = "Version " + Application.version + " made by Wokarol";
	}
}
