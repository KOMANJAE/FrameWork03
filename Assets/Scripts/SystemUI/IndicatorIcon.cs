using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorIcon : MonoBehaviour
{
    [SerializeField]
    GameObject icon = null;             //ȸ���ϴ� ������

    [SerializeField]
    float animTime = 1.0f / 12;     //�ִϸ��̼� �ð�

    [SerializeField]
    float animRotZ = -36;           //1�ʴ� �ִϸ��̼� ����

    [SerializeField]
    bool isDeviceIndicator = false; //����� �ε������͸� ����� ������

    bool isStarting = false;
    float rotZ = 0;
    List<System.Object> objList = new List<object>();

    void Awake()
    {
        if (IsDeviceIndicator())
        {
            WrapperUnityVersion.SetActivityIndicatorStyle();
            icon.SetActive(false);
        }
    }

    /// <summary>
    /// �ε������� ǥ�� ����
    /// ǥ�ø� ��û�ϴ� ��ü�� ���� ������ ������ �� ����
    /// ��� ��û�� �Ϸ�� ������ ǥ�ø� �����
    /// </summary>
    /// <param name="obj">ǥ�ø� ��û�ϴ� ��ü</param>
    public void StartIndicator(System.Object obj)
    {
        IncRef(obj);
        if (objList.Count <= 0) return;
        if (isStarting) return;

        this.gameObject.SetActive(true);
        isStarting = true;
        if (IsDeviceIndicator())
        {
#if UNITY_IPHONE || UNITY_ANDROID && !UNITY_EDITOR
			Handheld.StartActivityIndicator();
#endif
        }
        else
        {
            InvokeRepeating("RotIcon", 0, animTime); //RotIcon �̺�Ʈ�� 0�� �� animTime ���� ����
        }
    }
    /// <summary>
    /// �ε������� ǥ�� ����
    /// ǥ�ø� ��û�ϴ� ��ü�� ���� ������ ���� ����
    /// ��� ��û�� �Ϸ�� ������ ǥ�ø� �����
    /// </summary>
    /// <param name="obj">ǥ�ø� ��û�� ��ü</param>
    public void StopIndicator(System.Object obj)
    {
        DecRef(obj);
        if (objList.Count > 0) return;
        if (!isStarting) return;
        if (IsDeviceIndicator())
        {
#if UNITY_IPHONE || UNITY_ANDROID && !UNITY_EDITOR
	        Handheld.StopActivityIndicator();
#endif
        }
        else
        {
            CancelInvoke();
        }
        this.gameObject.SetActive(false);
        isStarting = false;
    }

    void RotIcon()
    {
        icon.transform.eulerAngles = new Vector3(0, 0, rotZ);
        rotZ += animRotZ;
    }

    void IncRef(System.Object obj)
    {
        if (!objList.Contains(obj))
        {
            objList.Add(obj);
        }
    }
    void DecRef(System.Object obj)
    {
        if (objList.Contains(obj))
        {
            objList.Remove(obj);
        }
    }

    bool IsDeviceIndicator()
    {
#if UNITY_IPHONE || UNITY_ANDROID && !UNITY_EDITOR
			return isDeviceIndicator;
#else
        isDeviceIndicator = false;  //return false�� ���� �ʴ� ���� ��� ��å�Դϴ�.
        return isDeviceIndicator;
#endif
    }
}

