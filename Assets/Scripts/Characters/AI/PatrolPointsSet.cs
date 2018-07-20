using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolPointsSet", menuName = "AI/PatrolPoints Set")]
public class PatrolPointsSet : ScriptableObject {
    public Transform[] patrolPoints;
    [HideInInspector]
    public List<int> occupiedAllies;
    public List<int> occupiedEnemies;

    private void OnEnable() {
        occupiedAllies = new List<int>();
        occupiedEnemies = new List<int>();
    }
}