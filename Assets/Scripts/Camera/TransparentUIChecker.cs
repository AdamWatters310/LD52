using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentUIChecker : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.CompareTag("Player")) {
			GameEvents.current.MakeUITransparent();
		}
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			GameEvents.current.MakeUIOpaque();
		}
	}
}
