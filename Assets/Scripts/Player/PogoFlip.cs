using UnityEngine;

public class PogoFlip : MonoBehaviour
{
    [SerializeField] private ComboManager _comboManager;
    private int _flipCount;
    private bool _canCountFlip;

    void Start()
    {
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
        }

        if (Vector2.Dot(transform.up, Vector2.up) > 0.9f)
        {
            _canCountFlip = true;
        }
    }
}
