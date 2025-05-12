using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    float _rotationRadius = 1f;
    Transform _player;
    Vector2 _prevMouseWorldPos;

    void Start()
    {
        _player = transform.root;
    }
}