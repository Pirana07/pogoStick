using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField]  float _wait = 0.01f;  // სცენის გადატვირთვის წინ შესამოწმებელი დრო
    [SerializeField]  ParticleSystem _bloodParticles; // სისხლის ნაწილაკები
    [SerializeField]  ParticleSystem _headImpactParticles; // თავსარტყმის ნაწილაკები
    [SerializeField]  AudioClip _headImpactSound; // თავსარტყმის ხმა
    [SerializeField]  AudioSource _audioSource; // აუდიო წყარო
    [SerializeField]  CameraShake _cameraShake; // კამერის სეიკის სკრიპტი
    
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
            HandleCrash();
        }
        else if (other.CompareTag("Ceiling") && !_hasCrashed)
        {
            HandleHeadImpact();
        }
    }

    void HandleCrash()
    {
        _hasCrashed = true;
        Debug.Log("Ouch!");
        _controller.enabled = false;  // PogoStickController-ის გათიშვა
        PlayBloodParticles();  // სისხლის ნაწილაკების გაშვება
        Invoke("SceneReload", _wait);
    }

    void HandleHeadImpact()
    {
        Debug.Log("Head Impact!");
        PlayHeadImpactEffects();  // თავსარტყმის ეფექტების გაშვება
    }

    void PlayBloodParticles()
    {
        if (_bloodParticles != null)
        {
            _bloodParticles.Play(); // სისხლის ნაწილაკების გაშვება
        }
    }

    void PlayHeadImpactEffects()
    {
        if (_headImpactParticles != null)
        {
            _headImpactParticles.Play(); // თავსარტყმის ნაწილაკების გაშვება
        }

        if (_audioSource != null && _headImpactSound != null)
        {
            _audioSource.PlayOneShot(_headImpactSound); // თავსარტყმის ხმის გაშვება
        }

        if (_cameraShake != null)
        {
            _cameraShake.Shake(); // კამერის სეიკის გაშვება
        }

        // აქ შეგიძლიათ დაამატოთ ნებისმიერი სხვა ეფექტი, რომელიც გსურთ.
    }

    void SceneReload()
    {
        SceneManager.LoadScene(0);
    }
}
