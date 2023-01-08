using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour {
	public float health = 100f;
	float maxHealth = 100f;
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		if (health <= 0f)
			SceneManager.LoadScene(3);
	}
}
