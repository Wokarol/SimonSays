using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoldedToggle : MonoBehaviour {

	[SerializeField] Toggle toggle;
	[SerializeField] Sprite notPressedSprite;
	[SerializeField] Sprite isPressedSprite;
	[SerializeField] Color switchedOffColor;
	[SerializeField] Color switchedOnColor;

	bool lastStateIsClicked;

	Image buttonImage;

	// Use this for initialization
	void Start () {
		buttonImage = toggle.targetGraphic as Image;
		lastStateIsClicked = toggle.isOn;

		SetNewState();
	}
	
	// Update is called once per frame
	void Update () {
		if (toggle.isOn != lastStateIsClicked) {
			SetNewState();
			lastStateIsClicked = toggle.isOn;
		}
	}

	void SetNewState () {
		if (toggle.isOn) {
			buttonImage.sprite = isPressedSprite;
			buttonImage.color = switchedOnColor;
			toggle.spriteState = new SpriteState {
				highlightedSprite = isPressedSprite,
				pressedSprite = toggle.spriteState.pressedSprite,
				disabledSprite = toggle.spriteState.disabledSprite
			};

		} else {
			buttonImage.sprite = notPressedSprite;
			buttonImage.color = switchedOffColor;
			toggle.spriteState = new SpriteState {
				highlightedSprite = notPressedSprite,
				pressedSprite = toggle.spriteState.pressedSprite,
				disabledSprite = toggle.spriteState.disabledSprite
			};
		}
	}
}
