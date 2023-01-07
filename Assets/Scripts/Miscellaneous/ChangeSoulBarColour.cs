using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSoulBarColour : MonoBehaviour {
	// Start is called before the first frame update
	void Start() {
		GameEvents.current.OnGhostEnable += ChangeToGreen;
		GameEvents.current.OnGhostDisable += ChangeToBlue;
	}

	void ChangeToBlue() {
		gameObject.GetComponent<Image>().color = Color.blue;
	}

	void ChangeToGreen() {
		gameObject.GetComponent<Image>().color = Color.green;
	}
}
