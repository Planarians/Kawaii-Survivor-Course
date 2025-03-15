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
    void Start()
    {
        // Prevent Following& Attacking durring the spawn sequence
        // Calculate the attack delay based on the attack frequency
        attackDelay = 1f / attackFrequency;
    }



    // Update is called once per frame
    void Update()
    {
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

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        healthText.text = health.ToString();

        onDamageTaken?.Invoke(damage, transform.position);

        if (health <= 0)
        {
            PassAway();
        }
    }

    private void Attack()
    {
        // Debug.Log("Dealing" + damage + "damage to the player...");
        attackTimer = 0f;

        player.TakeDamage(damage);

    }

    private void PassAway()
    {
        //Unparent the particle
        passAwayParticle.transform.SetParent(null);
        passAwayParticle.Play();
        Destroy(gameObject);
    }

    //用于检测Enemy检测范围
    private void OnDrawGizmos()
    {
        if (!gizmos) return;
        Gizmos.color = Color.red;
        //绘制Enemy检测范围
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);

    }

}
