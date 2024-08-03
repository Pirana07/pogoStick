using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]  Transform target; // კამერის მიზანი (თქვენი პერსონაჟი)
    [SerializeField]  Vector3 offset; // პერსონაჟისგან კამერის დაშორება
    [SerializeField]  float smoothSpeed = 0.125f; // კამერის მოძრაობის smooth 

    void FixedUpdate()
    {
        if (target != null)
        {
            // სასურველი პოზიციის გამოთვლა
            Vector3 desiredPosition = target.position + offset;
            
            // smooth მოძრაობის გამოთვლა
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            
            // კამერის ახალი პოზიციის დადგენა
            transform.position = smoothedPosition;
        }
    }
}
