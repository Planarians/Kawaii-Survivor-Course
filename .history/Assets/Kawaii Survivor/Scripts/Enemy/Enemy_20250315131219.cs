using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    protected EnemyMovement movement;


    [Header("Health")]
    [SerializeField] protected int maxHealth = 10;
    protected int health;
    [SerializeField] protected TextMeshPro healthText;


    [Header("Elements")]
    protected Player player;

    [Header("Spawn Sequence Related")]
    [SerializeField] protected SpriteRenderer enemyRenderer;
    [SerializeField] protected SpriteRenderer spawnIndicator;
    [SerializeField] protected Collider2D enemyCollider;
    protected bool hasSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
