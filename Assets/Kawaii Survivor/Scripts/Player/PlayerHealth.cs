using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Settings")]
    [SerializeField] private int maxHealth = 10;
    private int health;

    [Header("Elements")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    void Start()
    {
        health = maxHealth;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        UpdateUI();

        if (health <= 0)
        {
            PassAway();
        }
    }

    private void UpdateUI()
    {
        float healthSliderValue = (float)health / maxHealth;
        healthSlider.value = healthSliderValue;

        healthText.text = health + " / " + maxHealth;
    }

    private void PassAway()
    {
        Debug.Log("Player is dead");
        // Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
}
