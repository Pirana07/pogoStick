using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private float waitTime = 0.01f; // Time to wait before reloading the scene
    [SerializeField] private ParticleSystem bloodParticles; // Blood particles
    [SerializeField] private DeathMenu deathMenu; // Reference to the DeathMenu script

    private bool hasCrashed = false;
    private PogoStickController controller;

    void Start()
    {
        controller = GetComponent<PogoStickController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") && !hasCrashed)
        {
            HandleCrash();
        }
    }

    void HandleCrash()
    {
        hasCrashed = true;
        Debug.Log("Ouch!");
        controller.enabled = false; // Disable PogoStickController
        PlayBloodParticles(); // Play blood particles
        Vector3 respawnPosition = transform.position; // Save current position for respawn
        deathMenu.TriggerDeath(respawnPosition); // Trigger the death menu
    }

    void PlayBloodParticles()
    {
        if (bloodParticles != null)
        {
            bloodParticles.Play(); // Play blood particles
        }
    }

    public void ResetCrashStatus()
    {
        hasCrashed = false; // Reset the crash status
    }
}
