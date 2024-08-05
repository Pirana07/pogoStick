using UnityEngine;

public class PogoStickController : MonoBehaviour
{
    // საჯარო ველი გადახტომის ძალისთვის
    [SerializeField]  float jumpForce = 10f;
    // პირადი სერალიზებული ველები
    [SerializeField]  float _rotationSpeed = 5f;
    [SerializeField]  Transform _groundCheck;
    [SerializeField]  float _groundCheckRadius = 0.2f;
    [SerializeField]  LayerMask _groundLayer;
    [SerializeField]  ParticleSystem _groundImpactParticles; // მიწაზე შეჯახების ნაწილაკები

     Rigidbody2D _rb;
     bool _isGrounded;

    void Start()
    {
        // Rigidbody2D კომპონენტის მოპოვება, რომელიც მიბმულია game object-ზე
        _rb = GetComponent<Rigidbody2D>();

        // დარწმუნდით, რომ ნაწილაკთა სისტემა თავდაპირველად არ არის აქტიური
        if (_groundImpactParticles != null)
        {
            _groundImpactParticles.Stop();
            _groundImpactParticles.Clear();
        }
    }

    void Update()
    {
        // პოგოს ჯოხის მაუსის კურსორისკენ მობრუნება
        RotateTowardsMouse();
        // შეამოწმეთ, შეეხო თუ არა პოგოს ჯოხი მიწას
        CheckGround();

        // თუ პოგოს ჯოხი მიწაზეა, გადახტეთ
        if (_isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // გამოთვალეთ გადახტომის მიმართულება და გამოიყენეთ სიჩქარე
        Vector2 jumpDirection = transform.up;
        _rb.velocity = new Vector2(jumpDirection.x * jumpForce, jumpDirection.y * jumpForce);
    }

    void RotateTowardsMouse()
    {
        // მაუსის პოზიციის მიღება მსოფლიო კოორდინატებში
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // გამოთვალეთ მიმართულება პოგოს ჯოხიდან მაუსის პოზიციისკენ
        Vector2 direction = (mousePosition - transform.position).normalized;
        // გამოთვალეთ მობრუნების კუთხე
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        // შექმენით როტაცია გამოთვლილი კუთხისკენ
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        // გლუვად მობრუნდით მიზნის როტაციისკენ
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    void CheckGround()
    {
        // შეამოწმეთ, შეეხო თუ არა პოგოს ჯოხი მიწას
        bool wasGrounded = _isGrounded;
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);

        // თუ პოგოს ჯოხი ახლახან დაეშვა, გამოისახეთ მიწის შეჯახების ნაწილაკები
        if (_isGrounded && !wasGrounded)
        {
            SpawnGroundImpactParticles();
        }
    }

    void SpawnGroundImpactParticles()
    {
        // თუ მიწის შეჯახების ნაწილაკები კომპონენტი მინიჭებულია
        if (_groundImpactParticles != null)
        {
            // ნაწილაკთა სისტემის გადაადგილება მიწის შემოწმების პოზიციაზე
            _groundImpactParticles.transform.position = _groundCheck.position;
            // ნაწილაკთა სისტემის ჩართვა
            _groundImpactParticles.Play();
        }
    }
}
