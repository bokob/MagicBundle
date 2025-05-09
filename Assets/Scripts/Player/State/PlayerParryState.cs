using System.Collections;
using UnityEngine;

public class PlayerParryState : State
{
    PlayerFSM _playerFSM;
    Animator _anim;
    [SerializeField] bool _isParry = false;          // 패링 상태 여부
    [SerializeField] bool _isParryWindow = false;    // 패링 판정 시간에 들어왔는지 여부
    [SerializeField] float _parryWindow = 0.2f;      // 패링 윈도우 시간 (판정 시간)
    float _knockBackForce = 5f;                      // 넉백 힘

    Rigidbody2D _rb;
    Coroutine _parryCoroutine;
    PlayerStatus _playerStatus;
    Shouting _shouting;
    PlayerText _playerText;
    MagicBundle _magicBundle;

    void Start()
    {
        _playerFSM = GetComponent<PlayerFSM>();
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _playerStatus = GetComponent<PlayerStatus>();
        _shouting = GetComponentInChildren<Shouting>();
        _playerText = GetComponentInChildren<PlayerText>();

        _magicBundle = GetComponentInChildren<MagicBundle>();
    }

    public override void OnStateEnter()
    {
        Debug.Log("Parry 진입");
        StartParryCoroutine();
    }

    public override void OnStateUpdate()
    {
        if (!Managers.Input.IsPressParry)
            _playerFSM.SetState(PlayerState.Idle); // 패링 버튼을 떼면 Idle 상태로 전환
    }

    public override void OnStateExit()
    {
        Debug.Log("Parry 종료");
        StopParry();
    }

    public void StartParryCoroutine()
    {
        if (_parryCoroutine != null)
            StopCoroutine(_parryCoroutine);
        _parryCoroutine = StartCoroutine(ParryCoroutine());
    }

    IEnumerator ParryCoroutine()
    {
        _anim.SetTrigger("AttackTrigger");
        _isParry = true;
        _isParryWindow = true;
        yield return new WaitForSeconds(_parryWindow);
        _isParryWindow = false;
        _parryCoroutine = null;
    }

    // 패리 해제
    public void StopParry()
    {
        _isParryWindow = false;
        _isParry = false;
    }

    #region 패링
    void TryParry(IParryable attacker)
    {
        _shouting.Play();
        _playerText.StartShowTextCoroutine(0);

        //attacker.Parried(transform);
        attacker.ParriedTransform.gameObject.SetActive(false);

        if(attacker.ParryType == ParryType.Melee)
        {
            Debug.Log("근접 공격 패링");
        }
        else if (attacker.ParryType == ParryType.Projectile)
        {
            _magicBundle.AddMagic(attacker.ElementalType);
        }

        

        //if (attacker.ParryType == ParryType.Projectile)
        //{
        //    Destroy(attacker.ParriedTransform.gameObject);
        //}
    }
    #endregion

    // 공격 받는 함수 (적이 호출)
    public void ReceiveAttack(IParryable attacker)
    {
        if (_isParryWindow)
        {
            TryParry(attacker);
        }
        else // 못 막은 경우 데미지 입고 넉백
        {

            if (attacker.ParryType == ParryType.Projectile)
            {
                Destroy(attacker.ParriedTransform.gameObject);
            }

            _playerText.StartShowTextCoroutine(1);
            _playerStatus.TakeDamage(5);
            KnockBack(attacker);
        }
    }

    // 넉백
    public void KnockBack(IParryable attacker)
    {
        Vector2 knockBack = (transform.position - attacker.ParriedTransform.position).normalized * _knockBackForce;
        _rb.AddForce(knockBack, ForceMode2D.Impulse);
    }
}