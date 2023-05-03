using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StrenghtJauge : MonoBehaviour, IPointerUpHandler
{
    public UnityEvent<float> ev_SetValue;
    private Scrollbar scrollbar;

    public void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ev_SetValue.Invoke(1 - scrollbar.value);
        scrollbar.value = 1;
    }

    public void Lock(bool value)
    {
        scrollbar.interactable = !value;
    }
}
