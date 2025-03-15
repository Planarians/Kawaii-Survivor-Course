using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    protected EnemyMovement movement;


    [Header("Health")]
    [SerializeField] private int maxHealth = 10;
    private int health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
