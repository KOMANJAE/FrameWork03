using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEventInfo
{
    public ButtonEventInfo(string text, UnityAction callBackClicked)
    {
        this.text = text;
        this.callBackClicked = callBackClicked;
    }
    public string text;
    public UnityAction callBackClicked;
};

[System.Serializable]
public class OpenDialogEvent : UnityEvent<string, List<ButtonEventInfo>> { }

[System.Serializable]
public class Open1ButtonDialogEvent : UnityEvent<string, ButtonEventInfo> { }

[System.Serializable]
public class Open2ButtonDialogEvent : UnityEvent<string, ButtonEventInfo, ButtonEventInfo> { }

[System.Serializable]
public class Open3ButtonDialogEvent : UnityEvent<string, ButtonEventInfo, ButtonEventInfo, ButtonEventInfo> { }

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

[System.Serializable]
public class IntEvent : UnityEvent<int> { }

[System.Serializable]
public class StringEvent : UnityEvent<string> { }

[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }
