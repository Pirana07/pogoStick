using UnityEngine;

public class PogoStickController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private ParticleSystem _groundImpactParticles; // მიწაზე შეჯახების ნაწილაკები


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
    }

    void Update()
    {
        RotateTowardsMouse();
        CheckGround();

        if (_isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        Vector2 jumpDirection = transform.up;
        _rb.velocity = new Vector2(jumpDirection.x * jumpForce, jumpDirection.y * jumpForce);
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

  
}


