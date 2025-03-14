using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Collider2D playerCollider;
    private PlayerHealth playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        playerHealth.TakeDamage(damage);
    }

    public void GetCenter()
    {

    }
}
