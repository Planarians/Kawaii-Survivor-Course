using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;

    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float playerDetectionRadius = 1f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem passAwayParticle;

    [Header("DEBUG")]
    [SerializeField] private bool gizmos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();

        // 如果玩家不存在，则销毁敌人
        if (player == null)
        {
            Debug.LogWarning("Player not found");
            Destroy(gameObject);
        }

        // Hide the renderer
        // Show the spawn indicator
        enemyRenderer.enabled = false;
        spawnIndicator.enabled = true;

        // Scale up & down the spawn 
        Vector3 targetScale = spawnIndicator.transform.localScale * 1.2f;
        LeanTween.scale(spawnIndicator.gameObject, targetScale, .3f)
        .setEaseInOutSine()
        .setLoopPingPong(4)
        .setOnComplete(SpawnSequenceComplete);
        // Show the enemy after 3 seconds
        // Hide the spawn indicator
        // Prevent Following& Attacking durring the spawn sequence

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        TryAttack();
    }

    private void SpawnSequenceComplete()
    {
        enemyRenderer.enabled = false;
        spawnIndicator.enabled = true;
    }

    private void FollowPlayer()
    {
        // 获取玩家位置
        Vector2 direction = (player.transform.position - transform.position).normalized;
        // 计算目标位置
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }

    private void TryAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        Debug.Log(distanceToPlayer);

        if (distanceToPlayer <= playerDetectionRadius)
        {
            // player.TakeDamage(1);
            PassAway();
        }
    }
    //用于检测Enemy检测范围
    private void OnDrawGizmos()
    {
        if (!gizmos) return;
        Gizmos.color = Color.red;
        //绘制Enemy检测范围
        Gizmos.DrawWireSphere(transform.position, playerDetectionRadius);
    }

    private void PassAway()
    {
        //Unparent the particle
        passAwayParticle.transform.SetParent(null);
        passAwayParticle.Play();
        Destroy(gameObject);
    }
}
