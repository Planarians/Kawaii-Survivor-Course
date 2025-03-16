using UnityEngine;
using TMPro;
using System.Collections;
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
    private IEnumerator TrackAnimationState()
    {
        // 等待一帧确保动画开始
        yield return null;

        // 检查动画是否正在播放
        Debug.Log($"动画应该开始 - 当前状态: {animator.GetCurrentAnimatorStateInfo(0).fullPathHash}, 正在播放: {animator.GetCurrentAnimatorStateInfo(0).IsName("Animate")}");

        // 等待几帧来跟踪动画进度
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            Debug.Log($"动画跟踪 {i} - 当前状态: {stateInfo.fullPathHash}, 正在播放: {stateInfo.IsName("Animate")}, 归一化时间: {stateInfo.normalizedTime}");
        }
    }
}
