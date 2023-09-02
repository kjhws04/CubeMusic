using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    Text t_score;

    [SerializeField]
    int increaseScore = 10;
    int currentScore = 0;

    Animator scoreAnim;
    [SerializeField]
    ComboManager _combo;

    [SerializeField] float[] weight = null;
    [SerializeField] int comboBonusScore = 10;

    private void Start()
    {
        scoreAnim = GetComponent<Animator>();
        currentScore = 0;
        t_score.text = "0";
    }

    public void Init()
    {
        currentScore = 0;
        t_score.text = "0";
    }

    public void IncreaseScore(int judmentState)
    {
        //�޺� ����
        _combo.IncreaseCombo();

        //�޺� ����ġ ���
        int t_currentCombo = _combo.GetCurrentCombo();
        t_currentCombo = (t_currentCombo / 10) * comboBonusScore;

        //����ġ ���
        int t_increaseScore = increaseScore + t_currentCombo;
        t_increaseScore = (int)(t_increaseScore * weight[judmentState]);

        //���� �ݿ�
        currentScore += t_increaseScore;
        t_score.text = string.Format("{0:#,##0}", currentScore);

        //anim ����
        scoreAnim.SetTrigger("Hit");
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
