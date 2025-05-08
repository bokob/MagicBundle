using UnityEngine;

public class Shouting : MonoBehaviour
{
    Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void Play()
    {
        _anim.Play("Play");
    }
}