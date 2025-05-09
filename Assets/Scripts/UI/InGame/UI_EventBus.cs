using System;
using UnityEngine;

public class UI_EventBus : MonoBehaviour
{
    public static Action<float> OnHPChanged;
    public static Action<float> OnMPChanged;
}