using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 세 개의 버튼이 있는 다이얼로그
/// </summary>
//[AddComponentMenu("Utage/Lib/System UI/SystemUiDialog3Button")]
public class SystemUIDialog3Button : SystemUIDialog2Button
{
    [SerializeField]
    protected Text button3Text;

    /// <summary>
    /// 버튼3을 눌렀을 때의 이벤트
    /// </summary>
    [SerializeField]
    protected UnityEvent OnClickButton3;

    /// <summary>
    /// 3개 버튼 다이얼로그 열기
    /// </summary>
    /// <param name="text">표시 텍스트</param>
    /// <param name="buttonText1">버튼1의 텍스트</param>
    /// <param name="buttonText2">버튼2의 텍스트</param>
    /// <param name="buttonText3">버튼3의 텍스트</param>
    /// <param name="callbackOnClickButton1">버튼1을 눌렀을 때 호출되는 콜백</param>
    /// <param name="callbackOnClickButton2">버튼2를 눌렀을 때 호출되는 콜백</param>
    /// <param name="callbackOnClickButton3">버튼3을 눌렀을 때 호출되는 콜백</param>
    public virtual void Open(string text, string buttonText1, string buttonText2, string buttonText3, UnityAction callbackOnClickButton1, UnityAction callbackOnClickButton2, UnityAction callbackOnClickButton3)
    {
        button3Text.text = buttonText3;
        this.OnClickButton3.RemoveAllListeners();
        this.OnClickButton3.AddListener(callbackOnClickButton3);
        base.Open(text, buttonText1, buttonText2, callbackOnClickButton1, callbackOnClickButton2);
    }

    /// <summary>
    /// 버튼3가 눌렸을 때
    /// </summary>
    public virtual void OnClickButton3Sub()
    {
        this.OnClickButton3.Invoke();
        Close();
    }
}