using UnityEngine;

public class Enemy : MonoBehaviour
{

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
    private float attackTimer = 0f;
    private float attackDelay = 0f;

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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
