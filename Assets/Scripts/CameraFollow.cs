using UnityEngine;

public class CameraFollow : MonoBehaviour
{
      Vector3 _offset = new Vector3(0f, 0f, -10f);
     float _smoothTime = 0.25f;
     Vector3 _velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void Update()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}