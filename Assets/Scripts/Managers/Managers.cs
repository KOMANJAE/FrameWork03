using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }

    /* MonoBehaviour 상속 클래스 */
    GPGSAndPFManager _pf;
    public static GPGSAndPFManager PF { get { return Instance._pf; } }
    ADManager _ad;
    public static ADManager AD { get { return Instance._ad; } }

    void Start()
    {
        Init();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject obj = GameObject.Find("@Managers");
            if(obj == null)
            {
                obj = new GameObject { name = "@Managers" };

                obj.AddComponent<Managers>();
                obj.AddComponent<GPGSAndPFManager>();
                obj.AddComponent<ADManager>();
            }

            DontDestroyOnLoad(obj);

            s_instance = obj.GetComponent<Managers>();
            Instance._pf = obj.GetComponent<GPGSAndPFManager>();
            Instance._ad = obj.GetComponent<ADManager>();
        }
    }
}
