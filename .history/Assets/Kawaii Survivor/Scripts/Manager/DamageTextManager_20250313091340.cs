using UnityEngine;

public class DamageTextManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private DamageText damageTextPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [NaughtyAttributes.Button]
    private void InstantiateDamageText(Vector3 position, int damage)
    {
        DamageText damageText = Instantiate(damageTextPrefab, position, Quaternion.identity);
        damageText.SetDamage(damage);
    }
}
