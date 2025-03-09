using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField] private Rigidbody2D rig;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
