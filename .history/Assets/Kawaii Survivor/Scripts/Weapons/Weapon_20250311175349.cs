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
        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        transform.up = (enemy.position - transform.position).normalized;
    }
}
