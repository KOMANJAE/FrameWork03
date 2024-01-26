using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField]
    bool isDebug;

    [SerializeField]
    TMP_Text loadingText;

    [SerializeField]
    GameObject titleBtnObj;
    [SerializeField]
    GameObject mainGame;

    public static bool isStarted;

    private void OnEnable()
    {
        isStarted = false;

        if (isDebug)
        {
            //originalTitle.SetActive(true);
            //gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(CoStartLoading());
        }
    }

    IEnumerator CoStartLoading()
    {
        loadingText.gameObject.SetActive(true);
        titleBtnObj.SetActive(false);

        yield return new WaitForSeconds(0.2f);

        while (!Managers.PF.IsInitialized)
        {
            yield return new WaitForFixedUpdate();

            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.Sin(Time.time * 2.0f));

            if (Managers.PF.isInitializeFailed)
            {
                //초기화 및 로그인에 실패한 경우, 에러 메시지 다이얼로그 표기 및 나가기 동작
                //SystemUi.GetInstance().OpenDialog1Button(
                //        string.Format("구글 플레이 로그인 중 오류가 발생했습니다. \n 관리자에게 문의하세요 \n 오류코드 : {0}", Managers.PF.Error)
                //        , "나가기"
                //        , delegate { Application.Quit(); });
                break;
            }
        }

        //ClearSaveBtn.interactable = engine.SaveManager.ReadAutoSaveData();
        //if (PlayerPrefs.GetInt("AlertAgree", 0) == 0)
        //{
        //    customAlert.gameObject.SetActive(true);
        //}

        loadingText.gameObject.SetActive(false);
        titleBtnObj.SetActive(true);
    }


    IEnumerator CoSetUp()
    {
        yield return null;
        //mainGame.SetActive(true);
        isStarted = true;
    }

    //시작 버튼
    public void OnBtnStart()
    {
        StartCoroutine(CoSetUp());
    }
}
