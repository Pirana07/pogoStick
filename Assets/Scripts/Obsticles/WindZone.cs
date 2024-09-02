
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [SerializeField] Vector2 _windForce = new Vector2(0, 10f);

     void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")){
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null) {
                rb.AddForce(_windForce, ForceMode2D.Impulse);
            }
        }
    }
}
