using UnityEngine;
using UnityEngine.Pool;


public class DamageTextManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private DamageText damageTextPrefab;

    [Header("Pooling")]
    private ObjectPool<DamageText> damageTextPool;


    private void Awake()
    {
        Enemy.onDamageTaken += InstantiateDamageText;
    }

    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(
            () => Instantiate(damageTextPrefab, transform),
            (obj) => obj.gameObject.SetActive(true),
            (obj) => obj.gameObject.SetActive(false),
            (obj) => Destroy(obj.gameObject),
            false,
            10
        );
    }

    private DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab, transform);
    }

    private void ActionOnGet(DamageText obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void ActionOnRelease(DamageText obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(DamageText obj)
    {
        Destroy(obj.gameObject);
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
