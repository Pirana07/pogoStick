using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 20f;  // Force applied when bouncing

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PogoStickController pogoStick = other.GetComponent<PogoStickController>();
            if (pogoStick != null)
            {
                Bounce(pogoStick);
            }
        }
    }

    private void Bounce(PogoStickController pogoStick)
    {
        // Calculate the bounce direction based on the trampoline's normal
        Vector2 bounceDirection = Vector2.up;  // Assuming trampoline bounces straight up

        // Apply the bounce force to the pogo stick's Rigidbody2D
        pogoStick.GetComponent<Rigidbody2D>().velocity = new Vector2(pogoStick.GetComponent<Rigidbody2D>().velocity.x, _bounceForce);
    }
}
