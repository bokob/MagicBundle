using UnityEngine;
using static Define;

public class Projectile : MonoBehaviour, IParryable
{
    public ParryType ParryType => ParryType.Projectile;
    public Transform ParriedTransform => transform;

    public ElementalType ElementalType => _elementalType;
    [SerializeField] ElementalType _elementalType;

    float _speed;
    float _parriedSpeedCoefficient = 1.5f;
    Rigidbody2D _rb;
    Collider2D _collider;
    Transform _master;     // 투사체 주인


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    // 목표를 향해 발사
    public void SetTarget(Transform master, Transform target, float speed = 1f)
    {
        _master = master;
        _speed = speed;
        _rb.linearVelocity = Vector2.zero; // 초기화
        transform.right = target.position - transform.position;
        _rb.AddForce(transform.right * _speed, ForceMode2D.Impulse);
    }

    // 패링 당했을 때
    public void Parried(Transform attacker)
    {
        Debug.Log("투사체 패링");
        _master = null;
        _rb.linearVelocity = Vector2.zero;  // rb 초기화
        transform.right = -transform.right; // 진행 방향의 반대
        _rb.AddForce(transform.right * _speed * _parriedSpeedCoefficient, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //if(collision.TryGetComponent<PlayerParry>(out PlayerParry playerParry))
            //{
            //    playerParry.ReceiveAttack(this);
            //}
            if (collision.TryGetComponent<PlayerParryState>(out PlayerParryState playerParry))
            {
                playerParry.ReceiveAttack(this);
            }
        }
    }
}