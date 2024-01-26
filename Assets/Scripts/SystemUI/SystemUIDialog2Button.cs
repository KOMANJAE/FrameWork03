using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 두 개의 버튼이 있는 다이얼로그
/// </summary>
//[AddComponentMenu("Utage/Lib/System UI/SystemUiDialog2Button")]
public class SystemUIDialog2Button : SystemUIDialog1Button
{

    /// <summary>
    /// 버튼2용 텍스트 영역
    /// </summary>
    [SerializeField]
    protected Text button2Text;

    /// <summary>
    /// 버튼2를 눌렀을 때의 이벤트
    /// </summary>
    [SerializeField]
    protected UnityEvent OnClickButton2;

    /// <summary>
    /// 두 버튼 다이얼로그를 열다
    /// </summary>
    /// <param name="text">표시 텍스트</param>
    /// <param name="buttonText1">버튼1의 텍스트</param>
    /// <param name="buttonText2">버튼2의 텍스트</param>
    /// <param name="callbackOnClickButton1">버튼1을 눌렀을 때 호출되는 콜백</param>
    /// <param name="callbackOnClickButton2">버튼2를 눌렀을 때 호출되는 콜백</param>
    public virtual void Open(string text, string buttonText1, string buttonText2, UnityAction callbackOnClickButton1, UnityAction callbackOnClickButton2)
    {
        button2Text.text = buttonText2;
        this.OnClickButton2.RemoveAllListeners();
        this.OnClickButton2.AddListener(callbackOnClickButton2);
        base.Open(text, buttonText1, callbackOnClickButton1);
    }

    /// <summary>
    /// 버튼2가 눌렸을 때
    /// </summary>
    public virtual void OnClickButton2Sub()
    {
        OnClickButton2.Invoke();
        Close();
    }
}