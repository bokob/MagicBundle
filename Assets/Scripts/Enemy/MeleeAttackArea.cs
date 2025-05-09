using System.Collections;
using UnityEngine;
using static Define;

public class MeleeAttackArea : MonoBehaviour, IParryable
{
    public ParryType ParryType => ParryType.Melee;
    public Transform ParriedTransform => transform.root;
    [field: SerializeField] public ElementalType ElementalType { get; set; }

    float _fadeSpeed = 1f;
    SpriteRenderer _spriteRenderer;
    BoxCollider2D _collider;
    Coroutine _coroutine;
    LayerMask _playerMask = 1 << 3; // 플레이어 레이어 마스크


    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();

        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }

    #region 범위 공격
    // 영역 활성화 시작
    public void StartActiveArea()
    {
        if (_coroutine == null)
        {
            _spriteRenderer.color = new Color(1, 0, 0, 0);
            _spriteRenderer.enabled = true;
            _collider.enabled = false;
            _coroutine = StartCoroutine(ActiveAreaCoroutine());
        }
    }

    // 영역 활성화 코루틴
    IEnumerator ActiveAreaCoroutine()
    {
        Color color = _spriteRenderer.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime * _fadeSpeed;
            color.a = Mathf.Min(color.a, 1f); // 안전하게 1 초과 방지
            _spriteRenderer.color = color;
            yield return null;
        }

        Collider2D collider2D = Physics2D.OverlapBox(transform.position, _collider.size, 0, _playerMask);
        if (collider2D && collider2D.TryGetComponent<PlayerParryState>(out PlayerParryState playerParry))
            playerParry.ReceiveAttack(this);

        // 알파값이 1이 되면 콜라이더 활성화
        _collider.enabled = true;
        InActiveArea();
        _coroutine = null;
    }

    // 영역 비활성화
    void InActiveArea()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }
    #endregion

    public void Parried(Transform attaker)
    {
        Debug.Log("근접 공격 패링 성공");
        Rigidbody2D enemyRb = transform.root.GetComponent<Rigidbody2D>();
        if (enemyRb != null)
            enemyRb.AddForce((transform.root.position - attaker.position) * 5f, ForceMode2D.Impulse);
    }

    void OnDrawGizmos()
    {
        if (_collider == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, _collider.size);
    }
}