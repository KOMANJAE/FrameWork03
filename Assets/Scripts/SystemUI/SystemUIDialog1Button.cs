using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// ���� ��ư ���̾�α�
/// </summary>
//[AddComponentMenu("Utage/Lib/System UI/SystemUiDialog1Button")]
public class SystemUIDialog1Button : MonoBehaviour
{
    /// <summary>
    /// ���� ǥ�ÿ� �ؽ�Ʈ
    /// </summary>
    [SerializeField]
    protected Text titleText;

    /// <summary>
    /// ��ư1�� �ؽ�Ʈ
    /// </summary>
    [SerializeField]
    protected Text button1Text;

    /// <summary>
    /// ��ư1�� ������ ���� �̺�Ʈ
    /// </summary>
    [SerializeField]
    protected UnityEvent OnClickButton1;

    /// <summary>
    /// ���̾�α� ����
    /// </summary>
    /// <param name="text">ǥ�� �ؽ�Ʈ</param>
    /// <param name="buttonText1">��ư1�� �ؽ�Ʈ</param>
    /// <param name="target">��ư�� ������ �� ȣ��Ǵ� �ݹ�</param>
    public virtual void Open(string text, string buttonText1, UnityAction callbackOnClickButton1)
    {
        titleText.text = text;
        button1Text.text = buttonText1;
        this.OnClickButton1.RemoveAllListeners();
        this.OnClickButton1.AddListener(callbackOnClickButton1);
        Open();
    }

    /// <summary>
    /// ��ư1�� ������ ���� ó��
    /// </summary>
    public virtual void OnClickButton1Sub()
    {
        OnClickButton1?.Invoke();
        Close();
    }

    /// <summary>
    /// ����
    /// </summary>
    public virtual void Open()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// Ŭ����
    /// </summary>
    public virtual void Close()
    {
        this.gameObject.SetActive(false);
    }
}
