using System.Collections;
using UnityEngine;

public class PlayerAttackState : State
{
    PlayerFSM _playerFSM;
    Animator _anim;
    Coroutine _attackCoroutine;

    PlayerParryState _playerParryState;

    [field : SerializeField] public bool IsAttack { get; private set; }
    void Start()
    {
        _anim = GetComponent<Animator>();
        _playerFSM = GetComponent<PlayerFSM>();
        _playerParryState = GetComponent<PlayerParryState>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Attack 진입");
        StartAttackCoroutine();
    }

    public override void OnStateUpdate()
    {
        _playerFSM.SetState(PlayerState.Idle);
    }

    public override void OnStateExit()
    {
        Debug.Log("Attack 종료");
        IsAttack = false;
    }

    public void StartAttackCoroutine()
    {
        if (_attackCoroutine == null)
            _attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        IsAttack = true;

        Vector3 mousePos = Managers.Input.MouseWorldPos;
        Vector2 dir = (mousePos - transform.position).normalized;
        Debug.Log(dir);
        if (dir.x != 0)
            _playerFSM.Flip(dir.x);

        _playerParryState.ReleaseBunddle();
        _anim.SetTrigger("AttackTrigger");

        // 애니메이션 길이만큼 대기 (예: 0.5초)
        yield return new WaitForSeconds(0.5f);

        IsAttack = false;
        _attackCoroutine = null;
    }
}
