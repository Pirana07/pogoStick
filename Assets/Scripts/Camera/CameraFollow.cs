using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothSpeed = 0.125f;
    [SerializeField] private float _ySmoothSpeed = 0.05f;
    [SerializeField] private float _shakeDuration = 0.2f;
    [SerializeField] private float _shakeMagnitude = 0.1f;

    private Vector3 _shakeOffset;
    private float _shakeTimeRemaining;

    void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 desiredPosition = _target.position + _offset + _shakeOffset;

            Vector3 smoothedPosition = new Vector3(
                Mathf.Lerp(transform.position.x, desiredPosition.x, _smoothSpeed),
                Mathf.Lerp(transform.position.y, desiredPosition.y, _ySmoothSpeed),
                Mathf.Lerp(transform.position.z, desiredPosition.z, _smoothSpeed)
            );

            transform.position = smoothedPosition;

            if (_shakeTimeRemaining > 0)
            {
                _shakeOffset = Random.insideUnitSphere * _shakeMagnitude;
                _shakeOffset.z = 0;
                _shakeTimeRemaining -= Time.deltaTime;
            }
            else
            {
                _shakeOffset = Vector3.zero;
            }
        }
    }

    public void TriggerLandingShake()
    {
        _shakeTimeRemaining = _shakeDuration;
    }
}
