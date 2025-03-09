using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float minX;
    [SerializeField] private float maxY;
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position;
        targetPosition.z = -10;
        transform.position = targetPosition;
    }
}
