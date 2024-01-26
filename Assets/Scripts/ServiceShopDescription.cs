using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ServiceShopDescription : MonoBehaviour
{
    [System.Serializable]
    class DescriptionUI
    {
        public RawImage itemImage;
        public TMP_Text itemNameText;
        public TMP_Text itemDescriptionText;
        public TMP_Text itemPriceText;
        public TMP_Text itemPriceText2;
        public Button buyBtn;
        public Button cancelBtn;
    }

    [SerializeField]
    DescriptionUI descriptionUI;
    UnityAction btnEvent;
    CatalogItem selectedCatalog;
    Texture selectedTexture;
    bool isGooglePay;

    public void Open(CatalogItem catalog, Texture itemTexture, UnityAction onBuyEvent)
    {
        selectedCatalog = catalog;
        btnEvent = onBuyEvent;
        selectedTexture = itemTexture;
        descriptionUI.itemImage.texture = selectedTexture;
        descriptionUI.itemNameText.text = catalog.DisplayName;
        descriptionUI.itemDescriptionText.text = catalog.Description;
        gameObject.SetActive(false);

        Product product = GPGSAndPFManager.m_StoreController.products.WithID(selectedCatalog.ItemId);
        if (product != null)
        {
            isGooglePay = true;
            descriptionUI.itemPriceText.text = string.Format("<color=#F3B10A>{0} ��</color>", product.metadata.localizedPriceString);
            descriptionUI.itemPriceText2.text = string.Format("<color=#F3B10A>{0} ��</color>���� �����Ͻðڽ��ϱ�?", product.metadata.localizedPriceString);
        }
        else
        {
            isGooglePay = false;
            descriptionUI.itemPriceText.text = string.Format("<color=#C3F5F8>{0} ũ����Ż</color>", selectedCatalog.VirtualCurrencyPrices["CD"]);
            descriptionUI.itemPriceText2.text = string.Format("<color=#C3F5F8>ũ����Ż {0} ��</color>�� �����Ͻðڽ��ϱ�?", selectedCatalog.VirtualCurrencyPrices["CD"]);
        }

        descriptionUI.buyBtn.interactable = true;
    }

    public void OnBuyEvent()
    {
        descriptionUI.buyBtn.interactable = false;
        if (isGooglePay)
        {
            Debug.Log("---OnBuyEvent :: ItemId : " + selectedCatalog.ItemId);
            Managers.PF.Purchase(selectedCatalog.ItemId);
            gameObject.SetActive(false);
        }
        else
        {
            Managers.PF.PlayfabPurchase(selectedCatalog
                , delegate
                { /*OnItemSelect(selectedShopItem);*/
                    if (selectedCatalog.Tags.Count > 2)
                    {
                        List<Texture> targetTextureList = new List<Texture>();
                        List<string> targetNameList = new List<string>();
                        List<string> targetAmountList = new List<string>();
                        targetTextureList.Add(selectedTexture);
                        //targetNameList.Add(engine.Param.GetParameterString(string.Format("Item[{0}].ItemName", selectedCatalog.Tags[2])));
                        targetAmountList.Add(selectedCatalog.Tags[1]);
                        GetItemUI.instance.Open("���� ������ ���� �Ϸ�", targetTextureList, targetNameList, targetAmountList, null);
                        if (Title.isStarted)
                        {
                            //int amount = engine.Param.GetParameterInt(string.Format("Item[{0}].Amount", selectedCatalog.Tags[2]));
                            //engine.Param.SetParameter(string.Format("Item[{0}].Amount", selectedCatalog.Tags[2]), amount + 1);
                        }
                        // targetTextureList
                        btnEvent?.Invoke();
                        descriptionUI.buyBtn.interactable = true;
                    }

                    //   GetItemUI.instance.Open("������ ȹ��",)
                    gameObject.SetActive(false);
                }
                , delegate
                {
                    //SystemUi.GetInstance().OpenDialog1Button("���� ����", "Ȯ��", delegate { });
                    descriptionUI.buyBtn.interactable = true;
                });


        }

    }
}
