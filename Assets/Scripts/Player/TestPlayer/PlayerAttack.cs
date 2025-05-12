using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator _anim;
    Coroutine _attackCoroutine;
    PlayerController _playerController;
    MagicBundle _magicBundle;

    [field: SerializeField] public bool IsAttack { get; private set; }
    void Start()
    {
        _anim = GetComponent<Animator>();
        _magicBundle = GetComponentInChildren<MagicBundle>();
        _playerController = GetComponent<PlayerController>();

        Managers.Input.attackAction += StartAttackCoroutine;
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
            _playerController.Flip(dir.x);

        Debug.Log("공격");
        _magicBundle.ReleaseMagic();

        //_anim.SetTrigger("AttackTrigger");

        // 애니메이션 길이만큼 대기 (예: 0.5초)
        yield return new WaitForSeconds(0.5f);

        IsAttack = false;
        _attackCoroutine = null;
    }
}