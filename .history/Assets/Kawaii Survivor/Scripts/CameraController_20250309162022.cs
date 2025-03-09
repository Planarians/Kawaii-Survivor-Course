using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    // [SerializeField] private float minX;
    // [SerializeField] private float maxX;
    // [SerializeField] private float minY;
    // [SerializeField] private float maxY;
    // 限制相机位置在指定范围内
    [SerializeField] private Vector2 minMaxXY;
    private void LateUpdate()
    {
        // 如果目标为空，则不进行更新
        if (target == null)
        {
            Debug.LogError("No target has been specified");
            return;
        }
        Vector3 targetPosition = target.position;
        targetPosition.z = -10;
        // 限制相机位置在指定范围内
        // targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        // targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        targetPosition.x = Mathf.Clamp(targetPosition.x, minMaxXY.x, minMaxXY.y);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minMaxXY.x, minMaxXY.y);
        transform.position = targetPosition;
    }
}
