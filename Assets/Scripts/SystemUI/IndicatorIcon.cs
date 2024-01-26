using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorIcon : MonoBehaviour
{
    [SerializeField]
    GameObject icon = null;             //회전하는 아이콘

    [SerializeField]
    float animTime = 1.0f / 12;     //애니메이션 시간

    [SerializeField]
    float animRotZ = -36;           //1초당 애니메이션 각도

    [SerializeField]
    bool isDeviceIndicator = false; //기기의 인디케이터를 사용할 것인지

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
    /// 인디케이터 표시 시작
    /// 표시를 요청하는 객체는 여러 곳에서 설정할 수 있음
    /// 모든 요청이 완료될 때까지 표시를 계속함
    /// </summary>
    /// <param name="obj">표시를 요청하는 객체</param>
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
            InvokeRepeating("RotIcon", 0, animTime); //RotIcon 이벤트를 0초 후 animTime 마다 실행
        }
    }
    /// <summary>
    /// 인디케이터 표시 종료
    /// 표시를 요청하는 객체는 여러 곳에서 설정 가능
    /// 모든 요청이 완료될 때까지 표시를 계속함
    /// </summary>
    /// <param name="obj">표시를 요청한 객체</param>
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
        isDeviceIndicator = false;  //return false를 하지 않는 것은 경고 대책입니다.
        return isDeviceIndicator;
#endif
    }
}

