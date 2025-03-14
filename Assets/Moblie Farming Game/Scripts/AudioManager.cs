using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActionTester.myAction += DisplayHealthPopup;
    }

    // 当对象被销毁时，移除事件 不然reload场景就会error
    private void OnDestroy()
    {
        ActionTester.myAction -= DisplayHealthPopup;
    }

    private void DisplayHealthPopup(int health)
    {
        Debug.Log("Health: " + health);
    }
}
