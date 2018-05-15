using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
    TimeSpan timeSpan;
    float maxTime = 100f;
    TextMeshProUGUI timerText;

    void Start() {
        timerText = GetComponent<TextMeshProUGUI>();
        timeSpan = TimeSpan.FromSeconds(maxTime);
        timerText.text = string.Format("{0}:{1}", Mathf.Floor(maxTime / 60f), Mathf.Floor(maxTime % 60f));
    }


    void Update() {
        if (maxTime <= 0f) {
            //Derrota
        } else {
            maxTime -= Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(maxTime);
            timerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}
