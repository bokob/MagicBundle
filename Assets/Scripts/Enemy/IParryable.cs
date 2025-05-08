using UnityEngine;

public enum ParryType
{
    None,
    Melee,
    Projectile,
}

// 패링의 대상이 될 오브젝트가 상속받을 인터페이스
public interface IParryable
{
    ParryType ParryType { get; }                // 패링 타입
    Define.ElementalType ElementalType { get; }        // 원소 타입
    Transform ParriedTransform { get; }         // 패링 당할 대상
    public void Parried(Transform attacker);
}