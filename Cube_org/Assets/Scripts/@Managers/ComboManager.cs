using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField]
    GameObject comboImg;
    [SerializeField]
    Text comboTxt;

    Animator comboAnim;
    string hit = "Hit";

    int currentCombo = 0;
    int maxCombo = 0;

    private void Start()
    {
        comboAnim = GetComponent<Animator>();
        comboTxt.gameObject.SetActive(false);
        comboImg.SetActive(false);
    }

    public void IncreaseCombo(int com_num = 1)
    {
        currentCombo += com_num;
        comboTxt.text = string.Format("{0:#,##0}", currentCombo);

        if (maxCombo < currentCombo)
            maxCombo = currentCombo;

        if (currentCombo >2)
        {
            comboTxt.gameObject.SetActive(true);
            comboImg.SetActive(true);

            comboAnim.SetTrigger(hit);
        }
    }

    public int GetCurrentCombo()
    {
        return currentCombo;
    }

    public int GetMaxCombo()
    {
        return maxCombo;
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        comboTxt.text = "0";
        comboTxt.gameObject.SetActive(false);
        comboImg.SetActive(false);
    }
}
