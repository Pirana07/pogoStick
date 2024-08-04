using UnityEngine;

public class PogoStickController : MonoBehaviour
{
    [SerializeField]  float jumpForce = 10f;
    [SerializeField]  float _rotationSpeed = 5f;
    [SerializeField]  Transform _groundCheck;
    [SerializeField]  float _groundCheckRadius = 0.2f;
    [SerializeField]  LayerMask _groundLayer;
    [SerializeField]  ParticleSystem _groundParticles; // მიწაზე მოხვედრის ნაწილაკები

     Rigidbody2D _rb;
     bool _isGrounded;
     bool _wasGrounded; // წინა მიწაზე ყოფნის მდგომარეობა

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RotateTowardsMouse();
        CheckGround();

        if (_isGrounded && !_wasGrounded)
        {
            PlayGroundParticles(); // მიწაზე მოხვედრის ნაწილაკების გაშვება
        }

        if (_isGrounded)
        {
            Jump();
        }

        _wasGrounded = _isGrounded; // განახლება წინა მიწაზე ყოფნის მდგომარეობის
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
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
    }

    void PlayGroundParticles()
    {
        if (_groundParticles != null)
        {
            _groundParticles.Play(); // ნაწილაკების გაშვება
        }
    }
}
