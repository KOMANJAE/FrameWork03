using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class News : MonoBehaviour
{
    [System.Serializable]
    class NewsUI
    {
        public ScrollRect contentTarget;
        public GameObject contentElement;
    }
    [SerializeField]
    NewsUI newsUI;

    // Start is called before the first frame update
    void Start()
    {
        Managers.PF.onNews += NewsFeed;

        Managers.PF.RequestNews();
    }

    private void NewsFeed(GetTitleNewsResult result)
    {
        for (int i = 0; i < result.News.Count; i++)
        {
            bool isOpenState = false;
            if (i == 0)
                isOpenState = true;
            GameObject obj = GameObject.Instantiate(newsUI.contentElement, newsUI.contentTarget.content);
            obj.GetComponent<NewsElement>().SetUp(result.News[i].Title, result.News[i].Body, isOpenState);

        }
    }
}
