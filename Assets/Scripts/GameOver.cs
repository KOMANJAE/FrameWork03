using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    string adId;  //��Ȱ ����ID

    int resurrectCount;
    int savedCrystal;
    bool isResurrect;

    [SerializeField]
    Button adResurrectBtn;  //��Ȱ ���� ��ư
    [SerializeField]
    Button scrollResurrectBtn; //��ũ�� ���� ��ư

    [SerializeField]
    TMP_Text remainResurrectCount;

    [SerializeField]
    int MaxResurrectionCountPerDay = 5;

    public void OnEnable()
    {
        if(!isResurrect)
        {
            //resurrectCount = engine.Param.GetParameterInt("ResurrectCount");

            //AdManager.instance.onPayed += PayedResurrect;
            savedCrystal = Managers.PF.GetCrystal();
            remainResurrectCount.text = string.Format("���� ��Ȱ Ƚ�� : <color=#B44631>{0}ȸ</color>", MaxResurrectionCountPerDay - resurrectCount);

            //��Ȱ�� 5ȸ �̻� ���� �� ���
            if (resurrectCount >= MaxResurrectionCountPerDay)
            {
                scrollResurrectBtn.interactable = false;
                adResurrectBtn.interactable = false;
            }
            //engine.UiManager.IsInputTrigCustom = false;
            isResurrect = false;
        }
    }

    public void OnDisable()
    {
        //AdManager.instance.onPayed -= PayedResurrect;
    }

    private void PayedResurrect(string rewardedAD)
    {
        Debug.Log($"AD::{rewardedAD}");

        if(rewardedAD.Equals("Reward") || rewardedAD.Equals("Resurrect"))
        {
            Resurrect();
        }
    }

    public void OnBtnWatchAd()
    {
        //if (!AdManager.instance.IsCheckSkipAd())
        //{
        //    AdManager.instance.ShowAds(adId);
        //}
        //else
        //{
        //    PayedResurrect("Resurrect");
        //}
    }

    //GetBtnUserResurrectionScroll
    public void GetBtnUseCrystal()
    {
        int resurrectionScroll01Amount = 0;
        int resurrectionScroll02Amount = 0;
        //
        //int resurrectionScroll01Amount = GetResurrectionScroll01Amount();
        //int resurrectionScroll02Amount = GetResurrectionScroll02Amount();
        Debug.Log("resurrectAmount1::" + resurrectionScroll01Amount);
        Debug.Log("resurrectAmount2::" + resurrectionScroll02Amount);

        string resurrectString = "";
        int resurrectCommand = 0;

        if (resurrectionScroll01Amount > 0)
        {
            resurrectString = string.Format("<color=#B44631>�ֹ߼� ��Ȱ ��ũ��</color>�� ����Ͽ� ��Ȱ�Ͻðڽ��ϱ�?");
            resurrectCommand = 1;
        }
        else if (resurrectionScroll02Amount > 0)
        {
            resurrectString = string.Format("<color=#B44631>������ ��Ȱ ��ũ��</color>�� ����Ͽ� ��Ȱ�Ͻðڽ��ϱ�?");
            resurrectCommand = 2;
        }
        else
        {

        }

        //��ũ�� ���� ��ũ�� ��� �ݿ�
        if (!string.IsNullOrEmpty(resurrectString))
        {
            //SystemUi.GetInstance().OpenDialog2Button(resurrectString, "��", "�ƴϿ�"
            //  , delegate
            //  {
            //      if (resurrectCommand == 1)
            //      {
            //          engine.Param.SetParameter("Item[Revive_scroll].Amount", resurrectionScroll01Amount - 1);
            //      }
            //      else if (resurrectCommand == 2)
            //      {
            //          engine.Param.SetParameter("Item[Revive_scroll_2].Amount", resurrectionScroll02Amount - 1);
            //          engine.Param.SetParameter("Item[Revive_scroll_2].UsedAmount", engine.Param.GetParameterInt("Item[Revive_scroll_2].UsedAmount") + 1);
            //      }
            //      Resurrect();
            //  }
            //  , delegate
            //  {

            //  });
        }
        else
        {
            //SystemUi.GetInstance().OpenDialog1Button("��Ȱ ��ũ���� �����մϴ�.", "Ȯ��", delegate { });
        }

    }

    public void Resurrect()
    {
        //engine.Param.SetParameterBoolean("IsResurrect", true);
        //engine.Param.SetParameterInt("ResurrectCount", resurrectCount + 1);
        //engine.Param.SetParameterInt("Player[CurrentHp].StatusInt", engine.Param.GetParameterInt("Player[HP].StatusInt"));
        //engine.Param.SetParameterInt("Player[CurrentMp].StatusInt", engine.Param.GetParameterInt("Player[MP].StatusInt"));

        //Debug.Log("resurrected");

        //Debug.Log("stauts::" + engine.UiManager.Status);
        //Debug.Log("stat2::" + engine.Page.IsWaitBrPage);

        isResurrect = true;
    }

    public void GameEnd()
    {
        //engine.UiManager.IsInputTrigCustom = true;
    }

    public void FixedUpdate()
    {
        //if (isResurrect && !engine.UiManager.IsInputTrigCustom)
        //{
        //    engine.UiManager.IsInputTrigCustom = true;
        //}
    }

}
