using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparencyListener : MonoBehaviour {
	public CanvasGroup canvasGroup;
	// Start is called before the first frame update
	void Start() {
		GameEvents.current.OnUIHeadOpaque += makeSelfOpaque;
		GameEvents.current.OnUIHeadTransparent += makeSelfTransparent;

	}

	void makeSelfTransparent() {
		canvasGroup.alpha = .2f;
	}

	void makeSelfOpaque() {
		canvasGroup.alpha = 1f;
	}
}
