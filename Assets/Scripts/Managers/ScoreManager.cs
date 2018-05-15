using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager _instantiate;
	int playerScore;

	private void Awake() {
		if (_instantiate) {
			Destroy(gameObject);
		} else {
			_instantiate = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void SubmitScore(int score) {
		playerScore += score;
	}
}