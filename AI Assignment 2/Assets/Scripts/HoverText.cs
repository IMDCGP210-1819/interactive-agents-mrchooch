using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverText : MonoBehaviour {
	private RectTransform rectTransform;

	private void Start() {
		rectTransform = GetComponent<RectTransform>();
	}

	void Update() {
		transform.position = Input.mousePosition;
	}
}
