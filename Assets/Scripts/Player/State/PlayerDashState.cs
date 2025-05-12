using System.Collections;
using UnityEngine;

public class PlayerDashState : State
{
    PlayerFSM _playerFSM;
    Animator _anim;
    Rigidbody2D _rb;

    Vector2 _moveInput;
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
        _moveInput = Managers.Input.MoveInput;
        StartCoroutine(DashCoroutine());
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {
        if(!IsDash)
            _playerFSM.SetState(PlayerState.Idle); // 대시 중이 아닐 때 Idle 상태로 전환

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