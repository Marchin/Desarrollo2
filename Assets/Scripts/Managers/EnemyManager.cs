using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour {
    static EnemyManager _instance;
    List<GameObject> EnemyList;
    
    private void Awake() {
        if (_instance) {
            Destroy(gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Register(GameObject Enemy) {
        EnemyList.Add(Enemy);
    }

    public void Remove(GameObject Enemy) {
        EnemyList.Remove(Enemy);
        if (EnemyList.Count == 0) {
            //victoria
        }
    }

}
