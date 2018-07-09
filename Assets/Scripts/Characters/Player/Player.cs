using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    CharacterStats playerStats;

    private void Awake() {
        playerStats = GetComponent<CharacterStats>();
        playerStats.lifeChanged.AddListener(Dead);
    }

    void Dead() {
        if (playerStats.GetHealth()== 0) {
            LevelManager._instantiate.Defeat();
        }
    }
}