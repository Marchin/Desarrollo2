using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private void Start() {
        EnemyManager._instance.Register(gameObject);
    }

    private void OnDisable() {
        EnemyManager._instance.Remove(gameObject);
    }

    public void TakeDamage() {
        gameObject.SetActive(false);
    }
}