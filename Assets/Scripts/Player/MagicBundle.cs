using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MagicBundle : MonoBehaviour
{
    static MagicBundle _instance;
    public static MagicBundle Instance => _instance;

    [SerializeField] List<ElementalType> _magicBundle = new List<ElementalType>(); // 패링 가능한 공격 리스트
    [SerializeField] List<ElementalType> _reloadList = new List<ElementalType>();  // 장전 리스트(최대 2개)

    UI_MagicBundleOuter _uiMagicBundleOuter;
    UI_MagicBundleInner _uiMagicBundleInner;

    int _currentSlotCount = 0; // 현재 슬롯 개수
    int _maxSlotCount = 4;     // 최대 슬롯 개수

    PlayerStatus _playerStatus;

    void Awake()
    {
        if(_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        _uiMagicBundleOuter = FindAnyObjectByType<UI_MagicBundleOuter>();
        _uiMagicBundleInner = FindAnyObjectByType<UI_MagicBundleInner>();
        Managers.Input.rotateMagicAction += RotateMagic;
        Managers.Input.reloadMagicAction += ReloadMagic;

        _playerStatus = GetComponentInParent<PlayerStatus>();
    }

    // 마법 회전 (시계 방향 기본)
    void RotateMagic(bool isCCW = false)
    {
        if (_magicBundle.Count == 0)
            return;

        // 빼기
        int removeIdx = isCCW ? 0 : _magicBundle.Count - 1;
        ElementalType magic = _magicBundle[removeIdx];
        _magicBundle.RemoveAt(removeIdx);

        // 추가
        int insertIdx = isCCW ? _magicBundle.Count : 0;
        _magicBundle.Insert(insertIdx, magic);

        // UI 업데이트
        _uiMagicBundleInner.RotateSlot(isCCW);
    }

    // 추가
    public void AddMagic(ElementalType magic)
    {
        if (_magicBundle.Count + _reloadList.Count >= _maxSlotCount)
        {
            OverCharge();
            return;
        }

        _magicBundle.Add(magic);
        _uiMagicBundleInner.UpdateSlots(_magicBundle);
    }
    
    // 장전(마법 조합을 위해 입구에 넣는 것)
    public void ReloadMagic()
    {
        if(_reloadList.Count >= 2 || _magicBundle.Count == 0)
            return;

        // 마법 보따리 내부에서 빼기
        ElementalType elementalType = _magicBundle[0];
        _magicBundle.RemoveAt(0);

        // 마법 보따리 입구(장전 리스트)에 넣기
        _reloadList.Add(elementalType);

        // UI 업데이트
        _uiMagicBundleOuter.UpdateSlots(_reloadList);
        _uiMagicBundleInner.UpdateSlots(_magicBundle);
    }

    // 방출
    public void ReleaseMagic()
    {
        if (_reloadList.Count == 0)
            return;

        // 마법 보따리 입구(장전 리스트)에서 빼기
        ElementalType elementalType = ElementalType.None;
        if (_reloadList.Count == 2)
        {
            elementalType = GetCombineMagic(_reloadList);
            _reloadList.RemoveAt(0);
            _reloadList.RemoveAt(0);
        }
        else
        {
            elementalType = _reloadList[0];
            _reloadList.RemoveAt(0);
        }

        Transform target = FindAnyObjectByType<Enemy>().transform;
        string magicName = elementalType.ToString();
        Debug.Log($"{magicName} 생성");
        Transform projectile = Managers.Resource.Instantiate(magicName).transform;
        projectile.position = transform.position;
        //projectile.GetComponent<Projectile>().SetTarget(null, target, 10f);


        Vector3 direction = (Vector3)Managers.Input.MouseWorldPos - transform.position;
        projectile.GetComponent<Projectile>().Launch(null, direction, 10f);

        CameraController.Instance.ShakeCamera(5f, 0.1f); // 카메라 흔들기

        // UI 업데이트
        _uiMagicBundleOuter.UpdateSlots(_reloadList);
        _uiMagicBundleInner.UpdateSlots(_magicBundle);
    }

    // 원소 조합
    public ElementalType GetCombineMagic(List<ElementalType> elementalTypeList)
    {
        int combine = (1 << (int)elementalTypeList[0]) | (1 << (int)elementalTypeList[1]);
        ElementalTypeCombine elementalTypeCombine = Util.IntToEnum<ElementalTypeCombine>(combine);
        ElementalType elementalType = Util.StringToEnum<ElementalType>(elementalTypeCombine.ToString());


        Debug.LogWarning($"조합된 마법: {elementalType} ({elementalTypeList[0]}, {elementalTypeList[1]})\n" +
            $"결합된 결과(ElementalTypeCombine): {elementalTypeCombine}\n" +
            $"결합된 결과(ElementalType): {elementalType.ToString()}");

        return elementalType;
    }

    // 최대 마법 허용치일 때 마법 흡수 시, MP 증가
    public void OverCharge()
    {
        if(_magicBundle.Count + _reloadList.Count >= _maxSlotCount)
        {
            Debug.LogWarning("마법 보따리 과충전");
            _playerStatus.OverCharge(10f);
        }
    }
}