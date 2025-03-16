using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Elements")]
    private Rigidbody2D rb;
    private Collider2D bulletCollider;
    private RanageEnemyAttack ranageEnemyAttack;

    [Header("Settings")]
    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
