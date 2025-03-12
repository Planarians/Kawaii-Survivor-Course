using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    enum State
    {
        Idle,
        Attack
    }

    private State state;

    [Header("Elements")]
    [SerializeField] private Transform hitDetectionTransform;
    [SerializeField] private float hitDetectionRadius = 0.3f;

    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;


    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private List<Enemy> damagedEnemies = new List<Enemy>();

    [Header("Animations")]
    //目标旋转速度
    [SerializeField] private float aimLerp = 12f;
    [SerializeField] private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;
            case State.Attack:
                Attacking();
                break;
        }


    }

    [NaughtyAttributes.Button]
    private void StartAttack()
    {
        animator.Play("Attack");
        state = State.Attack;
    }
    private void Attacking()
    {
        Attack();
    }

    private void StopAttack()
    {
        state = State.Idle;
    }

    private void Attack()
    {
        // 检测是否在攻击范围内
        Enemy[] enemies = Physics2D.OverlapCircleAll(hitDetectionTransform.position, hitDetectionRadius, enemyMask).Select(x => x.GetComponent<Enemy>()).ToArray();

        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                // 如果敌人不在列表中，则添加到列表中
                if (!damagedEnemies.Contains(enemy))
                {
                    damagedEnemies.Add(enemy);
                }
                // 如果敌人已经在列表中，则不重复添加
                else
                {
                    continue;
                }

                enemy.TakeDamage(damage);
            }
        }
    }


    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();

        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
        }
        // 插值旋转
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
    }


    private Enemy GetClosestEnemy()
    {



        Enemy[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask).Select(x => x.GetComponent<Enemy>()).ToArray();

        int closestEnemyIndex = -1;
        float minDistance = Mathf.Infinity;

        if (enemies.Length == 0)
        {
            transform.up = Vector3.up;
            return null;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemyIndex = i;
            }
        }
        // If there is an enemy, set the enemy to the closest enemy
        if (closestEnemyIndex != -1)
        {

            return enemies[closestEnemyIndex];
        }
        // If there is no enemy, set the weapon to the up vector
        else
        {

            return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitDetectionTransform.position, hitDetectionRadius);
    }
}