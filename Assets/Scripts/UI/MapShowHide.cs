using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapShowHide : MonoBehaviour {

	public CanvasGroup MapHolder;

	void Update() {
		if (Input.GetKeyDown(KeyCode.Tab))
			MapHolder.alpha = 1f;
		if (Input.GetKeyUp(KeyCode.Tab))
			MapHolder.alpha = 0f;

	}
}
