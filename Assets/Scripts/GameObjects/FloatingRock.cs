using UnityEngine;

public class FloatingRock : MonoBehaviour
{
    [SerializeField]  float floatAmplitude = 0.5f;
    [SerializeField]  float floatSpeed = 2f;
    [SerializeField]  float pressureForce = 0.3f; // plyeris shexeba
    [SerializeField]  float recoverySpeed = 5f; // playeris agebis shemdeg
     Vector3 _initialPosition;
     Vector3 _targetPosition;
     bool _isPressed;

    private void Start()
    {
        _initialPosition = transform.position;
        _targetPosition = _initialPosition;
    }

    private void Update()
    {
        //floating effect
        if (!_isPressed)
        {
            float newY = _initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
            _targetPosition = new Vector3(_initialPosition.x, newY, _initialPosition.z);
        }

        //smoothly move towards the target position
        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * recoverySpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //chek if the player landed on the rock;
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPressed = true;
            _targetPosition = new Vector3(_initialPosition.x, _initialPosition.y - pressureForce, _initialPosition.z);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //when the player leaves the rock and   start recovery
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPressed = false;
        }
    }
}
