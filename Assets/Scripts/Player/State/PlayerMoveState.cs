using System.Collections;
using UnityEngine;

public class PlayerMoveState : State
{
    PlayerFSM _playerFSM;
    Animator _anim;
    Rigidbody2D _rb;

    float _moveSpeed = 4f;
    float _maxMoveSpeed = 5f;
    Vector2 _moveInput;



    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _playerFSM = GetComponent<PlayerFSM>();
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
        _playerFSM.CheckAndSetDash();
        _playerFSM.CheckAndSetIdle();
        PlayAnimation();
        Move();
    }

    public override void OnStateExit()
    {
        Debug.Log("Move 종료");
    }

    public void PlayAnimation()
    {
        _anim.SetFloat("MoveX", Mathf.Abs(_moveInput.x));
    }

    // Move 로직
    public void Move()
    {
        if (_rb.linearVelocity.magnitude < _maxMoveSpeed)
        {
            _playerFSM.Flip(_moveInput.x);
            Vector2 moveVector = new Vector2(_moveInput.x, 0).normalized;
            _rb.linearVelocityX = _moveInput.x * _moveSpeed;
        }
    }


}