using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{

    [Header("Elements")]
    private Rigidbody2D rb;
    private Collider2D bulletCollider;

    private RangeWeapon rangeWeapon;


    [Header("Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;
    [SerializeField] private LayerMask enemyMask;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();

        //destroy the bullet after 10 seconds because if it doesn't hit anything it will just fly forever
        // LeanTween.delayedCall(gameObject, 10f, () =>
        // {
        //     ranageEnemyAttack.ReleaseBullet(this);
        // });

        // StartCoroutine(ReleaseCoroutine());
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Configure(RangeWeapon rangeWeapon)
    {
        this.rangeWeapon = rangeWeapon;
    }

    public void Shoot(int damage, Vector2 direction)
    {
        Invoke("Release", 3f);

        this.damage = damage;
        rb.velocity = direction * moveSpeed;
        bulletCollider.enabled = true;
    }
    public void Reload()
    {
        rb.velocity = Vector2.zero;
        transform.right = Vector2.zero;
        bulletCollider.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsInLayerMask(collider.gameObject, enemyMask))
        {
            // this.bulletCollider.enabled = false;
            Attack(collider.GetComponent<Enemy>());
            Release();

        }
    }
    private void Release()
    {
        rangeWeapon.ReleaseBullet(this);
    }
    private void Attack(Enemy enemy)
    {
        enemy.TakeDamage(damage);
    }


    private bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return mask == (mask | (1 << obj.layer));
    }
}
