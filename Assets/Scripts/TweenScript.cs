using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class TweenScript : MonoBehaviour
{
    public Vector3 startPos;
    public float time;
    public Ease targetProcess;
    public Ease scaleProcess;
    RectTransform rectTrans;
    Vector3 originalPos;
    public Vector3 startScale = Vector3.one;
    Vector3 originalScale;

    [System.Serializable]
    class ColorSwaper
    {
        public RawImage targetRaw;
        public Image targetImage;
        public TMP_Text targetText;

        public Color startColor;
        [System.NonSerialized]
        public Color targetColor;
        public float time;
        public Ease ease;

        public void StartSwap()
        {
            if (targetRaw)
            {
                targetRaw.color = startColor;
                targetRaw.DOColor(targetColor, time).SetEase(ease);
            }
            else if (targetText)
            {
                targetText.color = startColor;
                targetText.DOColor(targetColor, time).SetEase(ease);
            }
            else
            {
                targetImage.color = startColor;
                targetImage.DOColor(targetColor, time).SetEase(ease);
            }
        }
    }
    [SerializeField]
    List<ColorSwaper> colorSwapers;

    public void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
        originalPos = rectTrans.localPosition;
        originalScale = rectTrans.localScale;

        for (int i = 0; i < colorSwapers.Count; i++)
        {
            if (colorSwapers[i].targetRaw)
                colorSwapers[i].targetColor = colorSwapers[i].targetRaw.color;
            else if (colorSwapers[i].targetText)
                colorSwapers[i].targetColor = colorSwapers[i].targetText.color;
            else if (colorSwapers[i].targetImage)
                colorSwapers[i].targetColor = colorSwapers[i].targetImage.color;

        }

    }

    public void OnEnable()
    {
        transform.localPosition = startPos;
        rectTrans.DOLocalMove(originalPos, time).SetEase(targetProcess);
        if (originalScale != startScale)
        {
            rectTrans.localScale = startScale;
            rectTrans.DOScale(originalScale, time).SetEase(scaleProcess);
        }
        for (int i = 0; i < colorSwapers.Count; i++)
        {
            colorSwapers[i].StartSwap();
        }
        //DOTween.To(() => myFloat, x => myFloat = x, 100, 1);
    }
}
