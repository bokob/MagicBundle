using UnityEngine;
using System.Collections.Generic;

public class MagicBundle : MonoBehaviour
{
    [SerializeField] List<Transform> _magicBundle = new List<Transform>(); // 패링 가능한 공격 리스트
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 추가
    public void Add(Transform magic)
    {
        _magicBundle.Add(magic);
    }

    // 방출
    public void Release()
    {

    }
}