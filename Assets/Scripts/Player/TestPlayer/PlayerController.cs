using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _anim;
    Rigidbody2D _rb;
    Transform _visual;
    BoxCollider2D _boxCollider;

    float _moveSpeed = 6f;
    float _maxMoveSpeed = 10f;
    Vector2 _moveInput;

    [SerializeField] float _jumpForce = 250f;

    [SerializeField] bool _isGround = false;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _visual = transform.Find("Visual");
    }

    void Update()
    {
        if(GetIsGround())
            Jump();
    }

    void FixedUpdate()
    {
        _moveInput = Managers.Input.MoveInput;
        if (_moveInput != Vector2.zero)
        {
            Move();
        }
    }

    void Move()
    {
        if (_rb.linearVelocity.magnitude < _maxMoveSpeed)
        {
            Flip(_moveInput.x);
            _rb.linearVelocityX = _moveInput.x * _moveSpeed;
        }
    }

    public void Flip(float x)
    {
        if (x == 0) return;
        Quaternion rotation = (x>0) ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
        _visual.rotation = rotation;
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * _jumpForce);
        }
    }

    public bool GetIsGround()
    {
        _isGround = Physics2D.OverlapCircle(transform.position + Vector3.down * _boxCollider.size.y * 0.45f, 0.1f, LayerMask.GetMask("Ground"));
        return _isGround;
    }

    void OnDrawGizmos()
    {
        if (_boxCollider == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * _boxCollider.size.y * 0.45f, 0.1f);
    }
}