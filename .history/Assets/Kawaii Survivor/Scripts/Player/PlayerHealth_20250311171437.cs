using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Settings")]
    [SerializeField] private int maxHealth = 10;
    private int health;

    [Header("Elements")]
    [SerializeField] private Slider healthSlider;

    void Start()
    {
        health = maxHealth;

        healthSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        float healthSliderValue = (float)health / maxHealth;
        healthSlider.value = healthSliderValue;

        if (health <= 0)
        {
            PassAway();
        }
    }

    private void PassAway()
    {
        Debug.Log("Player is dead");
        Destroy(gameObject);
    }
}
