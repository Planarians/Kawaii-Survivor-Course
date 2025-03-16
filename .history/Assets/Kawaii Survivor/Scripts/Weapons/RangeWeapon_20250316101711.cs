using System;
using UnityEngine;
using UnityEngine.Pool;
public class RangeWeapon : Weapon
{

    [Header("Elements")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform shootingPoint;

    [Header("Bullet Pooling")]
    private ObjectPool<EnemyBullet> bulletPool;

    bulletPool = new ObjectPool<EnemyBullet>(
              CreateFunction,
              ActionOnGet,
              ActionOnRelease,
              ActionOnDestroy,
            false,
            10
        );


           private EnemyBullet CreateFunction()
    {
        EnemyBullet bulletInstance = Instantiate(bulletPrefab, shootingoint.position, Quaternion.identity).GetComponent<EnemyBullet>();
        bulletInstance.Configure(this);
        return bulletInstance;
    }

    private void ActionOnGet(EnemyBullet enemyBullet)
    {
        enemyBullet.Reload();
        enemyBullet.transform.position = shootingoint.position;
        enemyBullet.gameObject.SetActive(true);
    }

    private void ActionOnRelease(EnemyBullet enemyBullet)
    {
        enemyBullet.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(EnemyBullet enemyBullet)
    {
        Destroy(enemyBullet.gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AutoAim();
    }

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();

        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            // 插值旋转
            transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
            ManageShooting();
            return;
        }
        transform.up = targetUpVector;

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
