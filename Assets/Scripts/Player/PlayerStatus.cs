using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : Status, IDamagable
{
    //[field: SerializeField] public bool IsDash { get; private set; }            // 대시 여부
    [field: SerializeField] public float MaxMP { get; private set; }
    [field: SerializeField] public float MP { get; private set; }
    WaitForSeconds _hitWait = new WaitForSeconds(0.5f);

    [SerializeField] float healMPAmount = 0.025f;
    void Start()
    {
        // 초기화
        MaxHP = 100;
        HP = MaxHP;
        MaxMP = 100;
        MP = 0;
    }

    void Update()
    {
        HealMP();
    }

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        StartCoroutine(HitCoroutine());
        Debug.Log($"데미지: {damage}");
        HP = Mathf.Clamp(HP - damage, 0, MaxHP);
        UI_EventBus.OnHPChanged?.Invoke(HP);

        if (HP <= 0)
        {
            // 사망 처리
            IsDead = true;
            Debug.Log("사망");
        }
    }

    public void OverCharge(float amount)
    {
        MP = Mathf.Clamp(MP + amount, 0, MaxMP);
        UI_EventBus.OnMPChanged?.Invoke(MP);
    }

    IEnumerator HitCoroutine()
    {
        IsHit = true;
        yield return _hitWait;
        IsHit = false;
    }

    public void HealMP()
    {
        if(MP >= MaxMP) return;
        MP = Mathf.Clamp(MP + healMPAmount, 0, MaxMP);
        UI_EventBus.OnMPChanged?.Invoke(MP);
    }

    public void PayUltimate()
    {
        if (MP >= 100)
        {
            MP = Mathf.Clamp(MP - 100, 0, MaxMP);
            UI_EventBus.OnMPChanged?.Invoke(MP);
        }
    }
}