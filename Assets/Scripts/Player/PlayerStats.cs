using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour {
	[SerializeField] int _maxHealth;
	public UnityEvent scoreChanged;
	public UnityEvent lifeChanged;
	Client client;
	int _currHealth;
	int _score;

	void Awake() {
		client = GetComponent<Client>();
		_currHealth = _maxHealth;
		_score = 0;
	}

	public void TakeDamage(int damage) {
		_currHealth -= damage;
		lifeChanged.Invoke();
		if (_currHealth <= 0 && !client) {
			gameObject.SetActive(false);
		}
	}

	public int GetHealth() {
		return _currHealth;
	}

	public void AddScore(int points) {
		_score += points;
		ScoreManager._instance.SubmitScore(points);
		scoreChanged.Invoke();
	}

	public int GetScore() {
		return _score;
	}
}