using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] protected LayerMask enemyMask;


    [Header("Attack")]
    [SerializeField] protected int damage;
    [SerializeField] protected float attackDelay = 0.3f;
    protected float attackTimer = 0f;

    [Header("Animations")]
    //目标旋转速度
    [SerializeField] protected float aimLerp = 12f;
    [SerializeField] protected Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    [NaughtyAttributes.Button]





    protected Enemy GetClosestEnemy()
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

    protected int GetDamage(out bool isCriticalHit)
    {
        isCriticalHit = false;

        //when I want the cristical to happen
        if (UnityEngine.Random.Range(0, 101) <= 50)
        {
            isCriticalHit = true;
            return damage * 2;
        }

        return damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);

    }
}