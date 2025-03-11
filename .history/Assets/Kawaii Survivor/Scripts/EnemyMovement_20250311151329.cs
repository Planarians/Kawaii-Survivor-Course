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

        if (player != null)
        {
            FollowPlayer();
        }

    }




    private void FollowPlayer()
    {
        // 获取玩家位置
        Vector2 direction = (player.transform.position - transform.position).normalized;
        // 计算目标位置
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
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
