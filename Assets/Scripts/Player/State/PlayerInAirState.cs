using UnityEngine;

public class PlayerInAirState : State
{
    PlayerFSM _playerFSM;
    Animator _anim;
    Rigidbody2D _rb;

    public override void OnStateEnter()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStateUpdate()
    {
        throw new System.NotImplementedException();
    }

    void Awake()
    {
        _playerFSM = GetComponent<PlayerFSM>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }
}
