using UnityEngine;

public class MyMobileJoystick : MonoBehaviour
{
    // 让玩家可以在屏幕的任何位置生成摇杆
    [Header(" Elements ")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;

    [Header(" Settings ")]
    private Vector3 clickPosition;
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
        canControl = true;
    }

    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControl = false;
    }

    //更新摇杆位置，使其随鼠标拖动移动。
    private void ControlJoystick()
    {
        // 获取当前鼠标位置
        Vector3 currentPosition = Input.mousePosition;
        // 计算摇杆位置相对于点击位置的偏移量
        Vector3 direction = currentPosition - clickPosition;
        // 更新摇杆位置
        joystickKnob.position = clickPosition + direction;
    }
}
