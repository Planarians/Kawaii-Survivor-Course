using UnityEngine;

public class MeleeWeapon : Weapon
{

    enum State
    {
        Idle,
        Attack
    }

    private State state;

    [Header("Elements")]
    [SerializeField] private Transform hitDetectionTransform;
    [SerializeField] private float hitDetectionRadius = 0.3f;
    [SerializeField] private BoxCollider2D hitCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = State.Idle;

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Idle:
                AutoAim();
                break;
            case State.Attack:
                Attacking();
                break;
        }
    }

    private void AutoAim()
    {
        Enemy closestEnemy = GetClosestEnemy();

        Vector2 targetUpVector = Vector3.up;

        if (closestEnemy != null)
        {
            targetUpVector = (closestEnemy.transform.position - transform.position).normalized;
            transform.up = targetUpVector;
            ManageAttackTimer();

        }
        // 插值旋转
        transform.up = Vector3.Lerp(transform.up, targetUpVector, Time.deltaTime * aimLerp);
        IncrementAttackTimer();
    }

    private void ManageAttackTimer()
    {
        IncrementAttackTimer();

        if (attackTimer >= attackDelay)
        {
            attackTimer = 0f;

            StartAttack();
        }

    }

    private void IncrementAttackTimer()
    {
        attackTimer += Time.deltaTime;
    }


}
