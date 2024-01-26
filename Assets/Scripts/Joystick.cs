using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    /*
        RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, Vector2, Camera, out Vector2)
        ù��° ���ڰ� RectTransform���� Area�� �־���
        �ι�° ���ڰ� Vector2�� ��ӹ��� �������̽� �Լ��� �Ű������� �ִ� eventData.point�� ���
        ����° ���ڰ� Camera�� ��ӹ��� �������̽� �Լ��� �Ű������� �ִ� eventData.pressEventCamera�� ���
        �׹�° ���ڰ� out Vector2���� localVector�� ��ȯ�Ǽ� ������ �����Ͽ� �־��ָ� ��
        ������ ���ڸ� touch�� �κ��� ��ǥ�� ScreenPoint���� LocalPoint�� �ٲ��ִ� �κ��� ��
        �׷��� ScreenPoint�� ���� ������ �ι�°, ����° ���ڰ��� �־��ִ� ��
     */

    public RectTransform touchArea;
    public Image inner;

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(touchArea, eventData.position, eventData.pressEventCamera, out Vector2 localVector))
        {
            if (localVector.magnitude < 200) //outer�ȿ� �ִٸ� //200�̶� ��ġ�� ���Ƿ� �ۼ�
                inner.transform.localPosition = localVector;
            else
                inner.transform.localPosition = localVector.normalized * 200;

            //localVector�� ������ ���
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); //OnDrag�� ���� ����
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inner.transform.localPosition = Vector2.zero; //inner�� ��ġ�� ���� ������
    }

    //��ȯ ���� localVector�� ������ �÷��̾ ȸ����Ű�ų� �̵��ϰų� �ϸ� ��
}
