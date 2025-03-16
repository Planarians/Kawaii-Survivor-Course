using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{

    [Header("Elemetns")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro damageText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [NaughtyAttributes.Button]
    public void Animate(int damage, bool isCriticalHit)
    {

        damageText.text = damage.ToString();
        Debug.Log("DamageText Animate" + damage + " " + isCriticalHit);
        //if the damage is critical, change the color to yellow
        damageText.color = isCriticalHit ? Color.yellow : Color.white;
        // 播放动画前记录状态
        Debug.Log($"动画播放前 - 当前状态: {animator.GetCurrentAnimatorStateInfo(0).fullPathHash}, 正在播放: {animator.GetCurrentAnimatorStateInfo(0).IsName("Animate")}");

        animator.Play("Animate");

        // 添加协程来跟踪动画状态
        StartCoroutine(TrackAnimationState());
    }

}
