using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public bool isStartGame = false;

    [SerializeField]
    GameObject[] goGameUI;
    [SerializeField]
    GameObject titleUI;

    [SerializeField]
    ComboManager _combo;
    [SerializeField]
    ScoreManager _score;
    [SerializeField]
    TimingManager _timing;
    [SerializeField]
    StatusManager _status;
    [SerializeField]
    PlayerController _player;
    [SerializeField]
    StageManager _stage;
    [SerializeField]
    NoteManager _note;
    [SerializeField]
    CenterFlame _center;
    [SerializeField]
    Result _result;

    private void Awake()
    {
        _instance = this;
        Application.targetFrameRate = 60;
    }

    public void GameStart(int songNum, int bpm)
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(true);
        }
        _center.bgmName = "BGM" + songNum;
        _note.bpm = bpm;
        _stage.RemoveStage();
        _stage.SettingStage(songNum);
        _combo.ResetCombo();
        _score.Init();
        _timing.Init();
        _status.Init();
        _player.Init();
        _result.SetCurrentSong(songNum);

        AudioManager._instance.StopBGM();

        isStartGame = true;
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        for (int i = 0; i < goGameUI.Length; i++)
        {
            goGameUI[i].SetActive(false);
        }
        titleUI.SetActive(true);
    }
}
