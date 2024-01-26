using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    TMP_Text textTarget;

    public void OnEnable()
    {
        Managers.PF.crystalChange += OnCrystalChange;
        textTarget.text = Managers.PF.GetCrystal().ToString();
    }

    public void OnDisable()
    {
        Managers.PF.crystalChange -= OnCrystalChange;
    }

    private void OnCrystalChange(int currentCrystal)
    {
        textTarget.text = currentCrystal.ToString();
    }
}
