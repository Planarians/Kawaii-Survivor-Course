using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // [Header("Elements")]
    // [SerializeField] private Transform enemy;

    [Header("Settings")]
    [SerializeField] private float range;
    [SerializeField] private LayerMask enemyMask;

    [Header("Animations")]
    //目标旋转速度
    [SerializeField] private float aimLerp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform enemy = null;
        // Enemy[] enemies = GameObject.FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        Vector2 targetUpVector = Vector3.up;

        Enemy[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyMask).Select(x => x.GetComponent<Enemy>()).ToArray();

        int closestEnemyIndex = -1;
        float minDistance = Mathf.Infinity;

        if (enemies.Length == 0)
        {
            transform.up = Vector3.up;
            return;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemyIndex = i;
            }
        }
        // If there is an enemy, set the enemy to the closest enemy
        if (closestEnemyIndex != -1)
        {

            enemy = enemies[closestEnemyIndex].transform;
        }
        // If there is no enemy, set the weapon to the up vector
        else
        {
            // 插值旋转
            transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
            return;
        }

        targetUpVector = (enemy.position - transform.position).normalized;
        transform.up = targetUpVector;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}