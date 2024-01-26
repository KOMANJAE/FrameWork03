using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GetItemUI : MonoBehaviour
{
    public static GetItemUI instance;

    [SerializeField]
    TMP_Text scriptText;

    [SerializeField]
    ScrollRect contentTarget;
    [SerializeField]
    GameObject elementTarget;


    List<GameObject> objList = new List<GameObject>();

    UnityAction actionTarget;

    [SerializeField]
    GameObject openGameObject;

    [SerializeField]
    ServiceShopDescription serviceShopDescription;

    public void Awake()
    {
        instance = this;
    }

    public void Open(string text, List<Texture> itemTexture, List<string> itemName, List<string> amount, UnityAction onOk)
    {
        openGameObject.SetActive(true);
        scriptText.text = text;
        actionTarget = onOk;

        if (objList.Count > 0)
        {
            for (int i = 0; i < objList.Count; i++)
            {
                Destroy(objList[i]);
                Debug.Log("destroyed");
            }
            Debug.Log("objClear");
            objList.Clear();
        }

        for (int i = 0; i < itemName.Count; i++)
        {
            GameObject obj = GameObject.Instantiate(elementTarget, contentTarget.content);
            obj.transform.Find("NameText").GetComponent<TMP_Text>().text = itemName[i];
            obj.transform.Find("AmountText").GetComponent<TMP_Text>().text = amount[i];
            objList.Add(obj);
        }
    }

    public void OpenCrystalShopDescription(CatalogItem catalog, Texture itemTexture, UnityAction onBuyEvent)
    {
        serviceShopDescription.Open(catalog, itemTexture, onBuyEvent);
    }

    public void OnBtnOk()
    {
        actionTarget?.Invoke();
        Resources.UnloadUnusedAssets(); //사용되지 않는 리소스들을 언로드 시키도록 한다
        openGameObject.SetActive(false);
    }

}
