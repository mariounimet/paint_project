using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image img;
    [SerializeField] private Sprite _default;
    [SerializeField] private Sprite _pressed;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        img.sprite = _pressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.sprite = _default;
    }
}
