using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.OurUtils;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class GPGSAndPFManager : MonoBehaviour, IDetailedStoreListener
{
    public List<CatalogItem> Catalog;
    public List<ItemInstance> playerInventory;

    //��ȭ ���� View ������Ʈ ������ �뵵 //Managers.PF.crystalChange += OnMethod();
    public delegate void OnCrystalChange(int currentCrystal);
    public OnCrystalChange crystalChange;

    //���� ���� View ������Ʈ ������ �뵵 //Managers.PF.onNews += OnMethod();
    public delegate void OnNews(GetTitleNewsResult result);
    public OnNews onNews;

    public bool IsInitialized
    {
        get
        {
            return m_StoreController != null && Catalog != null;
        }
    }

    public bool isIgnoreGooglePlay;
    public bool isInitializeFailed;

    public static IStoreController m_StoreController;
    public string Token;
    public string Error;

    int crystal;
    public int GetCrystal() { return crystal; }

    [System.Serializable]
    public class ItemSetter
    {
        public string itemId;
        public int maxBuyCount;
    }

    public List<ItemSetter> itemSetter;

    public IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);

        if (isIgnoreGooglePlay)
        {
            LoginTest();
        }
        else
        {
#if UNITY_EDITOR
            LoginTest();
#else
            LoginGooglePlayGames();
#endif
        }
    }

    /* �׽�Ʈ �α��� */
    public void LoginTest()
    {
        //errorText.text = "login...";
        Debug.Log("Login...");

        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Email = "testAccount@localId.com",
            Password = "testAccount"
        },
        result =>
        {
            //errorText.text = ("Logged in");
            Debug.Log("Logged in");
            //RefreshIAPItems();
        },
        error =>
        {
            Debug.LogError(error.GenerateErrorReport());
            Register("testAccount", "testName");
        }
        );
    }

    /* GPGS �α��� */
    public void LoginGooglePlayGames()
    {
        try
        {
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.Instance.Authenticate((result) =>
            {
                if (result == SignInStatus.Success)
                {
                    //errorText.text = ("Login with Google Play games successful.");
                    Debug.Log("Login with Google Play games successful.");

                    PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                    {
                        //errorText.text = ("Authorization code : " + code);
                        Debug.Log("Authorization code : " + code);
                        Token = code;
                        //Login();
                        //errorText.text = code;
                        Debug.Log(code);
                    });
                }
                else
                {
                    Error = "Failed to retrieve Google play games authorization code";

                    //errorText.text = Error + "::" + result.ToString();
                    Error = result.ToString();
                    isInitializeFailed = true;
                }
            });
        }
        catch (Exception e)
        {
            //errorText.text = e.Message;/
            Error = e.Message;
            isInitializeFailed = true;
        }
    }

    private void Register(string id, string userName)
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            Username = userName,
            Email = id + "@localId.com",
            Password = id
        },
        result =>
        {
            Login();
        },
        error =>
        {
            //errorText.text = error.GenerateErrorReport() + "::" + Social.localUser.userName;
            Debug.Log($"{error.GenerateErrorReport() + "::" + Social.localUser.userName}");
            Error = error.GenerateErrorReport();
            isInitializeFailed = true;
        }
        );
    }

    public void Login()
    {
        // Login with Android ID
        //errorText.text = "login...";
        Debug.Log($"login...");

        PlayFabClientAPI.LoginWithGooglePlayGamesServices(new LoginWithGooglePlayGamesServicesRequest()
        {
            TitleId = PlayFabSettings.TitleId,
            ServerAuthCode = Token,
            CreateAccount = true,
            //EncryptedRequest = "D77TZWWYETQHRBJ5UQE3SPO7KJR3HP85PDJSEGE4ARIUF7XWCU"
            // AndroidDeviceId = SystemInfo.deviceUniqueIdentifier

        }, result => {
            //errorText.text = ("Logged in");
            Debug.Log($"Logged in");
            // Refresh available items
            RefreshIAPItems();
        }, error =>
        {
            //errorText.text = error.GenerateErrorReport();
            Debug.Log($"{error.GenerateErrorReport()}");
            Debug.Log("error.GenerateErrorReport() : " + error.GenerateErrorReport());
        }
        );
    }

    private void RefreshIAPItems()
    {
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), result =>
        {
            Catalog = result.Catalog;
            // Make UnityIAP initialize
            InitializePurchasing();
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

    public void InitializePurchasing()
    {
        // If IAP is already initialized, return gently
        if (IsInitialized) return;

        // Create a builder for IAP service
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.GooglePlay));

        // Register each item from the catalog
        foreach (var item in Catalog)
        {
            if (item.VirtualCurrencyPrices.Count == 0)
                builder.AddProduct(item.ItemId, ProductType.Consumable);
        }

        // Trigger IAP service initialization
        UnityPurchasing.Initialize(this, builder);
    }

    public void AddOrSubCrystal(int value)
    {
        if (value > 0)
        {
            PlayFabClientAPI.AddUserVirtualCurrency(new AddUserVirtualCurrencyRequest()
            {
                VirtualCurrency = "CD",
                Amount = value
            },
            result =>
            {
                RefreshInventory();
            }, error => { });
        }
        else
        {
            PlayFabClientAPI.SubtractUserVirtualCurrency(new SubtractUserVirtualCurrencyRequest()
            {
                VirtualCurrency = "CD",
                Amount = Mathf.Abs(value)
            },
            result =>
            {
                RefreshInventory();
            }, error => { });
        }
    }

    public bool UseCrystal(int value)
    {
        if (crystal >= value)
        {
            AddOrSubCrystal(-value);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        //errorText.text = "initialLize";
        Debug.Log($"initialLize");
        RefreshInventory();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError(error.ToString());
        //errorText.text = failureReason.ToString();
        isInitializeFailed = true;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError(error.ToString());
        //errorText.text = error.ToString();
        Error = error.ToString() + "::" + message;
        isInitializeFailed = true;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log($"OnPurchaseFailed :: failureDescription :: reason : {failureDescription.reason.ToString()}, productId : {failureDescription.productId}, message : {failureDescription.message}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"OnPurchaseFailed :: failureReason :  {failureReason.ToString()}");
    }

    public void Purchase(string productId)
    {
        try
        {
            Product product = m_StoreController.products.WithID(productId); //��ǰ ����

            if (product != null && product.availableToPurchase) //��ǰ�� �����ϸ鼭 ���� �����ϸ�
            {
                m_StoreController.InitiatePurchase(product); //���Ű� �����ϸ� ����
            }
            else //��ǰ�� �������� �ʰų� ���� �Ұ����ϸ�
            {
                //errorText.text = ("��ǰ�� ���ų� ���� ���Ű� �Ұ����մϴ�");
                Debug.Log("��ǰ�� ���ų� ���� ���Ű� �Ұ����մϴ�");
            }
        }
        catch (Exception e)
        {
            //errorText.text = e.Message;
            Debug.Log($"{e.Message}");
        }
    }

    public void PlayfabPurchase(CatalogItem item, UnityAction buyCallBack, UnityAction errorCallback)
    {
        var request = new PurchaseItemRequest()
        {
            CatalogVersion = item.CatalogVersion,
            StoreId = "CrystalStore",
            ItemId = item.ItemId,
            VirtualCurrency = "CD",
            Price = Convert.ToInt32(item.VirtualCurrencyPrices["CD"]),
        };

        PlayFabClientAPI.PurchaseItem(request
        , (result) =>
        {
            //  Debug.Log("������ ���� �Ϸ�");
            RefreshInventory();
            buyCallBack?.Invoke();
        },
        (error) =>
        {
            // Debug.Log("������ ���� ����");
            Debug.LogError("���Ž���::" + error.ErrorMessage);
            errorCallback?.Invoke();
        });
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        try
        {
            if (!IsInitialized)
            {
                return PurchaseProcessingResult.Complete;
            }

            // Test edge case where product is unknown
            if (purchaseEvent.purchasedProduct == null)
            {
                //errorText.text = "Attempted to process purchase with unknown product. Ignoring";
                Debug.Log("Attempted to process purchase with unknown product. Ignoring");
                return PurchaseProcessingResult.Complete;
            }

            // Test edge case where purchase has no receipt
            if (string.IsNullOrEmpty(purchaseEvent.purchasedProduct.receipt))
            {
                //errorText.text = ("Attempted to process purchase with no receipt: ignoring");
                Debug.Log("Attempted to process purchase with no receipt: ignoring");
                return PurchaseProcessingResult.Complete;
            }

            //errorText.text = ("Processing transaction: " + purchaseEvent.purchasedProduct.transactionID);
            Debug.Log($"{"Processing transaction: " + purchaseEvent.purchasedProduct.transactionID}");
            // Deserialize receipt
            var googleReceipt = GooglePurchase.FromJson(purchaseEvent.purchasedProduct.receipt);

            // Invoke receipt validation
            // This will not only validate a receipt, but will also grant player corresponding items
            // only if receipt is valid.

            PlayFabClientAPI.ValidateGooglePlayPurchase(new ValidateGooglePlayPurchaseRequest()
            {
                // Pass in currency code in ISO format
                CurrencyCode = purchaseEvent.purchasedProduct.metadata.isoCurrencyCode,
                // Convert and set Purchase price
                PurchasePrice = (uint)(purchaseEvent.purchasedProduct.metadata.localizedPrice * 100),
                // Pass in the receipt
                ReceiptJson = googleReceipt.PayloadData.json,
                // Pass in the signature
                Signature = googleReceipt.PayloadData.signature
            },
            result =>
            {
                //List<CatalogItem>�� ItemId �� purchaseEvent�� ��� productId�� ��ġ �ϴ� CatalogItem�� ��ȯ
                var targetCatalog = Catalog.Find(x => x.ItemId == purchaseEvent.purchasedProduct.definition.id);

                if (targetCatalog != null)
                {
                    AddOrSubCrystal(Int32.Parse(targetCatalog.Tags[1]));
                    //ServiceShopItems/crystal10, 110
                    //Tags[0] = 'ServiceShopItems/crystal10', Tags[1] = '110'
                }
            },
            error => Debug.Log($"{"Validation failed: " + error.GenerateErrorReport()}") //errorText.text = ("Validation failed: " + error.GenerateErrorReport())
            );

            return PurchaseProcessingResult.Complete;
        }
        catch (Exception e)
        {
            //errorText.text = e.ToString();
            Debug.LogError(e.ToString());
            return PurchaseProcessingResult.Pending;
        }
    }

    public void RefreshInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(),
        result =>
        {
            crystal = result.VirtualCurrency["CD"];
            playerInventory = result.Inventory;

            //errorText.text = string.Format("Amount:{0}", crystal);
            Debug.Log($"{string.Format("Amount:{0}", crystal)}");

            crystalChange?.Invoke(crystal);
            RecoveryItem();
        },
        error =>
        {

        }
        );
    }

    /* �÷������� �κ��丮�� ����� ������ �ҷ��� �����ͷ� ���ӳ� �κ��丮�� �ݿ� */
    public void RecoveryItem()
    {
        for (int i = 0; i < itemSetter.Count; i++)
        {
            var targetInventory = playerInventory.Find(x => x.ItemId == itemSetter[i].itemId);
            if (targetInventory != null)
            {
                //engine.Param.SetParameter(string.Format("Item[{0}].SavedAmount", itemSetter[i].utageId), targetInventory.RemainingUses);
            }
            else
            {
                //engine.Param.SetParameter(string.Format("Item[{0}].SavedAmount", itemSetter[i].utageId), 0);
            }
        }
    }

    /* �÷��̾� �κ��丮�� �ش� �������� ���� ���� */
    public bool CheckInventory(string label) 
    {
        for(int i = 0; i < playerInventory.Count; i++)
        {
            if (playerInventory[i].ItemId == label)
            {
                return true;
            }
        }
        return false;
    }

    /* ������ ����  ���� */
    public void ProcessIngameItem()
    {
        /*
         * ��� ������ ������ �ҷ���
         * �������� ���������� Ȯ����
         * ������ �̶�� ���� ���� 0�� 99999(�ִ���� ������ ������ ����)
         */
        /*
            //���� ����� �������� ���� ����� ���� ������ �ҷ��ͼ� �ݿ�
            AdvParamStructTbl itemStruct = engine.Param.StructTbl["Item{}"];
            foreach (var structTarget in itemStruct.Tbl)
            {
                if (structTarget.Value.Tbl["SavedAmount"].IntValue > -1)
                {
                    engine.Param.SetParameter(string.Format("Item[{0}].Amount", structTarget.Key), Mathf.Clamp( structTarget.Value.Tbl["SavedAmount"].IntValue - structTarget.Value.Tbl["UsedAmount"].IntValue,0,9999999));
                }
            }
         */
    }

    public void RequestNews()
    {
        PlayFabClientAPI.GetTitleNews(new GetTitleNewsRequest(), result =>
        {
            onNews?.Invoke(result);
            // Process news using result.News
        }, error => Debug.LogError(error.GenerateErrorReport()));
    }

}

public class JsonData
{
    // JSON Fields, ! Case-sensitive

    public string orderId;
    public string packageName;
    public string productId;
    public long purchaseTime;
    public int purchaseState;
    public string purchaseToken;
}

public class PayloadData
{
    public JsonData JsonData;

    // JSON Fields, ! Case-sensitive
    public string signature;
    public string json;

    public static PayloadData FromJson(string json)
    {
        var payload = JsonUtility.FromJson<PayloadData>(json);
        payload.JsonData = JsonUtility.FromJson<JsonData>(payload.json);
        return payload;
    }
}

public class GooglePurchase
{
    public PayloadData PayloadData;

    // JSON Fields, ! Case-sensitive
    public string Store;
    public string TransactionID;
    public string Payload;

    public static GooglePurchase FromJson(string json)
    {
        var purchase = JsonUtility.FromJson<GooglePurchase>(json);
        purchase.PayloadData = PayloadData.FromJson(purchase.Payload);
        return purchase;
    }
}
