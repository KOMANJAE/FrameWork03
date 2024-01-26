using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 편의 클래스
/// </summary>
public class ToolKit
{
    public static bool IsPlatformStandAloneOrEditor()
    {
        return Application.isEditor || IsPlatformStandAlone();
    }

    public static bool IsPlatformStandAlone()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.OSXPlayer:
            case RuntimePlatform.LinuxPlayer:
                return true;
            default:
                return false;
        }
    }
}
