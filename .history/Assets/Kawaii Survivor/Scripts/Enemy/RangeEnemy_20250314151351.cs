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
        health = maxHealth;
        healthText.text = health.ToString();

        movement = GetComponent<EnemyMovement>();

        player = FindFirstObjectByType<Player>();

        // 如果玩家不存在，则销毁敌人
        if (player == null)
        {
            Debug.LogWarning("Player not found");
            Destroy(gameObject);
        }


        StartSpawnSequence();

        // Prevent Following& Attacking durring the spawn sequence
        // Calculate the attack delay based on the attack frequency
        attackDelay = 1f / attackFrequency;

    }

    private void StartSpawnSequence()
    {
        // Hide the renderer
        // Show the spawn indicator
        SetRenderersVisibility(false);


        // Scale up & down the spawn 
        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
            // .setEaseInOutSine()
            .setLoopPingPong(4)
            .setOnComplete(SpawnSequenceComplete);
    }
    private void SpawnSequenceComplete()
    {
        // Show the enemy after 3 seconds
        // Hide the spawn indicator
        SetRenderersVisibility(true);
        enemyCollider.enabled = true;
        // moveSpeed = 1f;
        hasSpawned = true;

        movement.StorePlayer(player);
    }
    private void SetRenderersVisibility(bool visibility)
    {
        enemyRenderer.enabled = visibility;
        spawnIndicator.enabled = !visibility;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
