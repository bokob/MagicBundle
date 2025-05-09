using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static Define;

public class UI_MagicBundleInner : MonoBehaviour
{
    [SerializeField] RectTransform _pivot;
    [SerializeField] GameObject _slotPrefab;
    [SerializeField] List<RectTransform> _slotList = new List<RectTransform>();

    [SerializeField] float _radius = 100f;
    [SerializeField] float _rotateDuration = 0.2f;
    [SerializeField] float _currentAngle = 0f;
    [SerializeField] float _targetAngle = 0f;

    void Start()
    {
        _pivot = GetComponentInParent<RectTransform>();
    }

    void Update()
    {
        Rotate();
    }

    // 슬롯 UI 갱신
    public void UpdateSlots(List<ElementalType> magicBundle)
    {
        // 기존 슬롯 제거
        foreach (var slot in _slotList)
        {
            Destroy(slot.gameObject);
        }
        _slotList.Clear();

        // 새로운 슬롯 추가
        int index = 0;
        foreach (var magic in magicBundle)
        {
            GameObject slot = Instantiate(_slotPrefab, _pivot);
            RectTransform slotRect = slot.GetComponent<RectTransform>();
            slot.name = $"Slot_{index}";
            _slotList.Add(slotRect);

            TextMeshProUGUI text = slot.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
                text.text = magic.ToString();

            index++;
        }

        _currentAngle = 0f;
        _targetAngle = 0f;

        UpdateSlotPositions();
    }

    // 슬롯 회전
    public void RotateSlot(bool isCCW = false)
    {
        if (_slotList.Count <= 1)
            return;

        float slotAngle = 360f / _slotList.Count;
        _targetAngle += isCCW ? -slotAngle : slotAngle;
        _targetAngle = NormalizeAngle(_targetAngle);
    }

    void Rotate()
    {
        _currentAngle = Mathf.Lerp(_currentAngle, _targetAngle, Time.deltaTime / _rotateDuration);

        if (Mathf.Abs(_currentAngle - _targetAngle) < 0.1f)
        {
            _currentAngle = _targetAngle;
        }

        UpdateSlotPositions();
    }

    // 슬롯 위치 업데이트
    void UpdateSlotPositions()
    {
        int slotCount = _slotList.Count;
        if (slotCount == 0)
            return;

        for (int i = 0; i < slotCount; i++)
        {
            float angle = (i * Mathf.PI * 2 / slotCount) + Mathf.Deg2Rad * _currentAngle;
            Vector2 position = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * _radius;
            _slotList[i].anchoredPosition = position;
        }
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0) angle += 360f;
        return angle;
    }
}