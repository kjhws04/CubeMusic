using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    [SerializeField]
    GameObject stageUI;

    public void BtnPlay()
    {
        stageUI.SetActive(true);
        gameObject.SetActive(false);
    }
}
