using System;
using UnityEngine;
using UnityEngine.Pool;
public class RangeWeapon : Weapon
{

    [Header("Elements")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform shootingPoint;

    [Header("Bullet Pooling")]
    private ObjectPool<Bullet> bulletPool;


    void Start()
    {
        bulletPool = new ObjectPool<Bullet>(
            CreateFunction,
            ActionOnGet,
            ActionOnRelease,
            ActionOnDestroy,
            false,
            10
        );
    }
    // Update is called once per frame
    void Update()
    {
        AutoAim();
    }

    private Bullet CreateFunction()
    {
        Bullet bulletInstance = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity).GetComponent<Bullet>();
        bulletInstance.Configure(this);
        return bulletInstance;
    }

    private void ActionOnGet(Bullet bullet)
    {
        bullet.Reload();
        bullet.transform.position = shootingPoint.position;
        bullet.gameObject.SetActive(true);
    }

    private void ActionOnRelease(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void ReleaseBullet(Bullet bullet)
    {
        bulletPool.Release(bullet);
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

        int damage = GetDamage(out bool isCriticalHit);
        // Bullet bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Bullet bullet = bulletPool.Get();
        bullet.Shoot(damage, transform.up);
        // Vector2 direction = (player.GetCenter() - (Vector2)shootingoint.position).normalized;

        // //show the direction in the gizmos
        // gizmosDirection = direction;
        // Debug.Log("Shooting at player");

        // //Instantiate the bullet
        // Bullet bullet = bulletPool.Get();
        // bullet.Shoot(damage, direction);
    }
}


