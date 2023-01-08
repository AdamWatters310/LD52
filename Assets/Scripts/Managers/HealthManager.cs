using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour {
	public float health = 100f;
	float maxHealth = 100f;
	public TextMeshProUGUI healthText;
	void Start() {
		GameEvents.current.OnHealthIncrease += IncreaseHealth;
		GameEvents.current.OnHealthDecrease += DecreaseHealth;
		GameEvents.current.OnHealthBigDecrease += DecreaseHealthBig;
		GameEvents.current.OnHealthUpdated += IncreaseMaxHealth;
		GameEvents.current.OnRefillHealth += RefillHealth;
	}

	// Update is called once per frame
	void Update() {
		if (health <= 0f)
			SceneManager.LoadScene(3);
		health = Mathf.Clamp(health, 0f, maxHealth);
		healthText.text = health.ToString();
	}

	void IncreaseHealth() {
		health += 10f;
	}

	void DecreaseHealth() {
		health -= 10f;
	}

	void DecreaseHealthBig() {
		health -= 45f;
	}

	void RefillHealth() {
		health = maxHealth;
	}

	void IncreaseMaxHealth() {
		maxHealth += 25f;
		health = maxHealth;
	}
}
