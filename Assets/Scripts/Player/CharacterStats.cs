using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour {
	[SerializeField] int _maxHealth;
	public UnityEvent scoreChanged;
	public UnityEvent lifeChanged;
	Player player;
	int _currHealth;
	int _score;

	void Awake() {
		player = GetComponent<Player>();
		_currHealth = _maxHealth;
		_score = 0;
	}

	public void TakeDamage(int damage) {
		_currHealth -= damage;
		lifeChanged.Invoke();
		if (_currHealth <= 0 && !player) {
			//gameObject.SetActive(false);
		}
	}

	public void Heal(int heal) {
		if (_currHealth < 100) {
			_currHealth += heal;
			if (_currHealth > 100) {
				_currHealth = 100;
			}
			lifeChanged.Invoke();
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