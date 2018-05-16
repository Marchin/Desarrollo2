using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour {
    TextMeshProUGUI scoreText;

    private void Awake() {
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = string.Format("Score: {0}",
            ScoreManager._instance.GetScore());
    }

}