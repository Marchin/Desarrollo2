using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {
	[SerializeField] PlayerStats playerStats;
	Image lifeBar;
	TextMeshProUGUI lifeText;
	int playerHealth;
	int maxHealth;

	private void Start() {
		lifeBar = GetComponent<Image>();
		lifeText = GetComponentInChildren<TextMeshProUGUI>();
		playerStats.lifeChanged.AddListener(UpdateLife);
		maxHealth = playerStats.GetHealth();
		playerHealth = maxHealth;
		lifeText.text = string.Format("{0}/{1}", playerHealth, maxHealth);
	}
	void UpdateLife() {
		if (playerHealth != playerStats.GetHealth()) {
			playerHealth = playerStats.GetHealth();
			lifeBar.fillAmount = (float)playerHealth / (float)maxHealth;
			lifeText.text = string.Format("{0}/{1}", playerHealth, maxHealth);
		}
	}
}