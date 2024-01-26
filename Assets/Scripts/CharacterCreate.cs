using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreate : MonoBehaviour
{
    [System.Serializable]
    class RandomStat
    {
        public string attributeName;
        public int randomMin;
        public int randomMax;
    }
    [System.Serializable]
    class Attribute
    {
        public string attributeName;
        public string[] caculateName;
        public string[] targetParam;
        public float deafultValue;
    }

    [SerializeField]
    List<RandomStat> attributeRandom;
    [SerializeField]
    List<Attribute> attributeDeafult;

    [SerializeField]
    Text nameText;
    [SerializeField]
    Text classText;

    int classIndex = 0;
    [SerializeField]
    GameObject StartTarget;
    [SerializeField]
    GameObject CreateTarget;

    public void OnEnable()
    {
        StartTarget.SetActive(true);
        CreateTarget.SetActive(false);
        OnReRoll();
        SetUp();
    }

    public void SetUp()
    {
        //classText.text = engine.Param.GetParameter("Player[Class].StatusString").ToString();
        //nameText.text = engine.Param.GetParameter("Player[Name].StatusString").ToString();
    }

    public void OnClass(int addIndex)
    {
        //classIndex += addIndex;

        //if (classIndex >= engine.Param.StructTbl["Class{}"].Tbl.Count)
        //{
        //    classIndex = 0;
        //}
        //else if (classIndex < 0)
        //{
        //    classIndex = engine.Param.StructTbl["Class{}"].Tbl.Count - 1;
        //}

        SetClass();
        SetUp();
    }

    public void SetClass()
    {
        //int checkIndex = 0;
        //var paramTarget = engine.Param.StructTbl["Class{}"].Tbl;
        //AdvParamStruct paramSet = null;
        //string key = null;
        //foreach (var param in paramTarget)
        //{
        //    if (checkIndex == classIndex)
        //    {
        //        paramSet = param.Value;
        //        key = param.Key;
        //        break;
        //    }
        //    else
        //    {
        //        checkIndex += 1;
        //    }
        //}
        //if (paramSet != null)
        //{
        //    engine.Param.TrySetParameter("Player[HP].StatusInt", paramSet.Tbl["DeafultHp"].IntValue/*+(int)engine.Param.GetParameter("Player[Str].StatusInt")*/);
        //    engine.Param.TrySetParameter("Player[MP].StatusInt", paramSet.Tbl["DeafultMp"].IntValue/*+ (int)engine.Param.GetParameter("Player[Wiz].StatusInt")*/);
        //    engine.Param.TrySetParameter("Player[Calss_Id].StatusString", key);
        //    engine.Param.TrySetParameter("Player[Class].StatusString", paramSet.Tbl["ClassName"].StringValue);
        //    engine.Param.TrySetParameter("Player[CurrentHp].StatusInt", (int)engine.Param.GetParameter("Player[HP].StatusInt"));
        //    engine.Param.TrySetParameter("Player[CurrentMp].StatusInt", (int)engine.Param.GetParameter("Player[MP].StatusInt"));

        //    int tempAtt;

        //    for (int i = 0; i < attributeDeafult.Count; i++)
        //    {
        //        tempAtt = (int)attributeDeafult[i].deafultValue;

        //        for (int j = 0; j < attributeDeafult[i].caculateName.Length; j++)
        //        {
        //            tempAtt += (int)(float.Parse(engine.Param.GetParameter(string.Format("Player[{0}].StatusInt", attributeDeafult[i].targetParam[j])).ToString()) * paramSet.Tbl[attributeDeafult[i].caculateName[j]].FloatValue);

        //        }
        //        engine.Param.TrySetParameter(attributeDeafult[i].attributeName, tempAtt);
        //        Debug.Log(attributeDeafult[i].attributeName + "::" + engine.Param.GetParameterInt(attributeDeafult[i].attributeName));
        //    }
        //}
    }

    public void OnReRoll()
    {

        //for (int i = 0; i < attributeRandom.Count; i++)
        //{
        //    engine.Param.SetParameterInt(attributeRandom[i].attributeName, Random.Range(attributeRandom[i].randomMin, attributeRandom[i].randomMax));
        //}

        SetClass();
        SetUp();
    }

    public void StartCharacterCreate()
    {
        Managers.PF.ProcessIngameItem();
        //engine.UiManager.IsInputTrigCustom = true;

    }
}
