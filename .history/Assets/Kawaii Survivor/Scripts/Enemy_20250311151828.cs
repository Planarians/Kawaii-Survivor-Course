using UnityEngine;


[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour
{

    [Header("Components")]
    private EnemyMovement movement;

    [Header("Elements")]
    private Player player;

    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned = false;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticle;

    [Header("Attack")]
    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency = 1f;
    [SerializeField] private float playerDetectionRadius = 1f;

    private float attackTimer = 0f;
    private float attackDelay = 0f;


    [Header("DEBUG")]
    [SerializeField] private bool gizmos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
        enemyRenderer.enabled = false;
        spawnIndicator.enabled = true;


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
        enemyRenderer.enabled = true;
        spawnIndicator.enabled = false;
        // moveSpeed = 1f;
        hasSpawned = true;

        movement.StorePlayer(player);
    }

    // Update is called once per frame
    void Update()
    {

        if (attackTimer >= attackDelay)
        {
            TryAttack();
            attackTimer = 0f;
        }
        else
        {
            Wait();
        }
    }

    private void SetRenderersVisibility(bool visible)
    {
        enemyRenderer.enabled = visible;
        spawnIndicator.enabled = !visible;
    }

    private void Wait()
    {
        attackTimer += Time.deltaTime;
    }


    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        // Debug.Log(distanceToPlayer);

        if (distanceToPlayer <= playerDetectionRadius)
        {
            // player.TakeDamage(1);
            // PassAway();
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("Dealing" + damage + "damage to the player...");
        attackTimer = 0f;
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
        Gizmos.color = Color.red;
        //绘制Enemy检测范围
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

}
