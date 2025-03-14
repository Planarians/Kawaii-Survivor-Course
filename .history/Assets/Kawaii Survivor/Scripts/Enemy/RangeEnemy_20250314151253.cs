using TMPro;
using UnityEngine;
using System;


[RequireComponent(typeof(EnemyMovement))]

public class RangeEnemy : MonoBehaviour
{

    [Header("Components")]
    private EnemyMovement movement;

    [Header("Health")]
    [SerializeField] private int maxHealth = 10;
    private int health;
    [SerializeField] private TextMeshPro healthText;

    [Header("Elements")]
    private Player player;

    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    [SerializeField] private Collider2D enemyCollider;
    private bool hasSpawned = false;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticle;

    [Header("Range Enemy Related")]
    [SerializeField] private bool isRangeEnemy = false;
    [SerializeField] private float rangePlayerDetectionRadius = 10f;


    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency = 1f;
    [SerializeField] private float playerDetectionRadius = 1f;

    private float attackTimer = 0f;
    private float attackDelay = 0f;

    [Header("Actions")]
    public static Action<int, Vector2> onDamageTaken;


    [Header("DEBUG")]
    [SerializeField] private bool gizmos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
