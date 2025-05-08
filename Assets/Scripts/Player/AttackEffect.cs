using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Play();
        }
    }

    public void Play()
    {
        _anim.Play("Play");
    }
}