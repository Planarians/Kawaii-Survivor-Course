using TMPro;
using UnityEngine;
using System;


[RequireComponent(typeof(EnemyMovement), typeof(RanageEnemyAttack))]

public class RangeEnemy : Enemy
{

    [Header("Components")]
    private RanageEnemyAttack attack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    new void Start()
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

}
