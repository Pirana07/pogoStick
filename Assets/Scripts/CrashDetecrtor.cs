using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private float _wait = 0.01f;  // Time to wait before reloading the scene
    private bool _hasCrashed = false;
    private PogoStickController _controller;

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
            _controller.enabled = false;  // Disable the PogoStickController
            Invoke("SceneReload", _wait);
        }
    }

    void SceneReload()
    {
        SceneManager.LoadScene(0);
    }
}
