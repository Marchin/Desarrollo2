using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {
    [SerializeField] GameObject pickupPrefab;
    [SerializeField] int ammount;
    [SerializeField] float rango;
    List<GameObject> pickupsList;

    void Start() {
        pickupsList = new List<GameObject>();
        for (int i = 0; i < ammount; i++) {
            GameObject pickup = Instantiate(pickupPrefab);
            pickup.transform.SetParent(transform);
            pickup.SetActive(false);
            pickupsList.Add(pickup);
        }
        InvokeRepeating("CheckSpawn", 0f, 5f);
    }

    void CheckSpawn() {
        foreach (GameObject pickup in pickupsList) {
            if (!pickup.activeInHierarchy) {
                Spawn(pickup);
            }
        }
    }

    void Spawn(GameObject pickup) {
        Vector3 spawnOffset = new Vector3(
            Random.Range(-rango, rango),
            0f,
            Random.Range(-rango, rango)
        );
        pickup.transform.position = transform.position + spawnOffset;
        pickup.SetActive(true);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(rango * 2, 0f, rango * 2));
    }
}