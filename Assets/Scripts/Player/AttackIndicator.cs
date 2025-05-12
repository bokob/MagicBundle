using UnityEngine;

public class AttackIndicator : MonoBehaviour
{
    float _rotationRadius = 1f;
    Transform _player;
    Vector2 _prevMouseWorldPos;

    void Start()
    {
        _player = transform.root;
    }

    void Update()
    {
        if(_prevMouseWorldPos != Managers.Input.MouseWorldPos)
        {
            _prevMouseWorldPos = Managers.Input.MouseWorldPos;
            Vector2 mouseWorldPos = Managers.Input.MouseWorldPos;
            Vector2 dir = (mouseWorldPos - (Vector2)_player.position).normalized;
            transform.position = (Vector2)_player.position + dir * _rotationRadius;

            // 인디케이터가 방향을 바라보게 회전
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}