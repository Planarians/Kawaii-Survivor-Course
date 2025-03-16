using System;
using UnityEngine;

public class RangeWeapon : Weapon
{

    [Header("Elements")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform shootingPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();

        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
            return;
        }
        // 插值旋转
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
        ManageShooting();
    }

    private void ManageShooting()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackDelay)
        {
            Shoot();
            attackTimer = 0f;
        }
    }

    private void Shoot()
    {

        Bullet bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        bullet.Shoot(damage, transform.up);
        // Vector2 direction = (player.GetCenter() - (Vector2)shootingoint.position).normalized;

        // //show the direction in the gizmos
        // gizmosDirection = direction;
        // Debug.Log("Shooting at player");

        // //Instantiate the bullet
        // EnemyBullet bullet = bulletPool.Get();
        // bullet.Shoot(damage, direction);
    }
}
