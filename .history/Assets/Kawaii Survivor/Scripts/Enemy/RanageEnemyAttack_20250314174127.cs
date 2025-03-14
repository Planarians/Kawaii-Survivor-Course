using UnityEngine;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attackDelay = 1f / attackFrequency;
        attackTimer = attackDelay;
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
        Vector2 direction = (player.transform.position - shootingoint.position).normalized;
        gizmosDirection = direction;
        Debug.Log("Shooting at player");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + gizmosDirection * 10f);
    }
}
