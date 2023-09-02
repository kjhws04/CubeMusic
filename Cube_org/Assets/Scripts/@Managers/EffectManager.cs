using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    [SerializeField]
    Animator noteHitAnim = null;
    [SerializeField]
    Animator judgementAnim = null;
    [SerializeField]
    Image judgementImage = null;
    [SerializeField]
    Sprite[] judgementSprite = null;

    string hit = "Hit";

    public void JudgementEffect(int jud_num)
    {
        judgementImage.sprite = judgementSprite[jud_num];
        judgementAnim.SetTrigger(hit);
    }

    public void NoteHitEffect()
    {
        noteHitAnim.SetTrigger(hit);
    }
}
