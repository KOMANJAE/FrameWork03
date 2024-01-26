using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// �� ���� ��ư�� �ִ� ���̾�α�
/// </summary>
//[AddComponentMenu("Utage/Lib/System UI/SystemUiDialog2Button")]
public class SystemUIDialog2Button : SystemUIDialog1Button
{

    /// <summary>
    /// ��ư2�� �ؽ�Ʈ ����
    /// </summary>
    [SerializeField]
    protected Text button2Text;

    /// <summary>
    /// ��ư2�� ������ ���� �̺�Ʈ
    /// </summary>
    [SerializeField]
    protected UnityEvent OnClickButton2;

    /// <summary>
    /// �� ��ư ���̾�α׸� ����
    /// </summary>
    /// <param name="text">ǥ�� �ؽ�Ʈ</param>
    /// <param name="buttonText1">��ư1�� �ؽ�Ʈ</param>
    /// <param name="buttonText2">��ư2�� �ؽ�Ʈ</param>
    /// <param name="callbackOnClickButton1">��ư1�� ������ �� ȣ��Ǵ� �ݹ�</param>
    /// <param name="callbackOnClickButton2">��ư2�� ������ �� ȣ��Ǵ� �ݹ�</param>
    public virtual void Open(string text, string buttonText1, string buttonText2, UnityAction callbackOnClickButton1, UnityAction callbackOnClickButton2)
    {
        button2Text.text = buttonText2;
        this.OnClickButton2.RemoveAllListeners();
        this.OnClickButton2.AddListener(callbackOnClickButton2);
        base.Open(text, buttonText1, callbackOnClickButton1);
    }

    /// <summary>
    /// ��ư2�� ������ ��
    /// </summary>
    public virtual void OnClickButton2Sub()
    {
        OnClickButton2.Invoke();
        Close();
    }
}