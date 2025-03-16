using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{

    [Header("Elements")]
    private Rigidbody2D rb;
    private Collider2D bulletCollider;
    // private RanageEnemyAttack ranageEnemyAttack;

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

    public void Shoot(int damage, Vector2 direction)
    {
        this.damage = damage;
        rb.velocity = direction * moveSpeed;
        bulletCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (IsInLayerMask(collider.gameObject, enemyMask))
        {
            // this.bulletCollider.enabled = false;
            Destroy(gameObject);
        }
    }

    private bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return mask == (mask | (1 << obj.layer));
    }
}
