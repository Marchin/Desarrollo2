using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {
	[SerializeField] PlayerStats playerStats;
	Image lifeBar;
	TextMeshProUGUI lifeText;
	float playerHealth;
	float maxHealth;

	private void Start() {
		lifeBar = GetComponent<Image>();
		lifeText = GetComponentInChildren<TextMeshProUGUI>();
		maxHealth = playerStats.GetHealth();
		playerHealth = maxHealth;
		lifeText.text = string.Format("{0}/{1}", playerHealth, maxHealth);
	}
	void Update() {
		if (playerHealth != playerStats.GetHealth()) {
			playerHealth = playerStats.GetHealth();
			lifeBar.fillAmount = playerHealth / maxHealth;
		lifeText.text = string.Format("{0}/{1}", playerHealth, maxHealth);
		}
	}
}