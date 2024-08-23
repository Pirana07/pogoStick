using System.Collections;  // Required for IEnumerator
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]  float _shakeDuration = 0.5f;  // The duration of the camera shake
    [SerializeField]  float _shakeMagnitude = 0.2f;  // The magnitude of the camera shake

     Vector3 _initialPosition;

    void Start()
    {
        _initialPosition = transform.localPosition;  // Store the initial position of the camera
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _shakeDuration)
        {
            // Calculate random shake position
            Vector3 randomPoint = _initialPosition + Random.insideUnitSphere * _shakeMagnitude;

            // Update camera position
            transform.localPosition = new Vector3(randomPoint.x, randomPoint.y, _initialPosition.z);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Reset camera position to the initial state
        transform.localPosition = _initialPosition;
    }
}
