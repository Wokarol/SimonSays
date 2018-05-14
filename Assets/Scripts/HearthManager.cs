using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthManager : MonoBehaviour {
	public static HearthManager instance;
	private void Awake () {
		instance = this;
	}
	public System.Action<int, bool> onHearthValueChange;

	private int hearthValue;
	public int HearthValue {
		set {
			hearthValue = value;
			RefreshText();
		}
		get {
			return hearthValue;
		}
	}

	private bool regenerable;
	public bool Regenerable {
		get {
			return regenerable;
		}

		set {
			regenerable = value;
			RefreshText();
		}
	}

	public bool ActiveDisplay {
		get {
			return activeDisplay;
		}

		set {
			activeDisplay = value;
		}
	}

	private bool activeDisplay;


	// Use this for initialization
	void Start () {
		RefreshText();
	}

	public  void Regenerate () {
		if (regenerable) {
			HearthValue = 1;
		}
	}

	public void RefreshText () {
		if (onHearthValueChange != null) {
			onHearthValueChange(hearthValue, regenerable);
		}
	}
}
