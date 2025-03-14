using UnityEngine;
using UnityEngine.Pool;

public class RanageEnemyAttack : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Transform shootingoint;
    [SerializeField] private GameObject bulletPrefab;
    private Player player;

    [Header("Settings")]

    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency = 1f;

    private float attackTimer = 0f;
    private float attackDelay = 0f;

    [Header("Bullet Pooling")]
    private ObjectPool<EnemyBullet> bulletPool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = attackDelay;

        bulletPool = new ObjectPool<EnemyBullet>(
            CreateFunction,
            ActionOnGet,
            ActionOnRelease,
            ActionOnDestroy,
            false,
            10
        );
    }

    private EnemyBullet CreateFunction()
    {
        EnemyBullet bulletInstance = Instantiate(bulletPrefab, shootingoint.position, Quaternion.identity).GetComponent<EnemyBullet>();
        bulletInstance.Configure(this);
        return bulletInstance;
    }

    private void ActionOnGet(EnemyBullet enemyBullet)
    {
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
    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyHitCallback;
    }
    void Update()
    {

    }

    public void StorePlayer(Player player)
    {
        this.player = player;
    }

    // Update is called once per frame

    public void AutoAim()
    {

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
    Vector2 gizmosDirection;
    private void Shoot()
    {
        Vector2 direction = (player.GetCenter() - (Vector2)shootingoint.position).normalized;

        //show the direction in the gizmos
        gizmosDirection = direction;
        Debug.Log("Shooting at player");

        //Instantiate the bullet
        GameObject bulletInstance = Instantiate(bulletPrefab, shootingoint.position, Quaternion.identity);
        bulletInstance.GetComponent<EnemyBullet>().Shoot(damage, direction);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(shootingoint.position, (Vector2)shootingoint.position + gizmosDirection * 10f);
    }
}
