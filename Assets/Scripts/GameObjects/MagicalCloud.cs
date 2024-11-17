using UnityEngine;

public class MagicalCloud : MonoBehaviour
{
    [SerializeField] private float swayAmplitude = 0.5f; // Maximum sway distance
    [SerializeField] private float swaySpeed = 2f;      // Speed of sway

    private float _originalXPosition;
    private float _swayTimer;

    void Start()
    {
        _originalXPosition = transform.position.x; // Store the original position
    }

    void Update()
    {
        SwayChain();
    }

    private void SwayChain()
    {
        _swayTimer += Time.deltaTime * swaySpeed;
        float swayOffset = Mathf.Sin(_swayTimer) * swayAmplitude;
        transform.position = new Vector3(_originalXPosition + swayOffset, transform.position.y, transform.position.z);
    }

    
}
