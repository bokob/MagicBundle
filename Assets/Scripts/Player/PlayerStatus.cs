using System.Collections;
using UnityEngine;

public class PlayerStatus : Status, IDamagable
{
    //[field: SerializeField] public bool IsDash { get; private set; }            // 대시 여부
    [field: SerializeField] public float MaxTenacity { get; private set; }      // 최대 맷집
    [field: SerializeField] public float Tenacity { get; private set; }         // 맷집

    WaitForSeconds _hitWait = new WaitForSeconds(0.5f);

    public void TakeDamage(int damage)
    {
        if (IsDead) return;

        StartCoroutine(HitCoroutine());
        Debug.Log($"데미지: {damage}");
        HP = Mathf.Clamp(HP - damage, 0, MaxHP);

        if (HP <= 0)
        {
            // 사망 처리
            IsDead = true;
            Debug.Log("사망");
        }
    }

    IEnumerator HitCoroutine()
    {
        IsHit = true;
        yield return _hitWait;
        IsHit = false;
    }
}