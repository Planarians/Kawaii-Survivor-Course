using TMPro;
using UnityEngine;
using System;


[RequireComponent(typeof(EnemyMovement), typeof(RanageEnemyAttack))]

public class RangeEnemy : Enemy
{

    [Header("Components")]
    private RanageEnemyAttack attack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attack = GetComponent<RanageEnemyAttack>();
        attack.StorePlayer(player);
    }
    void Update()
    {
        ManageAttack();
    }

    private void ManageAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > playerDetectionRadius)
        {
            movement.FollowPlayer();
        }
        else
        {
            TryAttack();
        }
    }


    private void TryAttack()
    {
        attack.AutoAim();
    }

    //用于检测Enemy检测范围
    private void OnDrawGizmos()
    {
        if (!gizmos) return;
        Gizmos.color = Color.magenta;
        //绘制Enemy检测范围
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);

    }
}
