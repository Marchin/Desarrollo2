using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour {
    [SerializeField] GameObject explosivePrefab;
    [SerializeField] int amount;
    [SerializeField] float rangoX;
    [SerializeField] float rangoZ;
    [Range(0f, 50f)]
    [SerializeField] float variance;
    [HideInInspector]
    public List<PickupPercentage> specialPickups = new List<PickupPercentage>();
    List<GameObject> pickupsList = new List<GameObject>();
    int amountLeft;

    private void Awake() {
        amountLeft = amount;
    }
    void Start() {
        foreach (PickupPercentage specialPickup in specialPickups) {
            int specialAmount = (int)Mathf.Round(amount * (specialPickup.percentage / 100));
            specialAmount = (int)(specialAmount + (specialAmount * (Random.Range(0f, variance) / 100f) *
                (((Random.Range(0, 10) % 2) > 0) ? 1 : -1)));
            for (int i = 0; i < specialAmount; i++) {
                Setup(specialPickup.pickup);
            }
            amountLeft -= specialAmount;
        }
        for (int i = 0; i < amountLeft; i++) {
            Setup(explosivePrefab);
        }
        InvokeRepeating("CheckSpawn", 0f, 5f);
    }

    void Setup(GameObject pickupType) {
        GameObject pickup = Instantiate(pickupType);
        pickup.transform.SetParent(transform);
        pickup.SetActive(false);
        pickupsList.Add(pickup);
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
            Random.Range(-rangoX, rangoX),
            0f,
            Random.Range(-rangoZ, rangoZ)
        );
        pickup.transform.position = transform.position + spawnOffset;
        pickup.SetActive(true);
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(rangoX * 2, 0f, rangoZ * 2));
    }
}