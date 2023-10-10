using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Thumbstick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    private float deadZoneX; // Мертвая зона тамбстика по оси Х
    [SerializeField]
    private float deadZoneY; // Мертвая зона тамбстика по оси Y

    private Vector2 inputVector; // полученные координаты тамбстика

    private Image thumbstickBG;
    private Image thumbstick;  

    private void Awake()
    {
        thumbstickBG = GetComponent<Image>();
        thumbstick = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;

        thumbstick.rectTransform.anchoredPosition = Vector2.zero; // возврат тамбстика в центр
    }

    public void OnDrag(PointerEventData ped)
    {
        MoveThumbstick(ped);
    }


    // Метод контроллера тамбстика
    private void MoveThumbstick(PointerEventData ped)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(thumbstickBG.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / thumbstickBG.rectTransform.sizeDelta.x); // получение координат позиции касания на тамбстик
            pos.y = (pos.y / thumbstickBG.rectTransform.sizeDelta.y); // получение координат позиции касания на тамбстик            

            inputVector = new Vector2(pos.x * 2, pos.y * 2); // установка точных координаты из касания            

            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            thumbstick.rectTransform.anchoredPosition = 
                new Vector2(inputVector.x * (thumbstickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (thumbstickBG.rectTransform.sizeDelta.y / 2));

        }
    }

    // Метод возврата горизонтальной оси
    public float Horizontal()
    {
        if (inputVector.x != 0 && Mathf.Abs(inputVector.y) > deadZoneX)
        {
            return inputVector.x;
        }
        else return Input.GetAxis("Horizontal");

    }

    // Метод возврата вертикальной оси
    public float Vertical()
    {
        if (inputVector.y != 0 && Mathf.Abs(inputVector.y) > deadZoneX)
        {
            return inputVector.y;
        }
        else return Input.GetAxis("Vertical");

    }
}
