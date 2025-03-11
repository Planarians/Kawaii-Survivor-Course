using UnityEngine;

public class Weapon : MonoBehaviour
{
    // [Header("Elements")]
    // [SerializeField] private Transform enemy;

    [Header("Settings")]
    [SerializeField] private float range;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform enemy = null;
        Enemy[] enemies = GameObject.FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        int closestEnemyIndex = -1;
        float minDistance = Mathf.Infinity;

        for (int i = 0; i < enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemyIndex = i;
            }
        }
        if (closestEnemyIndex != -1)
        {
            enemy = enemies[closestEnemyIndex].transform;
        }
        else
        {
            transform.up = Vector3.up;
            return;
        }
        transform.up = (enemy.position - transform.position).normalized;
    }
}