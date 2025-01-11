using UnityEngine;

public class PogoStickController : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    [Header("Effects")]
    [SerializeField] private ParticleSystem _groundImpactParticles;
    [SerializeField] private Animator _anim;
    [SerializeField] private CameraFollow _cameraFollow;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip landingSound; 
    [SerializeField] private AudioSource audioSource;

    private Rigidbody2D _rb;
    private bool _isGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (_groundImpactParticles != null)
        {
            _groundImpactParticles.Stop();
            _groundImpactParticles.Clear();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        RotateTowardsMouse();
        CheckGround();

        if (_isGrounded)
        {
            Jump();
            _anim.SetBool("IsGrounded", false);
        }
        else
        {
            _anim.SetBool("IsGrounded", true);
        }
    }

    void Jump()
    {
        Vector2 jumpDirection = transform.up;
        _rb.velocity = new Vector2(jumpDirection.x * jumpForce, jumpDirection.y * jumpForce);
        _anim.SetTrigger("PrepareForLanding");
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    void CheckGround()
    {
        bool wasGrounded = _isGrounded;
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        if (_isGrounded && !wasGrounded)
        {
            SpawnGroundImpactParticles();
            PlayLandingSound();
            _cameraFollow.TriggerLandingShake();
        }
    }

    void SpawnGroundImpactParticles()
    {
        if (_groundImpactParticles != null)
        {
            _groundImpactParticles.transform.position = _groundCheck.position;
            _groundImpactParticles.Play();
        }
    }

    void PlayLandingSound()
    {
        if (landingSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(landingSound); 
        }
    }
}
