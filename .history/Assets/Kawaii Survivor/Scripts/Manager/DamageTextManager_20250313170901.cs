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
    private void InstantiateDamageText(int damage)
    {
        Vector3 spawnPosition = Random.insideUnitCircle * Random.Range(0.5f, 1.5f);
        DamageText damageTextInstance = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, transform);
        damageTextInstance.Animate();
    }
}
