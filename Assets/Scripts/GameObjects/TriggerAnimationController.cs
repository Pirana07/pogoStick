using UnityEngine;

public class TriggerAnimationController : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (animator.enabled == false)
            {
                animator.enabled = true; // Enable the Animator component
            }
        }
    }
}
