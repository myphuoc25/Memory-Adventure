using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCoolDown = 2f;
    [SerializeField] private bool stopMovingWhiteAttacking = false;

    private StateEnemy currentState;
    private EnemyPathFinding pathFinding;
    private bool canAttack = true;
    private Vector2 roamPosition;
    private float timeRoaming = 0;

    public void Awake()
    {
        pathFinding = GetComponent<EnemyPathFinding>();
        currentState = StateEnemy.Roaming;
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch(currentState)
        {
            default:
            case StateEnemy.Roaming:
                Roaming();
            break;
            case StateEnemy.Attacking:
                Attacking();
            break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        pathFinding.MoveTo(roamPosition);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            currentState = StateEnemy.Attacking;
        }

        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            currentState = StateEnemy.Roaming;
        }

        if (attackRange != 0 && canAttack){
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if(stopMovingWhiteAttacking)
            {
                pathFinding.StopMoving();
            } else
            {
                pathFinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCoolDown());
        }
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

}
