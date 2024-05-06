using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private StateEnemy currentState;
    private EnemyPathFinding pathFinding;

    public void Awake()
    {
        pathFinding = GetComponent<EnemyPathFinding>();
        currentState = StateEnemy.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while(currentState == StateEnemy.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            pathFinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

}
