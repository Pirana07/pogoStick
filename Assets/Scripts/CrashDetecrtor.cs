using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField]  float _wait = 0.01f;  // სცენის გადატვირთვის წინ შესამოწმებელი დრო
    [SerializeField]  ParticleSystem _bloodParticles; // სისხლის ნაწილაკები
     bool _hasCrashed = false;
     PogoStickController _controller;

    void Start()
    {
        _controller = GetComponent<PogoStickController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") && !_hasCrashed)
        {
            _hasCrashed = true;
            Debug.Log("Ouch!");
            _controller.enabled = false;  // PogoStickController-ის გათიშვა
            PlayBloodParticles();  // სისხლის ნაწილაკების გაშვება
            Invoke("SceneReload", _wait);
        }
    }

    void SceneReload()
    {
        SceneManager.LoadScene(0);
    }

    void PlayBloodParticles()
    {
        if (_bloodParticles != null)
        {
            _bloodParticles.Play(); // სისხლის ნაწილაკების გაშვება
        }
    }
}
