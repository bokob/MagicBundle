using UnityEngine;

public class PlayerJumpState : State
{
    PlayerFSM _playerFSM;
    Animator _anim;
    Rigidbody2D _rb;

    void Start()
    {
        _playerFSM = GetComponent<PlayerFSM>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Jump 진입");
        _rb.AddForceY(100f); // 점프 힘 추가
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateUpdate()
    {

    }
}