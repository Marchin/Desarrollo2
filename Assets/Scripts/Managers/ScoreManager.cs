using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	[SerializeField] TextMeshProUGUI enemiesText;
	[SerializeField] TextMeshProUGUI alliesText;
	public static ScoreManager _instance;
	List<GameObject> alliesList;
	List<GameObject> enemiesList;

	private void Awake() {
		if (_instance) {
			Destroy(gameObject);
		} else {
			_instance = this;
			enemiesList = new List<GameObject>();
			alliesList = new List<GameObject>();
		}
	}
	public void Register(GameObject soldier) {
		if (1 << soldier.layer == LayerMask.GetMask("Allies")) {
			alliesList.Add(soldier);
		} else if (1 << soldier.layer == LayerMask.GetMask("Enemies")) {
			enemiesList.Add(soldier);
		}
		alliesText.text = alliesList.Count.ToString();
		enemiesText.text = enemiesList.Count.ToString();
	}

	public void Remove(GameObject soldier) {
		if (1 << soldier.layer == LayerMask.GetMask("Allies")) {
			alliesList.Remove(soldier);
			alliesText.text = alliesList.Count.ToString();
		} else if (1 << soldier.layer == LayerMask.GetMask("Enemies")) {
			enemiesList.Remove(soldier);
			enemiesText.text = enemiesList.Count.ToString();
		}
		if (enemiesList.Count == 0) {
			LevelManager._instantiate.Victory();
		}
	}
}