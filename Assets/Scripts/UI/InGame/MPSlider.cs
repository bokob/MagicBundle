using UnityEngine;
using UnityEngine.UI;

public class MPSlider : MonoBehaviour
{
    Slider _mpSlider;

    void Awake()
    {
        _mpSlider = GetComponent<Slider>();
        UI_EventBus.OnMPChanged += UpdateMP;
    }

    void UpdateMP(float mp)
    {
        Debug.Log(mp + "이 정도 입니다.");
        _mpSlider.value = mp;
    }
}