using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour {
	public TextMeshProUGUI textDisplay;
	public CanvasGroup group;
	bool showingDialogue = false;

	void Start() {
		GameEvents.current.OnDialogueTrigger += ShowText;
	}

	void ShowText() {
		showingDialogue = true;
		textDisplay.text = GlobalVars.current.currentDialogue;
		group.alpha = 1.0f;
	}

	private void Update() {
		if (showingDialogue && Input.GetKeyDown(KeyCode.A)) {
			showingDialogue = false;
			group.alpha = 0.0f;
		}
	}

}
