using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ServiceShopUI : MonoBehaviour
{
    [System.Serializable]
    class ShopUI
    {
        public Toggle crystalShopToggle;
        public Toggle realMoneyShopToggle;
        public GameObject shopItemElement;
        public ScrollRect shopItemContent;
    }

    [SerializeField]
    ShopUI shopUI;

    public enum ShopPuchaseType
    {
        Money,
        Crystal
    }

    class ItemElement
    {
        public GameObject obj;
        public RawImage itemImage;
        public TMP_Text itemNameText;
        public TMP_Text itemPriceText;
        public Button btn;
        public CatalogItem itemCatalog;
        public ShopPuchaseType puchaseType;
        public GameObject soldOutObj;
    }

    List<ItemElement> itemElementList = new List<ItemElement>();
    public List<CatalogItem> catalog;

    public void Start()
    {
        catalog = Managers.PF.Catalog;
        for (int i = 0; i < catalog.Count; i++)
        {
            ItemElement newElement = new ItemElement();

            newElement.obj = GameObject.Instantiate(shopUI.shopItemElement, shopUI.shopItemContent.content); //original, parent
            newElement.itemImage = newElement.obj.transform.Find("ItemImage").GetComponent<RawImage>(); //������ �̹��� ǥ�� ������Ʈ
            newElement.itemNameText = newElement.obj.transform.Find("ItemNameText").GetComponent<TMP_Text>(); //������ �̸� ǥ�� �ؽ�Ʈ
            newElement.itemPriceText = newElement.obj.transform.Find("ItemPriceText").GetComponent<TMP_Text>(); //������ ���� ǥ�� �ؽ�Ʈ
            newElement.itemCatalog = catalog[i]; //īŻ�α� ����
            newElement.itemNameText.text = newElement.itemCatalog.DisplayName; //�÷����ջ� �̸�
            newElement.soldOutObj = newElement.obj.transform.Find("SoldOut").gameObject; //���� ǥ�� ������Ʈ

            Product product = GPGSAndPFManager.m_StoreController.products.WithID(newElement.itemCatalog.ItemId); //ID�� product �޾ƿ���

            if (product != null)
            {
                newElement.itemPriceText.text = string.Format("<color=#F3B10A>{0} ��</color>", product.metadata.localizedPriceString);
                newElement.puchaseType = ShopPuchaseType.Money;
            }
            else
            {
                newElement.itemPriceText.text = string.Format("<color=#C3F5F8>{0} ũ����Ż</color>", newElement.itemCatalog.VirtualCurrencyPrices["CD"]);
                newElement.puchaseType = ShopPuchaseType.Crystal;
            }

            newElement.itemImage.texture = Resources.Load<Texture2D>(string.Format("trpgProject/Texture/{0}", newElement.itemCatalog.Tags[0]));
            newElement.btn = newElement.obj.GetComponent<Button>();
            newElement.btn.onClick.AddListener(delegate { OnItemSelect(newElement); });
            itemElementList.Add(newElement);
        }
        OnToggleSetUp();

    }

    private void OnItemSelect(ItemElement newElement)
    {
        GetItemUI.instance.OpenCrystalShopDescription(newElement.itemCatalog, newElement.itemImage.texture, delegate { OnToggleSetUp(); });
    }

    public void OnToggleSetUp()
    {
        /*
                ShopPuchaseType targetPuchaseType = ShopPuchaseType.Money;
                if (shopUI.realMoneyShopToggle.isOn)
                {
                    targetPuchaseType = ShopPuchaseType.Money;
                }
                else
                {
                    targetPuchaseType = ShopPuchaseType.Crystal;
                }

                for (int i = 0; i < itemElementList.Count; i++)
                {
                    itemElementList[i].obj.SetActive(targetPuchaseType == itemElementList[i].puchaseType);

                    if (itemElementList[i].obj.activeInHierarchy && itemElementList[i].puchaseType == ShopPuchaseType.Crystal)
                    {
                        var targetSpec = GPGSAndPFManager.instance.itemSetter.Find(x => x.itemId == itemElementList[i].itemCatalog.ItemId);
                        if (targetSpec != null && targetSpec.maxBuyCount > -1 && engine.Param.GetParameterInt(string.Format("Item[{0}].SavedAmount", targetSpec.utageId)) > targetSpec.maxBuyCount)
                        {
                            itemElementList[i].btn.interactable = false;
                            itemElementList[i].soldOutObj.SetActive(true);
                        }
                        else if (targetSpec == null && GoogleAndPlayFabManager.instance.CheckInventory(itemElementList[i].itemCatalog.ItemId))
                        {
                            itemElementList[i].btn.interactable = false;
                            itemElementList[i].soldOutObj.SetActive(true);
                        }
                        else
                        {
                            itemElementList[i].soldOutObj.SetActive(false);
                        }


                    }
                    else
                    {
                        if (itemElementList[i].itemCatalog.DisplayName.Contains("���� ����")
                            && AdManager.instance.IsCheckSkipAd())
                        {
                            itemElementList[i].soldOutObj.SetActive(true);
                        }
                        else
                        {
                            itemElementList[i].soldOutObj.SetActive(false);
                        }

                    }
                }
        */
    }
}
