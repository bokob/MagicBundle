using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Define;

public class UI_MagicBundleOuter : MonoBehaviour
{
    [SerializeField] GameObject _slotPrefab;
    [SerializeField] List<RectTransform> _slotList = new List<RectTransform>();

    // 슬롯 UI 갱신
    public void UpdateSlots(List<ElementalType> reloadMagicList)
    {
        // 기존 슬롯 제거
        foreach (var slot in _slotList)
        {
            Destroy(slot.gameObject);
        }
        _slotList.Clear();

        // 새로운 슬롯 추가
        int index = 0;
        foreach (var magic in reloadMagicList)
        {
            GameObject slot = Instantiate(_slotPrefab, transform);
            RectTransform slotRect = slot.GetComponent<RectTransform>();
            slot.name = $"Slot_{index}";
            _slotList.Add(slotRect);

            TextMeshProUGUI text = slot.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
                text.text = magic.ToString();

            index++;
        }
    }
}
