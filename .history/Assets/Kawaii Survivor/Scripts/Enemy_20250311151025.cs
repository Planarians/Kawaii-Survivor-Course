using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Elements")]
    private Player player;

    [Header("Spawn Sequence Related")]
    [SerializeField] private SpriteRenderer enemyRenderer;
    [SerializeField] private SpriteRenderer spawnIndicator;
    private bool hasSpawned = false;

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

    // Update is called once per frame
    void Update()
    {

    }
}
