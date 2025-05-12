using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    None,
    Idle,
    Move,
    Jump,
    Dash,
    Attack,
    InAir,
    Parry,
    Hit,
    Die,
}

public class PlayerFSM : MonoBehaviour
{
    [field: SerializeField] public State CurrentState { get; set; } // 현재 상태
    Dictionary<PlayerState, State> _stateDictionary = new Dictionary<PlayerState, State>();
    SpriteRenderer _spriteRenderer;
    CapsuleCollider2D _capsuleCollider;

    void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _spriteRenderer = transform.Find("Visual").GetComponent<SpriteRenderer>();

        //_stateDictionary.Add(PlayerState.Idle, gameObject.AddComponent<PlayerIdleState>());
        //_stateDictionary.Add(PlayerState.Move, gameObject.AddComponent<PlayerMoveState>());
        //_stateDictionary.Add(PlayerState.Attack, gameObject.AddComponent<PlayerAttackState>());
        //_stateDictionary.Add(PlayerState.Die, gameObject.AddComponent<PlayerDieState>());
        //_stateDictionary.Add(PlayerState.Parry, gameObject.AddComponent<PlayerParryState>());

        foreach (PlayerState playerState in Enum.GetValues(typeof(PlayerState)))
        {
            if (playerState == PlayerState.None)
                continue;

            string playerStateClassName = $"Player{playerState}State";
            Type type = Type.GetType(playerStateClassName);
            if (type != null && typeof(State).IsAssignableFrom(type))
            {
                State stateInstance = gameObject.AddComponent(type) as State;
                if (stateInstance != null)
                    _stateDictionary.Add(playerState, stateInstance);
            }
        }
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
        if (x == 0) return;
        _spriteRenderer.flipX = (x > 0) ? false : true;
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
        if (Managers.Input.MoveInput.x != 0f)
            SetState(PlayerState.Move);
    }

    public void CheckAndSetDash()
    {
        if (Managers.Input.IsPressDash)
            SetState(PlayerState.Dash);
    }

    public void CheckAndSetJump()
    {
        if (Managers.Input.MoveInput.y > 0f && GetIsGround())
            SetState(PlayerState.Jump);
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

    public bool GetIsGround()
    {
        return Physics2D.OverlapCircle(transform.position + Vector3.down * _capsuleCollider.size.y * 0.5f, _capsuleCollider.size.x * 0.5f, LayerMask.GetMask("Ground"));
    }

    void OnDrawGizmos()
    {
        if (_capsuleCollider == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * _capsuleCollider.size.y * 0.5f, 0.1f);
    }
}