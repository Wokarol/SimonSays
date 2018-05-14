using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonOffsetHandler:MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler {

	public GameObject target;
	public Vector3 offset;
	public Selectable button;

	[SerializeField] bool offsetAdded;
	bool pressingButton;
	bool overButton;

	private void Update () {
		if(!button.interactable || (pressingButton && overButton)) {
			if (!offsetAdded) {
				target.transform.position += offset;
				offsetAdded = true;
			}
		} else {
			if (offsetAdded) {
				target.transform.position -= offset;
				offsetAdded = false;
			}
		}
	}

	public void OnPointerDown (PointerEventData eventData) {
		pressingButton = true;
	}

	public void OnPointerUp (PointerEventData eventData) {
		pressingButton = false;
	}

	public void OnPointerEnter (PointerEventData eventData) {
		overButton = true;
	}

	public void OnPointerExit (PointerEventData eventData) {
		overButton = false;
	}
}
