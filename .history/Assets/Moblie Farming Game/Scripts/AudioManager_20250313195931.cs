using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActionTester.myAction += DisplayHealthPopup;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DisplayHealthPopup(int health)
    {
        Debug.Log("Health: " + health);
    }
}
