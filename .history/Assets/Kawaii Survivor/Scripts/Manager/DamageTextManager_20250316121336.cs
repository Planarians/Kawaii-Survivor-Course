using TMPro;
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
        Enemy.onDamageTaken += EnemyHitCallback;
    }

    void Start()
    {
        damageTextPool = new ObjectPool<DamageText>(
            CreateFunction,
            ActionOnGet,
            ActionOnRelease,
            ActionOnDestroy,
            false,
            10
        );
    }

    private DamageText CreateFunction()
    {
        return Instantiate(damageTextPrefab, transform);
    }

    private void ActionOnGet(DamageText damageText)
    {
        // 重置TextMeshPro的alpha值
        TextMeshPro textComponent = damageText.GetComponent<TextMeshPro>();
        if (textComponent != null)
        {
            textComponent.alpha = 1.0f;
        }
        else
        {
            textComponent = damageText.GetComponentInChildren<TextMeshPro>();
            if (textComponent != null)
            {
                textComponent.alpha = 1.0f;
            }
        }
        damageText.gameObject.SetActive(true);
    }

    private void ActionOnRelease(DamageText damageText)
    {
        damageText.gameObject.SetActive(false);
    }

    private void ActionOnDestroy(DamageText damageText)
    {
        Destroy(damageText.gameObject);
    }
    private void OnDestroy()
    {
        Enemy.onDamageTaken -= EnemyHitCallback;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void EnemyHitCallback(int damage, Vector2 enemyPos, bool isCriticalHit)
    {

        DamageText damageTextInstance = damageTextPool.Get();
        Vector3 spawnPosition = enemyPos + Vector2.up * Random.Range(0.5f, 1.5f);
        damageTextInstance.transform.position = spawnPosition;
        damageTextInstance.Animate(damage, isCriticalHit);

        // Release the damage text after a short delay
        LeanTween.delayedCall(1f, () =>
        {
            damageTextPool.Release(damageTextInstance);
        });
    }
}
