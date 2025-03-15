using UnityEngine;
using TMPro;
using System;

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


    [Header("Effects")]
    [SerializeField] protected ParticleSystem passAwayParticle;

    [Header("Attack")]
    [SerializeField] protected float playerDetectionRadius = 1f;

    [Header("Actions")]
    public static Action<int, Vector2> onDamageTaken;


    [Header("DEBUG")]
    [SerializeField] protected bool gizmos;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemyRenderer.enabled)
        {
            return;
        }
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
}
