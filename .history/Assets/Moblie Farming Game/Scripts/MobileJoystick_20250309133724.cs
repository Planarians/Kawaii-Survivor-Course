using UnityEngine;

public class MyMobileJoystick : MonoBehaviour
{
    // 让玩家可以在屏幕的任何位置生成摇杆
    [Header(" Elements ")]
    [SerializeField] private RectTransform joystickOutline;

    [Header(" Settings ")]
    private bool canControl;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideJoystick();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControl)
        {
            ControlJoystick();
        }

    }


    public void ClickedOnJoystickZoneCallback()
    {
        Vector3 clickPosition = Input.mousePosition;
        joystickOutline.position = clickPosition;
        ShowJoystick();
    }

    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
    }

    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
    }

    private void ControlJoystick()
    {
        Vector3 clickPosition = Input.mousePosition;
        joystickOutline.position = clickPosition;
    }
}
