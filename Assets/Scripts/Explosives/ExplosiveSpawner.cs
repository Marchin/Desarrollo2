using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveSpawner : MonoBehaviour {
    [SerializeField] GameObject explosivePrefab;
    [SerializeField] int ammount;
    [SerializeField] float rango;
    List<GameObject> explosivesList;

	void Start () {
        explosivesList = new List<GameObject>();
        for (int i = 0; i < ammount; i++) {
            GameObject explosive = Instantiate(explosivePrefab);
            explosive.transform.SetParent(transform);
            explosive.SetActive(false);
            explosivesList.Add(explosive);
        }
        InvokeRepeating("CheckSpawn", 0f,5f);
	}

    void CheckSpawn() {
        foreach (GameObject explosive in explosivesList) {
            if (!explosive.activeInHierarchy) {
                Spawn(explosive);
            }
        }
    }

    void Spawn(GameObject explosive) {
        Vector3 spawnOffset = new Vector3(
            Random.Range(-rango, rango),
            0f,
            Random.Range(-rango, rango)
        );
        explosive.transform.position = transform.position + spawnOffset;
        explosive.SetActive(true);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(rango*2, 0f, rango*2));
    }
}
