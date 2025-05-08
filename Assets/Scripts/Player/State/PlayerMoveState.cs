using System.Collections;
using UnityEngine;

public class PlayerMoveState : State
{
    PlayerFSM _playerFSM;
    Animator _anim;
    Rigidbody2D _rb;

    float _moveSpeed = 2f;
    float _maxMoveSpeed = 4f;
    Vector2 _moveInput;

    [Header("대시")]
    [SerializeField] bool _isDash = false;
    public bool IsDash { get; private set; }
    float _dashSpeed = 12.5f;
    float _dashCoolTime = 0.5f;
    Silhouette _silhouette;
    Coroutine _dashCoroutine;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _playerFSM = GetComponent<PlayerFSM>();
        _silhouette = GetComponent<Silhouette>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Move 진입");
    }

    public override void OnStateUpdate()
    {
        _moveInput = Managers.Input.MoveInput;
        _playerFSM.CheckAndSetParry();
        _playerFSM.CheckAndSetAttack();
        _playerFSM.CheckAndSetIdle();
        PlayMoveAnimation();
        Dash();
        Move();
    }

    public override void OnStateExit()
    {
        Debug.Log("Move 종료");
    }

    // Move 로직
    public void Move()
    {
        if (_rb.linearVelocity.magnitude < _maxMoveSpeed)
        {
            _playerFSM.Flip(_moveInput.x);
            _rb.AddForce(_moveInput * _moveSpeed, ForceMode2D.Impulse);
        }
    }

    // Move 애니메이션 재생
    public void PlayMoveAnimation()
    {
        _anim.SetFloat("MoveX", _moveInput.x);
        _anim.SetFloat("MoveY", _moveInput.y);
        if (_moveInput.x == 1 || _moveInput.x == -1 || _moveInput.y == 1 || _moveInput.y == -1)
        {
            _anim.SetFloat("LastMoveX", _moveInput.x);
            _anim.SetFloat("LastMoveY", _moveInput.y);
            _playerFSM.Flip(_moveInput.x);
        }
    }

    public void Dash()
    {
        _isDash = Managers.Input.IsPressDash;
        if (_isDash && _dashCoroutine == null)
        {
            Debug.LogWarning("대시");
            _dashCoroutine = StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        IsDash = true;
        _silhouette.IsActive = true;                        // 대시 중 실루엣 활성화
        _rb.linearVelocity = _moveInput * _dashSpeed;
        // if (_particlesysetem) _particlesysetem.Play();
        yield return new WaitForSeconds(_dashCoolTime);     // 대시 쿨타임
        IsDash = false;
        _silhouette.IsActive = false;                       // 대시 중 실루엣 비활성화
        _dashCoroutine = null;
    }
}