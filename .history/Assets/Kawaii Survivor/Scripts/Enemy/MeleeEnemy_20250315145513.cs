using TMPro;
using UnityEngine;
using System;

[RequireComponent(typeof(EnemyMovement))]
public class MeleeEnemy : Enemy
{


    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency = 1f;
    private float attackTimer = 0f;
    private float attackDelay = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        // Prevent Following& Attacking durring the spawn sequence
        // Calculate the attack delay based on the attack frequency
        attackDelay = 1f / attackFrequency;

    }


    // Update is called once per frame
    void Update()
    {
        if (CanAttack())
        {
            return;
        }

        if (attackTimer >= attackDelay)
        {
            TryAttack();
            attackTimer = 0f;
        }
        else
        {
            Wait();
        }
        movement.FollowPlayer();
    }


    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }


    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Debug.Log(distanceToPlayer);
        if (distanceToPlayer <= playerDetectionRadius)
        {
            // player.TakeDamage(1);
            // PassAway();
            Attack();
        }
    }

    private void Attack()
    {
        // Debug.Log("Dealing" + damage + "damage to the player...");
        attackTimer = 0f;
        player.TakeDamage(damage);

    }


}
