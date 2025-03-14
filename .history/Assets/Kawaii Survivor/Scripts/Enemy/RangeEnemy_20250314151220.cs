using UnityEngine;


[RequireComponent(typeof(EnemyMovement))]

public class RangeEnemy : MonoBehaviour
{

    [Header("Components")]
    private EnemyMovement movement;

    [Header("Health")]
    [SerializeField] private int maxHealth = 10;
    private int health;
    [SerializeField] private TextMeshPro healthText;

    [Header("Elements")]
    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
