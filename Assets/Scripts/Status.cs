using UnityEngine;

public class Status : MonoBehaviour
{
    [field: SerializeField] public bool IsDead { get; protected set; } = false;   // 사망 여부
    [field: SerializeField] public bool IsHit { get; protected set; } = false;    // 피격 여부
    [field: SerializeField] public float MaxHP { get; protected set; }            // 최대 체력
    [field: SerializeField] public float HP { get; protected set; }               // 체력
}