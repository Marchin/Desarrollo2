using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour {
    public static EnemyManager _instance;
    List<GameObject> EnemyList;

    private void Awake() {
        if (_instance) {
            Destroy(gameObject);
        } else {
            _instance = this;
            EnemyList = new List<GameObject>();
        }
    }

    public void Register(GameObject Enemy) {
        EnemyList.Add(Enemy);
    }

    public void Remove(GameObject Enemy) {
        EnemyList.Remove(Enemy);
        if (EnemyList.Count == 0) {
            LevelManager._instantiate.Victory();
        }
    }

}