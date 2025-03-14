using TMPro;
using UnityEngine;
using System;


[RequireComponent(typeof(EnemyMovement), typeof(RanageEnemyAttack))]

public class RangeEnemy : MonoBehaviour
{

    [Header("Components")]
    private EnemyMovement movement;
    private RanageEnemyAttack attack;

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




    [Header("Attack")]
    [SerializeField] private float playerDetectionRadius = 1f;

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
        attack = GetComponent<RanageEnemyAttack>();

        player = FindFirstObjectByType<Player>();

        // 如果玩家不存在，则销毁敌人
        if (player == null)
        {
            Debug.LogWarning("Player not found");
            Destroy(gameObject);
        }


        StartSpawnSequence();


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


    void Update()
    {

        if (!enemyRenderer.enabled)
        {
            return;
        }

        ManageAttack();

    }

    private void ManageAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > playerDetectionRadius)
        {
            movement.FollowPlayer();
        }
        else
        {
            TryAttack();
        }
    }


    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }


    private void TryAttack()
    {
        attack.AutoAim();
    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        healthText.text = health.ToString();

        onDamageTaken?.Invoke(damage, transform.position);

        if (health <= 0)
        {
            PassAway();
        }
    }

    private void Attack()
    {
        // Debug.Log("Dealing" + damage + "damage to the player...");
        attackTimer = 0f;

        // player.TakeDamage(damage);
        //TODO: 射击玩家
        Debug.Log("Range Enemy Shooting at player");

    }

    private void PassAway()
    {
        //Unparent the particle
        passAwayParticle.transform.SetParent(null);
        passAwayParticle.Play();
        Destroy(gameObject);
    }

    //用于检测Enemy检测范围
    private void OnDrawGizmos()
    {
        if (!gizmos) return;
        Gizmos.color = Color.magenta;
        //绘制Enemy检测范围
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);

    }
}
