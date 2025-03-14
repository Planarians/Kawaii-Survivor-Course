using UnityEngine;

public class RanageEnemyAttack : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Transform shootingoint;
    [SerializeField] private GameObject bulletPrefab;
    private Player player;

    [Header("Settings")]

    [SerializeField] private int damage;
    [SerializeField] private float attackFrequency = 1f;

    private float attackTimer = 0f;
    private float attackDelay = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
