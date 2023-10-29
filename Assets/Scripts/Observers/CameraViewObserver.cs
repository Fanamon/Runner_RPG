using UnityEngine;

public class CameraViewObserver : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public bool IsPositionLowerDisablePointZ(float positionZ)
    {
        Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector3(0, 0, -10));

        return positionZ < disablePoint.z;
    }
}
