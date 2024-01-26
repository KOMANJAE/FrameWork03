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
                //�ʱ�ȭ �� �α��ο� ������ ���, ���� �޽��� ���̾�α� ǥ�� �� ������ ����
                //SystemUi.GetInstance().OpenDialog1Button(
                //        string.Format("���� �÷��� �α��� �� ������ �߻��߽��ϴ�. \n �����ڿ��� �����ϼ��� \n �����ڵ� : {0}", Managers.PF.Error)
                //        , "������"
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

    //���� ��ư
    public void OnBtnStart()
    {
        StartCoroutine(CoSetUp());
    }
}
