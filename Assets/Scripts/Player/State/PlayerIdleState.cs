using UnityEngine;

public class PlayerIdleState : State
{
    PlayerFSM _playerFSM;

    void Awake()
    {
        _playerFSM = GetComponent<PlayerFSM>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Idle 진입");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("Idle 상태");
        _playerFSM.CheckAndSetParry();
        _playerFSM.CheckAndSetAttack();
        _playerFSM.CheckAndSetMove();
    }

    public override void OnStateExit()
    {
        Debug.Log("Idle 종료");
    }
}