using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform _target;
    [SerializeField] List<Projectile> _projectileList = new List<Projectile>();
    float _throwSpeed = 25f;
    MeleeAttackArea _meleeAttackArea;
    Coroutine _meleeCoroutine;

    float _launchTimer = 0f;
    float _launchTime = 3f; // 발사 시간

    float _targetDistance;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _meleeAttackArea = GetComponentInChildren<MeleeAttackArea>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            RangeAttack();
        }
        if (_launchTimer >= _launchTime)
        {
            _launchTimer = 0f;
            RangeAttack();
        }
        else
        {
            _launchTimer += Time.deltaTime;
        }

        _targetDistance = Vector2.Distance(transform.position, _target.position);
        if (_targetDistance < 3f)
        {
            MeleeAreaAttack();
        }
    }

    // 근접 영역 공격
    void MeleeAreaAttack()
    {
        _meleeAttackArea.StartActiveArea();
    }

    // 원거리 공격
    void RangeAttack()
    {
        Projectile projectilePrefab = _projectileList[Random.Range(0, _projectileList.Count)];
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.SetTarget(transform, _target, _throwSpeed);
    }
}