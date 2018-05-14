using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageManager : MonoBehaviour {
	[SerializeField]
	Animator messageAnimator;
	[SerializeField]
	TextMeshProUGUI textMesh;
	[SerializeField]
	Image backgroundImage;

	public static MessageManager instance;
	private void Awake () {
		instance = this;
	}

	public void Message(string text, Color color) {
		messageAnimator.SetTrigger("Deactivate");
		textMesh.text = text;
		backgroundImage.color = color;
		messageAnimator.SetTrigger("Activate");
	}
}
