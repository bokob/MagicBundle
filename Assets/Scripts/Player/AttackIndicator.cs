using UnityEngine;

public class AttackIndicator : MonoBehaviour
{
    Transform _player;
    float _radius = 1f;

    void Start()
    {
        _player = transform.root;
    }

    void Update()
    {
        Vector2 mouseWorldPos = Managers.Input.MouseWorldPos;
        Vector2 dir = (mouseWorldPos - (Vector2)_player.position).normalized;
        transform.position = (Vector2)_player.position + dir * _radius;

        // 인디케이터가 방향을 바라보게 회전
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}