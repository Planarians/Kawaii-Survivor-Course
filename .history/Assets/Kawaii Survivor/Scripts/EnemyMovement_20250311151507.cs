using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Elements")]
    private Player player;



    [Header("Settings")]
    [SerializeField] private float moveSpeed = 2f;




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

    public void StorePlayer(Player player)
    {
        this.player = player;
    }


    private void FollowPlayer()
    {
        // 获取玩家位置
        Vector2 direction = (player.transform.position - transform.position).normalized;
        // 计算目标位置
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }




}
