using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private void Update()
    {
        transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime);
    }
}
