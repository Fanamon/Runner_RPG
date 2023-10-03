using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewObserver : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public bool IsPositionLowerDisablePointZ(float positionZ)
    {
        Vector3 disablePoint = _camera.ViewportToWorldPoint(new Vector3(0, 0, 0));

        return positionZ < disablePoint.z;
    }
}
