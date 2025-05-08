using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    None,
    Idle,
    Move,
    Attack,
    Parry,
    Hit,
    Die,
}

public class PlayerFSM : MonoBehaviour
{
    [field: SerializeField] public State CurrentState { get; set; } // 현재 상태
    Dictionary<PlayerState, State> _stateDictionary = new Dictionary<PlayerState, State>();
    SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();

        _stateDictionary.Add(PlayerState.Idle, gameObject.AddComponent<PlayerIdleState>());
        _stateDictionary.Add(PlayerState.Move, gameObject.AddComponent<PlayerMoveState>());
        _stateDictionary.Add(PlayerState.Attack, gameObject.AddComponent<PlayerAttackState>());
        _stateDictionary.Add(PlayerState.Die, gameObject.AddComponent<PlayerDieState>());
        _stateDictionary.Add(PlayerState.Parry, gameObject.AddComponent<PlayerParryState>());
        SetState(PlayerState.Idle); // 초기 상태 설정
    }

    void Update()
    {
        Do();
    }

    public void SetState(PlayerState state)
    {
        if(CurrentState != null)
            CurrentState.OnStateExit();
        CurrentState = _stateDictionary[state];
        CurrentState.OnStateEnter();
    }

    public void Do()
    {
        if(CurrentState != null)
            CurrentState.OnStateUpdate();
    }

    public void Flip(float x)
    {
        _spriteRenderer.flipX = (x > 0) ? true : false;
    }

    #region 각 상태로의 전환 검사 및 전환
    // Idle 전환 검사
    public void CheckAndSetIdle()
    {
        if (Managers.Input.MoveInput == Vector2.zero)
            SetState(PlayerState.Idle);
    }

    public void CheckAndSetMove()
    {
        if (Managers.Input.MoveInput != Vector2.zero)
            SetState(PlayerState.Move);
    }

    // Attack 전환 검사
    public void CheckAndSetAttack()
    {
        if (Managers.Input.IsPressAttack)
            SetState(PlayerState.Attack);
    }

    // Parry 전환 검사
    public void CheckAndSetParry()
    {
        if (Managers.Input.IsPressParry)
            SetState(PlayerState.Parry);
    }
    #endregion
}