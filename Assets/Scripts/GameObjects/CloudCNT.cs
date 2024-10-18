using UnityEngine;

public class CloudCNT : MonoBehaviour
{
    [SerializeField]
    private float floatAmplitude = 0.1f;

    [SerializeField]
    private float floatSpeed = 0.5f;

    [SerializeField]
    private float fadeSpeed = 0.2f;

    [SerializeField]
    private float minAlpha = 0.8f;

    [SerializeField]
    private float maxAlpha = 1f;

    private SpriteRenderer spriteRenderer;
    private float startY;
    private float alphaDirection = 1;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startY = transform.position.y;
    }

    private void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        Color color = spriteRenderer.color;
        color.a += fadeSpeed * alphaDirection * Time.deltaTime;
        if (color.a > maxAlpha || color.a < minAlpha)
        {
            alphaDirection *= -1;
        }
        spriteRenderer.color = color;
    }
}
