using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSender : MonoBehaviour {
	[TextArea(3, 10)]
	public string text;
	
	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.CompareTag("Player")) {
			GlobalVars.current.currentDialogue = text;
			GameEvents.current.TriggerDialogue();
		}
	}

}
