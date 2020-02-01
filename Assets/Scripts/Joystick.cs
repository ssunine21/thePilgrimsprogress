using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image backgroundImg;
    private Image joystickImg;
    private Vector3 inputVector;

    private void Start() {
        backgroundImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData ptnData) {
        Debug.Log("Joystick >>> OnDrag()");

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImg.rectTransform, ptnData.position, null, out Vector2 pos)) {

            pos.x = (pos.x / backgroundImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / backgroundImg.rectTransform.sizeDelta.y);

            //Debug.Log(pos.x + ", " + pos.y);

            inputVector = new Vector3(pos.x * 2, pos.y * 2, 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            //move Joystick
            joystickImg.rectTransform.anchoredPosition = new Vector3(inputVector.x * (backgroundImg.rectTransform.sizeDelta.x / 3)
                ,inputVector.y * (backgroundImg.rectTransform.sizeDelta.y / 3));
        }
    }

    public virtual void OnPointerDown(PointerEventData ptnData) {
        OnDrag(ptnData);
    }

    public virtual void OnPointerUp(PointerEventData ptnData) {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

    public float GetHorizontal() {
        return inputVector.x;
    }

    public float GetVertical() {
        return inputVector.y;
    }
}
