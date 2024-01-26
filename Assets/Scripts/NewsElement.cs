using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewsElement : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    class UI
    {
        public TMP_Text TitleText;
        public TMP_Text descriptionText;
        public GameObject obj;
    }
    [SerializeField]
    UI openState;
    [SerializeField]
    UI closeState;


    public void SetUp(string title, string description, bool isOpenState)
    {
        openState.TitleText.text = title;
        openState.descriptionText.text = description;
        closeState.TitleText.text = title;
        if (isOpenState)
        {
            openState.obj.SetActive(true);
            closeState.obj.SetActive(false);
        }
    }
}
