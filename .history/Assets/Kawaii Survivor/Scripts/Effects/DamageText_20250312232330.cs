using UnityEngine;
using TMPro;
public class DamageText : MonoBehaviour
{

    [Header("Elemetns")]
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshPro text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [NaughtyAttributes.Button]
    private void Animate()
    {
        damageText.text = Random.Range(1, 500).ToString();
        animator.Play("Animate");
    }

}
