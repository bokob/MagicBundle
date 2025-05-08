using UnityEngine;

public class BaseScene : MonoBehaviour
{
    public Define.SceneType SceneType { get; protected set; } = Define.SceneType.None;
    void Awake()
    {
        Init();
    }

    void OnDestroy()
    {
        Clear();
    }

    // 자식 씬에서 각자 실행할 것들
    public virtual void Init() { }
    public virtual void Clear() { }
}