using UnityEngine;

public class PogoFlip : MonoBehaviour
{
    [SerializeField] private ComboManager _comboManager;
    private Rigidbody2D _rb;
    private int _flipCount;
    private bool _canCountFlip;
    private const float _resetDelay = 3f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _flipCount = 0;
        _canCountFlip = true;
    }

    void Update()
    {
        CheckFlip();
    }

    void CheckFlip()
    {
        if (Vector2.Dot(transform.up, Vector2.down) > 0.9f && _canCountFlip)
        {
            _flipCount++;
            _canCountFlip = false;
            _comboManager.ShowCombo(_flipCount);
            ResetTimer();
        }

        if (Vector2.Dot(transform.up, Vector2.up) > 0.9f)
        {
            _canCountFlip = true;
        }
    }

    void ResetTimer()
    {
        CancelInvoke(nameof(ResetFlipCount));
        Invoke(nameof(ResetFlipCount), _resetDelay);
    }

    void ResetFlipCount()
    {
        _flipCount = 0;
        _comboManager.ResetEffects();
    }
}
