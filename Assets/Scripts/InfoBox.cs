using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoBox :MonoBehaviour {

	[SerializeField] InfoTexts infoTexts;
	[SerializeField] TextMeshProUGUI textMesh;

	[ContextMenu("RefreshText")]
	public void RefreshInfo () {
		string modeText = "";
		string difficultyText = "";
		string playerPrefsMode = PlayerPrefs.GetString("GameMode");
		string playerprefsDiff = PlayerPrefs.GetString("DifficultyLevel");

		if(playerPrefsMode == "Repeat") {
			modeText = infoTexts.repeat;
		} else if (playerPrefsMode == "Memory") {
			modeText = infoTexts.memory;
		}

		if(playerprefsDiff == "1H") {
			difficultyText = infoTexts.oneH;
		} else if (playerprefsDiff == "3H") {
			difficultyText = infoTexts.threeH;
		} else if (playerprefsDiff == "RegH") {
			difficultyText = infoTexts.regH;
		}

		textMesh.text = modeText + "\n\n" + difficultyText;
	}

	[System.Serializable]
	class InfoTexts{
		[TextArea]
		public string memory;
		[TextArea]
		public string repeat;
		[Space]
		[Space]
		[TextArea]
		public string oneH;
		[TextArea]
		public string threeH;
		[TextArea]
		public string regH;
	}
}