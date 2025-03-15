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
        // if (player != null)
        // {
        //     FollowPlayer();
        // }
    }

    public void StorePlayer(Player player)
    {
        if (player == null)
        {
            Debug.Log("player is null storeplayer");
        }
        this.player = player;
    }


    public void FollowPlayer()
    {

        if (player == null)
        {
            Debug.LogWarning("Player is null, cannot follow.");
            return;
        }

        Debug.Log("startFollowPlayer");
        // 获取玩家位置
        Vector2 direction = (player.transform.position - transform.position).normalized;
        // 计算目标位置
        Vector2 targetPosition = (Vector2)transform.position + direction * moveSpeed * Time.deltaTime;

        transform.position = targetPosition;
    }




}
