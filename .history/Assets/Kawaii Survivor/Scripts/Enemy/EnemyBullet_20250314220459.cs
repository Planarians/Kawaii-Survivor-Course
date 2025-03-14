using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyBullet : MonoBehaviour
{
    [Header("Elements")]
    private Rigidbody2D rb;
    private Collider2D bulletCollider;
    private RanageEnemyAttack ranageEnemyAttack;

    [Header("Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletCollider = GetComponent<Collider2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Configure(RanageEnemyAttack ranageEnemyAttack)
    {
        this.ranageEnemyAttack = ranageEnemyAttack;
    }

    public void Shoot(int damage, Vector2 direction)
    {
        this.damage = damage;
        transform.right = direction;
        //set the velocity of the bullet
        rb.velocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //check if the collider has a player component and get the player
        if (collider.TryGetComponent(out Player player))
        {
            player.TakeDamage(damage);
            //disable the collider 
            this.bulletCollider.enabled = false;
            ranageEnemyAttack.ReleaseBullet(this);
        }
    }

    public void Reload()
    {
        rb.velocity = Vector2.zero;
        transform.right = Vector2.zero;
    }
}
