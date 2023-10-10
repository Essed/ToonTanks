using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    [SerializeField]
    private float deadZoneX; // Мертвая зона джойстика по оси Х
    [SerializeField]
    private float deadZoneY; // Мертвая зона джойстика по оси Y

    private Vector2 inputVector; // координаты джойстика

    private Image joystick; // Джойстик
    private Image joystickBG; // Задник джойстика


    private void Start()
    {
        SetJoystick();
    }


    public virtual void OnDrag(PointerEventData eventData)
    {
        MoveJoystick(eventData);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
    
    private void SetJoystick()
    {
        joystickBG = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }

    
    private void MoveJoystick(PointerEventData eventData)
    {
        Vector2 pos;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;


            joystick.rectTransform.anchoredPosition =
                new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x * 0.5f), inputVector.y * (joystickBG.rectTransform.sizeDelta.y * 0.5f));
        }
    }

    public float Horizontal()
    {
            if (inputVector.x != 0 && Mathf.Abs(inputVector.y) > deadZoneX)
            {
                return inputVector.x;
            }
            else return Input.GetAxis("Horizontal");

    }

    public float Vertical()
    {
        if (inputVector.y != 0 && Mathf.Abs(inputVector.y) > deadZoneX)
        {
            return inputVector.y;
        }
        else return Input.GetAxis("Vertical");

    }

}
