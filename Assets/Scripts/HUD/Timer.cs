using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {
    [SerializeField] float maxTime = 100f;
    TimeSpan timeSpan;
    TextMeshProUGUI timerText;

    void Start() {
        timerText = GetComponent<TextMeshProUGUI>();
        timeSpan = TimeSpan.FromSeconds(maxTime);
        timerText.text = string.Format("{0}:{1}", Mathf.Floor(maxTime / 60f), Mathf.Floor(maxTime % 60f));
    }

    void Update() {
        if (maxTime <= 0f) {
            LevelManager._instantiate.Defeat();
        } else {
            maxTime -= Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(maxTime);
            timerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}