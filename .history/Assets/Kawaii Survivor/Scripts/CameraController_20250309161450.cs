using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position;
        targetPosition.z = -10;
        // 限制相机位置在指定范围内
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        transform.position = targetPosition;
    }
}
