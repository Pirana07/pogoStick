using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField] private Transform _target; // კამერის მიზანი (თქვენი პერსონაჟი)
    [SerializeField] private Vector3 _offset; // პერსონაჟისგან კამერის დაშორება
    [SerializeField] private float _smoothSpeed = 0.125f; // კამერის მოძრაობის smooth 
    [SerializeField] private float _ySmoothSpeed = 0.05f; // y ღერძზე უფრო ნელი და smooth მოძრაობა
    [SerializeField] private float _shakeDuration = 0.2f; // How long the camera shakes for
    [SerializeField] private float _shakeMagnitude = 0.1f; // The amount the camera shakes

    private Vector3 _shakeOffset;
    private float _shakeTimeRemaining;

    void FixedUpdate()
    {
        if (_target != null)
        {
            // სასურველი პოზიციის გამოთვლა
            Vector3 desiredPosition = _target.position + _offset + _shakeOffset;

            // კამერის ახალი პოზიციის გამოთვლა y ღერძზე ნელა
            Vector3 smoothedPosition = new Vector3(
                Mathf.Lerp(transform.position.x, desiredPosition.x, _smoothSpeed),
                Mathf.Lerp(transform.position.y, desiredPosition.y, _ySmoothSpeed), // y ღერძზე ნელი მოძრაობა
                Mathf.Lerp(transform.position.z, desiredPosition.z, _smoothSpeed)
            );

            // კამერის ახალი პოზიციის დადგენა
            transform.position = smoothedPosition;

            // Update shake effect
            if (_shakeTimeRemaining > 0)
            {
                _shakeOffset = Random.insideUnitSphere * _shakeMagnitude;
                _shakeOffset.z = 0; // Keep the shake effect in 2D (x and y axis only)
                _shakeTimeRemaining -= Time.deltaTime;
            }
            else
            {
                _shakeOffset = Vector3.zero;
            }
        }
    }

    public void TriggerShake(float duration, float magnitude)
    {
        _shakeDuration = duration;
        _shakeMagnitude = magnitude;
        _shakeTimeRemaining = duration;
    }

    public void TriggerShake()
    {
        _shakeTimeRemaining = _shakeDuration;
    }
}
