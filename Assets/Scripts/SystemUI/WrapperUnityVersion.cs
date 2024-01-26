using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrapperUnityVersion
{
    public static void SetActivityIndicatorStyle()
    {
#if UNITY_IPHONE
#if UNITY_4_6_OR_EARLIER
			Handheld.SetActivityIndicatorStyle(iOSActivityIndicatorStyle.Gray);
#else
			Handheld.SetActivityIndicatorStyle(UnityEngine.iOS.ActivityIndicatorStyle.Gray);
#endif
#elif UNITY_ANDROID
        Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Small);
#endif
    }
}
