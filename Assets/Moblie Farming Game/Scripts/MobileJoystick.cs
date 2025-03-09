using UnityEngine;

public class MyMobileJoystick : MonoBehaviour
{
    // 让玩家可以在屏幕的任何位置生成摇杆
    [Header(" Elements ")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickKnob;

    [Header(" Settings ")]
    // 移动因子，用于控制摇杆的移动速度
    [SerializeField] private float moveFactor = 540f;
    private Vector3 clickPosition;
    private Vector3 move;
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
        clickPosition = Input.mousePosition;
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
        // 计算摇杆移动的距离 
        float moveMagnitude = direction.magnitude * moveFactor / Screen.width;
        // 限制摇杆移动的距离不超过摇杆的半径
        moveMagnitude = Mathf.Min(moveMagnitude, joystickOutline.rect.width / 2);
        // 计算摇杆移动的距离
        move = direction.normalized * moveMagnitude;
        // 计算摇杆目标位置
        Vector3 targetPosition = clickPosition + move;
        // 更新摇杆位置
        joystickKnob.position = targetPosition;

        // 如果鼠标松开，隐藏摇杆
        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }
    }

    // 获取摇杆移动方向
    public Vector3 GetMoveVector()
    {
        return move;
    }
}
