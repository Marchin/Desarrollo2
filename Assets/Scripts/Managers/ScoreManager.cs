using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	public static ScoreManager _instance;
	int playerScore = 0;

	private void Awake() {
		if (_instance) {
			Destroy(gameObject);
		} else {
			_instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public int GetScore() {
		return playerScore;
	}

	public void SubmitScore(int score) {
		playerScore += score;
	}
}