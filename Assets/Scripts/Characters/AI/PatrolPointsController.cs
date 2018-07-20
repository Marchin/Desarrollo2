using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsController : MonoBehaviour {
    [SerializeField] PatrolPointsSet m_pps;

    public bool RequestPatrolPoint(ref int aiCurrentPoint, ref Vector3 newPosition, bool isEnemy) {
        int point = Random.Range(0, m_pps.patrolPoints.Length - 1);
        List<int> occupied = (isEnemy? m_pps.occupiedEnemies : m_pps.occupiedAllies);
        if (IsPointFree(occupied, point, aiCurrentPoint)) {
            newPosition = m_pps.patrolPoints[point].position;
            aiCurrentPoint = point;
            return true;
        } else {
            return false;
        }
    }

    bool IsPointFree(List<int> list, int point, int aiCurrentPoint) {
        if (!list.Contains(point)) {
            list.Remove(aiCurrentPoint);
            list.Add(point);
            return true;
        } else {
            return false;
        }
    }
}