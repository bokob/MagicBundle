using UnityEngine;
using static Define;

public class Projectile : MonoBehaviour, IParryable
{
    public ParryType ParryType => ParryType.Projectile; // 패링 타입
    public Transform ParriedTransform => transform;     // 본체

    [field: SerializeField] public ElementalType ElementalType { get; set; }   // 원소타입

    float _speed;
    float _parriedSpeedCoefficient = 1.5f;
    Rigidbody2D _rb;
    Transform _master;     // 투사체 주인

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
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

    public void Launch(Transform master, Vector3 direction, float speed = 1f)
    {
        _master = master;
        _speed = speed;
        _rb.linearVelocity = Vector2.zero; // 초기화
        transform.right = direction;
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
        if (_master == null && collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            CameraController.Instance.ShakeCamera(5f, 0.1f);
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            //if (_master != null && collision.TryGetComponent<PlayerParryState>(out PlayerParryState playerParry))
            //{
            //    playerParry.ReceiveAttack(this);
            //}
            if (_master != null && collision.TryGetComponent<PlayerParry>(out PlayerParry playerParry))
            {
                playerParry.ReceiveAttack(this);
            }
        }
    }
}