using UnityEngine;

public class Weapon : MonoBehaviour
{
    // [Header("Elements")]
    // [SerializeField] private Transform enemy;
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
            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemyIndex = i;
            }
        }
        if (closestEnemyIndex != -1)
        {
            enemy = enemies[closestEnemyIndex].transform;
        }
        if (enemy != null)
        {
            transform.up = (enemy.position - transform.position).normalized;
        }
    }
}