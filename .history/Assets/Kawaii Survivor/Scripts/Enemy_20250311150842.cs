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


        // Hide the renderer
        // Show the spawn indicator
        enemyRenderer.enabled = false;
        spawnIndicator.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
