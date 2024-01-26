using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// �� ���� ��ư�� �ִ� ���̾�α�
/// </summary>
//[AddComponentMenu("Utage/Lib/System UI/SystemUiDialog3Button")]
public class SystemUIDialog3Button : SystemUIDialog2Button
{
    [SerializeField]
    protected Text button3Text;

    /// <summary>
    /// ��ư3�� ������ ���� �̺�Ʈ
    /// </summary>
    [SerializeField]
    protected UnityEvent OnClickButton3;

    /// <summary>
    /// 3�� ��ư ���̾�α� ����
    /// </summary>
    /// <param name="text">ǥ�� �ؽ�Ʈ</param>
    /// <param name="buttonText1">��ư1�� �ؽ�Ʈ</param>
    /// <param name="buttonText2">��ư2�� �ؽ�Ʈ</param>
    /// <param name="buttonText3">��ư3�� �ؽ�Ʈ</param>
    /// <param name="callbackOnClickButton1">��ư1�� ������ �� ȣ��Ǵ� �ݹ�</param>
    /// <param name="callbackOnClickButton2">��ư2�� ������ �� ȣ��Ǵ� �ݹ�</param>
    /// <param name="callbackOnClickButton3">��ư3�� ������ �� ȣ��Ǵ� �ݹ�</param>
    public virtual void Open(string text, string buttonText1, string buttonText2, string buttonText3, UnityAction callbackOnClickButton1, UnityAction callbackOnClickButton2, UnityAction callbackOnClickButton3)
    {
        button3Text.text = buttonText3;
        this.OnClickButton3.RemoveAllListeners();
        this.OnClickButton3.AddListener(callbackOnClickButton3);
        base.Open(text, buttonText1, buttonText2, callbackOnClickButton1, callbackOnClickButton2);
    }

    /// <summary>
    /// ��ư3�� ������ ��
    /// </summary>
    public virtual void OnClickButton3Sub()
    {
        this.OnClickButton3.Invoke();
        Close();
    }
}