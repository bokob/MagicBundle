using UnityEngine;

public class PlayerIdleState : State
{
    PlayerFSM _playerFSM;
    Animator _anim;
    Rigidbody2D _rb;

    void Awake()
    {
        _playerFSM = GetComponent<PlayerFSM>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Idle 진입");
        _rb.linearVelocity = Vector2.zero; // Idle 상태에서 속도 초기화
        PlayAnimation();
    }

    public override void OnStateUpdate()
    {
        Debug.Log("Idle 상태");
        _playerFSM.CheckAndSetParry();
        _playerFSM.CheckAndSetAttack();
        _playerFSM.CheckAndSetMove();
        _playerFSM.CheckAndSetJump();
    }

    public override void OnStateExit()
    {
        Debug.Log("Idle 종료");
    }

    public void PlayAnimation()
    {
        _anim.SetFloat("MoveX", 0);
    }
}