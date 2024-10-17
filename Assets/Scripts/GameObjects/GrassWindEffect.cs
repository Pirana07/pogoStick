using UnityEngine;

public class GrassWindEffect : MonoBehaviour
{
    [SerializeField] private float _swaySpeed = 1f; // სიჩქარე ჰაერის ეფექტისთვის
    [SerializeField] private float _swayAmount = 5f; // ჰაერის რაოდენობა

    private Vector3 _initialRotation;

    void Start()
    {
        _initialRotation = transform.eulerAngles;
    }

    void Update()
    {
        float sway = Mathf.Sin(Time.time * _swaySpeed) * _swayAmount;
        transform.eulerAngles = _initialRotation + new Vector3(0, 0, sway);
    }
}
