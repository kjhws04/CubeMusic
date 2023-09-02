using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField]
    GameObject result_ui;

    [SerializeField]
    Text[] txt_Count;
    [SerializeField]
    Text txt_Coin;
    [SerializeField]
    Text txt_Score;
    [SerializeField]
    Text txt_MaxCombo;

    int currentSong = 0;
    public void SetCurrentSong(int songNum) { currentSong = songNum; }

    [SerializeField]
    ScoreManager _score;
    [SerializeField]
    ComboManager _combo;
    [SerializeField]
    TimingManager _timing;
    [SerializeField]
    DataManager _data;

    public void ShowResult()
    {
        FindObjectOfType<CenterFlame>().ResetMusic();
        StopBGM();

        result_ui.SetActive(true);
        for (int i = 0; i < txt_Count.Length; i++)
        {
            txt_Count[i].text = "0";
        }

        txt_Coin.text = "0";
        txt_Score.text = "0";
        txt_MaxCombo.text = "0";

        int[] t_jud = _timing.GetJudgeRecord();
        int t_curScr = _score.GetCurrentScore();
        int t_maxCom = _combo.GetMaxCombo();
        int t_coin = (t_curScr / 50);

        for (int i = 0; i < txt_Count.Length; i++)
        {
            txt_Count[i].text = string.Format("{0:#,##0}", t_jud[i]);
        }

        txt_Score.text = string.Format("{0:#,##0}", t_curScr);
        txt_MaxCombo.text = string.Format("{0:#,##0}", t_maxCom);
        txt_Coin.text = string.Format("{0:#,##0}", t_coin);

        if (t_curScr > _data.score[currentSong])
        {
            _data.score[currentSong] = t_curScr;
            _data.SaveScore();
        }
    }

    void StopBGM()
    {
        AudioManager._instance.StopBGM();
    }

    public void BtnMainMenu()
    {
        result_ui.SetActive(false);
        GameManager._instance.MainMenu();
        _combo.ResetCombo();
    }
}
