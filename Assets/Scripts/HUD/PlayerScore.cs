using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour {
    [SerializeField] CharacterStats playerStats;
    TextMeshProUGUI scoreText;
    int playerScore;

    void Awake() {
        scoreText = GetComponent<TextMeshProUGUI>();
        playerStats.scoreChanged.AddListener(UpdateScore);
        playerScore = playerStats.GetScore();
        scoreText.text = "Score: " + playerScore.ToString();
    }

    void UpdateScore() {
        playerScore = playerStats.GetScore();
        scoreText.text = "Score: " + playerScore.ToString();
    }

}