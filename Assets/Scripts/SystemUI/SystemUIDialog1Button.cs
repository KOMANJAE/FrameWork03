using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 단일 버튼 다이얼로그
/// </summary>
//[AddComponentMenu("Utage/Lib/System UI/SystemUiDialog1Button")]
public class SystemUIDialog1Button : MonoBehaviour
{
    /// <summary>
    /// 본문 표시용 텍스트
    /// </summary>
    [SerializeField]
    protected Text titleText;

    /// <summary>
    /// 버튼1용 텍스트
    /// </summary>
    [SerializeField]
    protected Text button1Text;

    /// <summary>
    /// 버튼1을 눌렀을 때의 이벤트
    /// </summary>
    [SerializeField]
    protected UnityEvent OnClickButton1;

    /// <summary>
    /// 다이얼로그 열기
    /// </summary>
    /// <param name="text">표시 텍스트</param>
    /// <param name="buttonText1">버튼1의 텍스트</param>
    /// <param name="target">버튼을 눌렀을 때 호출되는 콜백</param>
    public virtual void Open(string text, string buttonText1, UnityAction callbackOnClickButton1)
    {
        titleText.text = text;
        button1Text.text = buttonText1;
        this.OnClickButton1.RemoveAllListeners();
        this.OnClickButton1.AddListener(callbackOnClickButton1);
        Open();
    }

    /// <summary>
    /// 버튼1이 눌렸을 때의 처리
    /// </summary>
    public virtual void OnClickButton1Sub()
    {
        OnClickButton1?.Invoke();
        Close();
    }

    /// <summary>
    /// 오픈
    /// </summary>
    public virtual void Open()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 클로즈
    /// </summary>
    public virtual void Close()
    {
        this.gameObject.SetActive(false);
    }
}
