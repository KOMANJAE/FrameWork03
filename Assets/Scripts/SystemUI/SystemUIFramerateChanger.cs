using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// FPSǥ��
/// </summary>
//[AddComponentMenu("Utage/Lib/System UI/SystemUiFramerateChanger")]
public class SystemUiFramerateChanger : MonoBehaviour
{
    [SerializeField]
    Text text = null;

    void Update()
    {
        if (text != null)
            text.text = string.Format("FPS:{0}", Application.targetFrameRate);
    }


    List<int> frameRateList = new List<int>() { 30, 60, 120 };
    List<int> vSyncCountList = new List<int>() { 2, 1, 0 };

    int currentIndex = 0;

    public int TargetFrameRate()
    {
        return Application.targetFrameRate;
    }

    //FPS﷪���
    public void OnClickChangeFrameRate()
    {
        currentIndex = (currentIndex + 1) % frameRateList.Count;
        Application.targetFrameRate = frameRateList[currentIndex];
        QualitySettings.vSyncCount = vSyncCountList[currentIndex];
    }
}