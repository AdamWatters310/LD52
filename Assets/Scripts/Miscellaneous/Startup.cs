using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startup : MonoBehaviour {
	private void Awake() {
#if UNITY_EDITOR
		Debug.Log("Ignoring usual startup routine");
	#else
		SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
	#endif
	}
}
