using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools : MonoBehaviour
{
	private void OnDrawGizmos() {
		for(int i = 0; i < 30; i++) {
			for(int j = 0; j < 30; j++) {
				Gizmos.color = Color.cyan;
				Gizmos.DrawWireCube(new Vector3(i * 32, j * 18, 0), new Vector3(32, 18, 1));
			}
		}
	}
}
