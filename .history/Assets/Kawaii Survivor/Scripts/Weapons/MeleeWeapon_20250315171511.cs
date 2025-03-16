using UnityEngine;
using System.Collections.Generic;

public class MeleeWeapon : Weapon
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
    [SerializeField] private BoxCollider2D hitCollider;

    [Header("Attack")]

    [SerializeField] private List<Enemy> damagedEnemies = new List<Enemy>();


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

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();

        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageAttackTimer();

        }
        // 插值旋转
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
        IncrementAttackTimer();
    }

    private void ManageAttackTimer()
    {
        IncrementAttackTimer();

        if (attackTimer >= attackDelay)
        {
            attackTimer = 0f;

            StartAttack();
        }

    }

    private void IncrementAttackTimer()
    {
        attackTimer += Time.deltaTime;
    }
    private void StartAttack()
    {
        animator.Play("Attack");
        state = State.Attack;
        damagedEnemies.Clear();

        // 设置动画速度
        animator.speed = 1f / attackDelay;
    }
    private void Attacking()
    {
        Attack();
    }

    private void StopAttack()
    {
        state = State.Idle;
        damagedEnemies.Clear();
    }

    private void Attack()
    {
        // 检测是否在攻击范围内
        // Enemy[] enemies = Physics2D.OverlapCircleAll(hitDetectionTransform.position, hitDetectionRadius, enemyMask).Select(x => x.GetComponent<Enemy>()).ToArray();
        Enemy[] enemies = Physics2D.OverlapBoxAll
        (
            hitDetectionTransform.position,
            // 获取碰撞器的大小
            hitCollider.bounds.size,
            // 获取碰撞器的位置 
            hitDetectionTransform.localEulerAngles.z,
            // 获取碰撞器所在的层
            enemyMask)
        .Select(x => x.GetComponent<Enemy>()).ToArray();

        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                // 如果敌人不在列表中，则添加到列表中
                if (!damagedEnemies.Contains(enemy))
                {
                    enemy.TakeDamage(damage);

                    damagedEnemies.Add(enemy);
                }
                // 如果敌人已经在列表中，则不重复添加
                else
                {
                    continue;
                }

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);

        if (hitDetectionTransform == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitDetectionTransform.position, hitDetectionRadius);
    }

}
