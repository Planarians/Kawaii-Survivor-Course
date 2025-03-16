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
        animator.Play("Animate");
    }

}
