using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatternButtonsManager : MonoBehaviour {

	[SerializeField] PatternButton patternButtonPrefab;
	[SerializeField] PatternPack patternPack;

	MenuController menuController;
	GridLayoutGroup gridLayoutGroup;

	List<PatternButton> patternButtons;

	void Start () {
		menuController = MenuController.instance;

		ResetButtons();

		// Resizing
		gridLayoutGroup = GetComponent<GridLayoutGroup>();
		int paddingTop = gridLayoutGroup.padding.top;
		int paddingBottom = gridLayoutGroup.padding.bottom;
		float cellHeight = gridLayoutGroup.cellSize.y;
		float spacingY = gridLayoutGroup.spacing.y;

		int rowsCount = (int)Mathf.Ceil((float)patternButtons.Count / gridLayoutGroup.constraintCount);
		int spacesCount = rowsCount - 1;

		float totalHeight = paddingTop + paddingBottom + (rowsCount*cellHeight) + (spacesCount*spacingY);
		RectTransform rt = GetComponent<RectTransform>();
		if(rt.sizeDelta.y > totalHeight) {
			totalHeight = rt.sizeDelta.y;
		}
		rt.sizeDelta = new Vector2(rt.sizeDelta.x, totalHeight);
	}

	void ResetButtons () {
		patternButtons = new List<PatternButton>();

		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++) {
			Destroy(transform.GetChild(i).gameObject);
		}

		// Adding "NONE" button
		PatternButton _nonePatternButton = Instantiate(patternButtonPrefab, transform);
		_nonePatternButton.transform.name = ("Pattern_none");
		_nonePatternButton.AddPattern(-1, patternPack);
		_nonePatternButton.SetManagers(menuController, this);
		_nonePatternButton.index = -1;
		patternButtons.Add(_nonePatternButton);

		// Adding button by pattern
		int patternsCount = patternPack.patterns.Length;
		for (int i = 0; i < patternsCount; i++) {
			PatternButton _patternButton = Instantiate(patternButtonPrefab, transform);
			_patternButton.transform.name = ("Pattern_" + i);
			_patternButton.AddPattern(i, patternPack);
			_patternButton.index = i;
			patternButtons.Add(_patternButton);
		}

	}

	public void RefreshStates () {
		if (!PlayerPrefs.HasKey("SelectedPattern")) {
			PlayerPrefs.SetInt("SelectedPattern", -1);
		}
		int patternButtonsCount = patternButtons.Count;
		for (int i = 0; i < patternButtonsCount; i++) {
			patternButtons[i].RefreshState();
		}
	}
}
