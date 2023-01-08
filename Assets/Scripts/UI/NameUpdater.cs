using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameUpdater : MonoBehaviour {

	public TextMeshProUGUI nameText;

	void Start() {
		GameEvents.current.OnRoomChange += ChangeName;
	}

	void ChangeName() {
		nameText.text = GlobalVars.current.currentRoomName;
	}
}
