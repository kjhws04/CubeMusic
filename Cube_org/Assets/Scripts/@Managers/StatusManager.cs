using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusManager : MonoBehaviour
{
    [SerializeField]
    float blickSpeed = 0.1f;
    [SerializeField]
    int blinkCount = 10;
    int currentBlinkCount = 0;
    bool isBlink = false;

    [SerializeField]
    MeshRenderer playerMesh;

    bool isDead = false;
    int maxHp = 3;
    int currentHp = 3;

    int maxShield = 3;
    int currentShield = 0;

    [SerializeField]
    Image[] hpImage;
    [SerializeField]
    Image[] shieldImage;

    [SerializeField]
    Result _result;
    [SerializeField]
    NoteManager _note;

    [SerializeField]
    int shieldIncreaseCombo = 5;
    int currentshieldCombo = 0;
    [SerializeField]
    Image shieldGauge;

    public void OnEnable()
    {
        shieldGauge.fillAmount = 0;
    }

    public void Init()
    {
        currentHp = maxHp;
        currentShield = 0;
        currentshieldCombo = 0;
        shieldGauge.fillAmount = 0;
        isDead = false;
        SettingHpImage();
        SettingShieldImage();
    }

    public void CheckShield()
    {
        currentshieldCombo++;
        if (currentshieldCombo >= shieldIncreaseCombo)
        {
            currentshieldCombo = 0;
            InCreaseShield();
        }
        shieldGauge.fillAmount = (float)currentshieldCombo / shieldIncreaseCombo;
    }

    public void IncresaeHp(int num)
    {
        currentHp += num;
        if (currentHp >= maxHp)
            currentHp = maxHp;

        SettingHpImage();
    }

    public void DecreaseHp(int num)
    {
        if (!isBlink)
        {
            if (currentShield > 0)
                DecreaseShield(num);
            else
            {
                currentHp -= num;

                if (currentHp <= 0)
                {
                    isDead = true;
                    _result.ShowResult();
                    _note.RemoveNote();
                }
                else
                {
                    StartCoroutine(BlinkCoroutine());
                }

                SettingHpImage();

            }
        }
    }

    public void DecreaseShield(int num)
    {
        currentShield -= num;
        if (currentShield <= 0)
            currentShield = 0;
        SettingShieldImage();
    }

    void SettingShieldImage()
    {
        for (int i = 0; i < shieldImage.Length; i++)
        {
            if (i < currentShield)
                shieldImage[i].gameObject.SetActive(true);
            else
                shieldImage[i].gameObject.SetActive(false);
        }
    }

    public void InCreaseShield()
    {
        currentShield++;

        if (currentShield >= maxShield)
            currentShield = maxShield;

        SettingShieldImage();
    }

    public void ResetShieldCombo()
    {
        currentshieldCombo = 0;
        shieldGauge.fillAmount = (float)currentshieldCombo / shieldIncreaseCombo;
    }

    void SettingHpImage()
    {
        for (int i = 0; i < hpImage.Length; i++)
        {
            if (i < currentHp)
                hpImage[i].gameObject.SetActive(true);
            else
                hpImage[i].gameObject.SetActive(false);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    IEnumerator BlinkCoroutine()
    {
        isBlink = true;
        while (currentBlinkCount <= blinkCount)
        {
            playerMesh.enabled = !playerMesh.enabled;
            yield return new WaitForSeconds(blickSpeed);
            currentBlinkCount++;
        }
        playerMesh.enabled = true;
        currentBlinkCount = 0;
        isBlink = false;
    }
}
