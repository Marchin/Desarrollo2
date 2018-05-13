using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
	[SerializeField] float _maxHealth;
	float _currHealth;
	float _score;

	void Awake() {
		_currHealth = _maxHealth;
		_score = 0f;
	}

	public void TakeDamage(float damage) {
		_currHealth -= damage;
		if (_currHealth <= 0f) {
			gameObject.SetActive(false);
		}
	}

	public void AddScore(float points) {
		_score += points;
	}
}