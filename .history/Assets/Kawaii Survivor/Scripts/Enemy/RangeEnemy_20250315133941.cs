using TMPro;
using UnityEngine;
using System;
using UnityEditor.Rendering;
using Unity.Collections;


[RequireComponent(typeof(EnemyMovement), typeof(RanageEnemyAttack))]

public class RangeEnemy : Enemy
{

    [Header("Components")]
    private RanageEnemyAttack attack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        attack = GetComponent<RanageEnemyAttack>();
        attack.StorePlayer(player);
    }
    void Update()
    {
        if (CanAttack())
        {
            return;
        }
        ManageAttack();
        Vector3 scale = transform.localScale;
        scale.x = player.transform.position.x > transform.position.x ? 1 : -1;
        transform.localScale = scale;

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
