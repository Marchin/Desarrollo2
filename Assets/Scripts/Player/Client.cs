using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour {
    PlayerStats playerStats;

    private void Awake() {
        playerStats = GetComponent<PlayerStats>();
        playerStats.lifeChanged.AddListener(Dead);
    }

    void Dead() {
        if (playerStats.GetHealth()== 0) {
            LevelManager._instantiate.Defeat();
        }
    }
}