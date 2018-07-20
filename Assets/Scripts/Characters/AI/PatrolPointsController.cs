using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointsController : MonoBehaviour {
    [SerializeField] PatrolPointsSet m_pps;

    public bool RequestPatrolPoint(ref int aiCurrentPoint, ref Vector3 newPosition, bool isEnemy) {
        bool success = false;
        int point = -1;
        List<int> occupied = (isEnemy? m_pps.occupiedEnemies : m_pps.occupiedAllies);
        for (int i = 0; i < m_pps.patrolPoints.Length; i++) {
            if (point == -1 || (Vector3.Distance(newPosition, m_pps.patrolPoints[i].position) <
                    (Vector3.Distance(newPosition, m_pps.patrolPoints[(point < 0 ? 0 : point)].position)))) {

                if (IsPointFree(occupied, i, aiCurrentPoint)) {
                    point = i;
                    newPosition = m_pps.patrolPoints[point].position;
                    aiCurrentPoint = point;
                    success = true;
                }
            }
        }
        return success;
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