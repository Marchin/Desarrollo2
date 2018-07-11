using UnityEngine;
using UnityEngine.Events;

public class CharacterStats : MonoBehaviour {
	[SerializeField] int _maxHealth;
	public UnityEvent lifeChanged;
	int _currHealth;

	void Awake() {
		_currHealth = _maxHealth;
	}

	private void Start() {
		ScoreManager._instance.Register(gameObject);
	}

	public void TakeDamage(int damage) {
		Animator anim;
		_currHealth -= damage;
		lifeChanged.Invoke();
		if (_currHealth <= 0) {
			ScoreManager._instance.Remove(gameObject);
			gameObject.SetActive(false);
		} else if (anim = GetComponent<Animator>()) {
			anim.SetTrigger("Hit");
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

}