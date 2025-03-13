using UnityEngine;

public class DamageTextManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private DamageText damageTextPrefab;


    private void Awake()
    {
        Enemy.onDamageTaken += InstantiateDamageText;
    }

    private void OnDestroy()
    {
        Enemy.onDamageTaken -= InstantiateDamageText;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [NaughtyAttributes.Button]
    private void InstantiateDamageText(int damage, Vector2 enemyPos)
    {
        // Vector3 spawnPosition = Random.insideUnitCircle * Random.Range(0.5f, 1.5f);
        Vector3 spawnPosition = enemyPos + Vector2.up * Random.Range(0.5f, 1.5f);

        DamageText damageTextInstance = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, transform);
        damageTextInstance.Animate(damage);
    }
}
