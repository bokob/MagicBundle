using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerText : MonoBehaviour
{
    TextMeshPro _playerText;
    Coroutine _showTextCoroutine;

    const string _shoutingText = "호로록!";
    const string _hitText = "크윽... Delicious~";

    void Start()
    {
        _playerText = GetComponent<TextMeshPro>();
    }

    // 사자후(갈)
    public void Shouting()
    {
        _playerText.text = _shoutingText;
        _playerText.fontSize = 5f;
        _playerText.color = Color.red;
    }

    public void Hit()
    {
        _playerText.text = _hitText;
        _playerText.fontSize = 3f;
        _playerText.color = Color.white;
    }

    public void StartShowTextCoroutine(int idx)
    {
        switch(idx)
        {
            case 0:
                Shouting();
                break;
            case 1:
                Hit();
                break;
        }

        if(_showTextCoroutine != null)
            StopCoroutine(_showTextCoroutine);
        _showTextCoroutine = StartCoroutine(ShowTextCoroutine());
    }

    IEnumerator ShowTextCoroutine()
    {
        _playerText.enabled = true;
        yield return new WaitForSeconds(1f);
        _playerText.enabled = false;
    }
}
