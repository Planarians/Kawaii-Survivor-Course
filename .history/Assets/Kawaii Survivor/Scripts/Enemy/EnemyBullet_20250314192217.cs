using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //check if the collider has a player component
        if (collider.TryGetComponent(out Player player))
        {
            player.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
