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
        //콤보 증가
        _combo.IncreaseCombo();

        //콤보 가중치 계산
        int t_currentCombo = _combo.GetCurrentCombo();
        t_currentCombo = (t_currentCombo / 10) * comboBonusScore;

        //가중치 계산
        int t_increaseScore = increaseScore + t_currentCombo;
        t_increaseScore = (int)(t_increaseScore * weight[judmentState]);

        //점수 반영
        currentScore += t_increaseScore;
        t_score.text = string.Format("{0:#,##0}", currentScore);

        //anim 실행
        scoreAnim.SetTrigger("Hit");
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
