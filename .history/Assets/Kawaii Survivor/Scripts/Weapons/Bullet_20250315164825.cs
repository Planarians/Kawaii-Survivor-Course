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
        rb.velocity = direction * moveSpeed;
        bulletCollider.enabled = true;
    }
}
