using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    Slider _hpSlider;

    void Awake()
    {
        _hpSlider = GetComponent<Slider>();
        UI_EventBus.OnHPChanged += UpdateHP;
    }

    void UpdateHP(float hp)
    {
        _hpSlider.value = hp;
    }
}