using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    CharacterStats playerStats;

    private void Start() {
        EnemyManager._instance.Register(gameObject);
        playerStats = GetComponent<CharacterStats>();
    }

    private void Update() {
        if (playerStats.GetHealth()<= 0) {
            EnemyManager._instance.Remove(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage() {
        playerStats.TakeDamage(10);
    }
}