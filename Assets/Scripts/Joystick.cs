using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    /*
        RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, Vector2, Camera, out Vector2)
        첫번째 인자값 RectTransform에는 Area를 넣어줌
        두번째 인자값 Vector2는 상속받은 인터페이스 함수의 매개변수에 있는 eventData.point를 사용
        세번째 인자값 Camera는 상속받은 인터페이스 함수의 매개변수에 있는 eventData.pressEventCamera를 사용
        네번째 인자값 out Vector2에는 localVector가 반환되서 나오니 선언하여 넣어주면 됨
        설명을 하자면 touch한 부분의 좌표를 ScreenPoint에서 LocalPoint로 바꿔주는 부분인 것
        그래서 ScreenPoint에 대한 정보를 두번째, 세번째 인자값에 넣어주는 것
     */

    public RectTransform touchArea;
    public Image inner;

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(touchArea, eventData.position, eventData.pressEventCamera, out Vector2 localVector))
        {
            if (localVector.magnitude < 200) //outer안에 있다면 //200이랑 수치는 임의로 작성
                inner.transform.localPosition = localVector;
            else
                inner.transform.localPosition = localVector.normalized * 200;

            //localVector를 가지고 놀기
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); //OnDrag와 같은 로직
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inner.transform.localPosition = Vector2.zero; //inner의 위치를 복구 시켜줌
    }

    //반환 받은 localVector를 가지고 플레이어를 회전시키거나 이동하거나 하면 끝
}
