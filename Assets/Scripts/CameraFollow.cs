using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target; // კამერის მიზანი (თქვენი პერსონაჟი)
    [SerializeField] private Vector3 _offset; // პერსონაჟისგან კამერის დაშორება
    [SerializeField] private float _smoothSpeed = 0.125f; // კამერის მოძრაობის smooth 
    [SerializeField] private float _ySmoothSpeed = 0.05f; // y ღერძზე უფრო ნელი და smooth მოძრაობა

    void FixedUpdate()
    {
        if (_target != null)
        {
            // სასურველი პოზიციის გამოთვლა
            Vector3 desiredPosition = _target.position + _offset;

            // კამერის ახალი პოზიციის გამოთვლა y ღერძზე ნელა
            Vector3 smoothedPosition = new Vector3(
                Mathf.Lerp(transform.position.x, desiredPosition.x, _smoothSpeed),
                Mathf.Lerp(transform.position.y, desiredPosition.y, _ySmoothSpeed), // y ღერძზე ნელი მოძრაობა
                Mathf.Lerp(transform.position.z, desiredPosition.z, _smoothSpeed)
            );

            // კამერის ახალი პოზიციის დადგენა
            transform.position = smoothedPosition;
        }
    }
}
