using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;



    [Header("Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float playerDetectionRadius = 1f;



    [Header("DEBUG")]
    [SerializeField] private bool gizmos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {
        if (!hasSpawned)
        {
            return;
        }
        if (player != null)
        {
            FollowPlayer();
        }

    }

    private void Wait()
    {
        attackTimer += Time.deltaTime;
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
