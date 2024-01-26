using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SystemUI : MonoBehaviour
{
    [SerializeField]
    protected SystemUIDialog1Button dialog1Button = null;

    [SerializeField]
    protected SystemUIDialog2Button dialog2Button = null;

    [SerializeField]
    protected SystemUIDialog3Button dialog3Button = null;

    [SerializeField]
    protected SystemUIDialog2Button dialogGameExit = null;

    [SerializeField]
    protected IndicatorIcon indicator = null;

    public bool IsEnableInputEscape
    {
        get { return isEnableInputEscape; }
        set { isEnableInputEscape = value; }
    }

    [SerializeField]
    bool isEnableInputEscape = true;

    public void OpenDialog(string text, List<ButtonEventInfo> buttons)
    {
        switch (buttons.Count)
        {
            case 1:
                OpenDialog1Button(text, buttons[0]);
                break;
            case 2:
                OpenDialog2Button(text, buttons[0], buttons[1]);
                break;
            case 3:
                OpenDialog3Button(text, buttons[0], buttons[1], buttons[2]);
                break;
            default:
                Debug.LogError(" Dilog Button Count over = " + buttons.Count);
                break;
        }
    }

    public void OpenDialog1Button(string text, ButtonEventInfo button1)
    {
        OpenDialog1Button(text, button1.text, button1.callBackClicked);
    }
    public void OpenDialog2Button(string text, ButtonEventInfo button1, ButtonEventInfo button2)
    {
        OpenDialog2Button(text, button1.text, button2.text, button1.callBackClicked, button2.callBackClicked);
    }
    public void OpenDialog3Button(string text, ButtonEventInfo button1, ButtonEventInfo button2, ButtonEventInfo button3)
    {
        OpenDialog3Button(text, button1.text, button2.text, button3.text, button1.callBackClicked, button2.callBackClicked, button3.callBackClicked);
    }


    public void OpenDialog1Button(string text, string buttonText1, UnityAction callbackOnClickButton1)
    {
        dialog1Button.Open(text, buttonText1, callbackOnClickButton1);
    }

    public void OpenDialog2Button(string text, string buttonText1, string buttonText2, UnityAction callbackOnClickButton1, UnityAction callbackOnClickButton2)
    {
        dialog2Button.Open(text, buttonText1, buttonText2, callbackOnClickButton1, callbackOnClickButton2);
    }

    public void OpenDialog3Button(string text, string buttonText1, string buttonText2, string buttonText3, UnityAction callbackOnClickButton1, UnityAction callbackOnClickButton2, UnityAction callbackOnClickButton3)
    {
        dialog3Button.Open(text, buttonText1, buttonText2, buttonText3, callbackOnClickButton1, callbackOnClickButton2, callbackOnClickButton3);
    }

    public void OpenDialogYesNo(string text, UnityAction callbackOnClickYes, UnityAction callbackOnClickNo)
    {
        //OpenDialog2Button(text, LanguageSystemText.LocalizeText(SystemText.Yes), LanguageSystemText.LocalizeText(SystemText.No), callbackOnClickYes, callbackOnClickNo);
        OpenDialog2Button(text, "Yes", "No", callbackOnClickYes, callbackOnClickNo);
    }

    public void StartIndicator(Object obj)
    {
        if (indicator) indicator.StartIndicator(obj);
    }

    public void StopIndicator(Object obj)
    {
        if (indicator) indicator.StopIndicator(obj);
    }

    void Update()
    {
        //안드로이드 에서 뒤로가기 키로 앱 종료 확인
        if (IsEnableInputEscape)
        {
            //if (WrapperMoviePlayer.GetInstance() != null && WrapperMoviePlayer.IsPlaying()) return;

            if (InputUtil.GetKeyDown(KeyCode.Escape))
            {
                OnOpenDialogExitGame();
            }
        }
    }

    public virtual void OnOpenDialogExitGame()
	{
		//InputUtil.EnableInput = false;
		//dialogGameExit.Open(
		//	LanguageSystemText.LocalizeText(SystemText.QuitGame),
		//	LanguageSystemText.LocalizeText(SystemText.Yes),
		//	LanguageSystemText.LocalizeText(SystemText.No),
		//	OnDialogExitGameYes, OnDialogExitGameNo
		//	);
        dialogGameExit.Open(
            "Exit Game",
            "Yes",
            "NO",
            OnDialogExitGameYes, OnDialogExitGameNo
            );
    }

    protected virtual void OnDialogExitGameYes()
    {
        InputUtil.EnableInput = true;
        StartCoroutine(CoGameExit());
    }

    protected virtual void OnDialogExitGameNo()
    {
        InputUtil.EnableInput = true;
    }

    protected virtual IEnumerator CoGameExit()
    {
        Application.Quit();
        yield break;
    }

}
