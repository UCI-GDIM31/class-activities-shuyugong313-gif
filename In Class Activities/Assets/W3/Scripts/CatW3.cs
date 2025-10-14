using TMPro;
using UnityEngine;

public class CatW3 : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed;
    [SerializeField] private float _jump;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _pointsText;
    [SerializeField] private TMP_Text _speechText;
    [SerializeField] private float _maxHealth = 10;
    [SerializeField] private bool _destroyCatWhenDead;

    private bool _facingLeft;
    private bool _isGrounded = true;
    private int _points = 0;
    private float _health;
    private int _ballHitCount = 0; // Add hit count variable

    private void Start()
    {
        _health = _maxHealth;

        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
        if (_animator == null) _animator = GetComponent<Animator>();
        if (_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();

        _healthText.text = "health = " + _health;
        _pointsText.text = "points: " + _points;
    }

    private void Update()
    {
        _rigidbody.linearVelocity = new Vector2(
            Input.GetAxis("Horizontal") * _speed,
            _rigidbody.linearVelocity.y
        );

        if (Input.GetAxis("Vertical") > 0 && _isGrounded)
        {
            _isGrounded = false;

            _rigidbody.linearVelocity = new Vector2(
                _rigidbody.linearVelocity.x,
                _jump
            );
        }

        if (Input.GetAxis("Horizontal") < 0 && !_facingLeft)
        {
            _spriteRenderer.flipX = true;
            _facingLeft = true;
        }
        else if (Input.GetAxis("Horizontal") > 0 && _facingLeft)
        {
            _spriteRenderer.flipX = false;
            _facingLeft = false;
        }

        _animator.SetBool("walking", _rigidbody.linearVelocity.x != 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Item"))
        {
            _points++;
            _pointsText.text = "points: " + _points;
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("ground"))
        {
            _isGrounded = true;
        }

        BallW3 ball = collision.gameObject.GetComponent<BallW3>();
        if (ball != null)
        {
            ChangeColor(ball);
            _ballHitCount++; // Increase hit count when ball hits cat
            DecreaseHealth();

            if (_health <= 0 && _destroyCatWhenDead)
            {
                DestroyCat();
            }

            // Check if ball hits cat 10 times, then destroy cat
            if (_ballHitCount >= 10)
            {
                DestroyCat();
            }
        }
    }

    private void DecreaseHealth()
    {
        _health--;
        _healthText.text = "health = " + _health;
        _speechText.text = GetHealthSpeechText();
    }

    private string GetHealthSpeechText()
    {
        if (_health < _maxHealth / 2)
        {
            return "OH NO!";
        }
        else
        {
            return "ouch";
        }
    }

    private void ChangeColor(BallW3 ball)
    {
        _spriteRenderer.color = ball.ballRenderer.color;
    }

    private void DestroyCat()
    {
        Destroy(gameObject);
    }
}