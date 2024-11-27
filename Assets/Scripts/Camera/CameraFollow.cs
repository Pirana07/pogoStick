using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] 
     Transform _target;
     
    [SerializeField]
      Vector3 _offset;

    [SerializeField] 
     float _smoothSpeed = 0.125f;

    [SerializeField] 
     float _ySmoothSpeed = 0.05f;

    [SerializeField] 
     float _shakeDuration = 0.2f;

    [SerializeField] 
     float _shakeMagnitude = 0.1f;

    [SerializeField]  
    float _manualMoveSpeed = 2f;

    [SerializeField] 
     Vector2 _manualMoveLimit = new Vector2(3f, 3f);

    [SerializeField]  
    float _manualReturnSpeed = 1f;

    private Vector3 _shakeOffset;
    private Vector3 _manualOffset;
    private float _shakeTimeRemaining;

    void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 desiredPosition = _target.position + _offset + _manualOffset + _shakeOffset;

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

    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * _manualMoveSpeed * Time.deltaTime;
        _manualOffset += input;

        _manualOffset.x = Mathf.Clamp(_manualOffset.x, -_manualMoveLimit.x, _manualMoveLimit.x);
        _manualOffset.y = Mathf.Clamp(_manualOffset.y, -_manualMoveLimit.y, _manualMoveLimit.y);

        if (input == Vector3.zero)
        {
            _manualOffset = Vector3.Lerp(_manualOffset, Vector3.zero, _manualReturnSpeed * Time.deltaTime);
        }
    }

    public void TriggerLandingShake()
    {
        _shakeTimeRemaining = _shakeDuration;
    }
}
