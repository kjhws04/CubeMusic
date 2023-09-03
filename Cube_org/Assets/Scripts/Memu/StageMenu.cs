using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Song
{
    public string name;
    public string composer;
    public int bpm;
    public Sprite sprite;
}

public class StageMenu : MonoBehaviour
{
    [SerializeField]
    GameObject controller;

    [SerializeField]
    Song[] songList;
    [SerializeField]
    Text songName;
    [SerializeField]
    Text songComposer;
    [SerializeField]
    Text songScore;
    [SerializeField]
    Image imgDisk;

    [SerializeField]
    GameObject TitleMenu;
    [SerializeField]
    DataManager _data;

    int currentSong = 0;

    private void OnEnable()
    {
        SettingSong();
    }

    public void BtnNext()
    {
        AudioManager._instance.PlaySFX("Touch");
        if (++currentSong > songList.Length -1)
            currentSong = 0;
        SettingSong();
    }

    public void BtnPrior()
    {
        AudioManager._instance.PlaySFX("Touch");
        if (--currentSong < 0)
            currentSong = songList.Length - 1;
        SettingSong();
    }

    public void SettingSong()
    {
        songName.text = songList[currentSong].name;
        songComposer.text = songList[currentSong].composer;
        songScore.text = string.Format("{0:#,##0}", _data.score[currentSong]);
        imgDisk.sprite = songList[currentSong].sprite;

        AudioManager._instance.PlayBGM("BGM" + currentSong);
    }

    public void BtnBack()
    {
        TitleMenu.SetActive(true);
        gameObject.SetActive(false);
        AudioManager._instance.StopBGM();
    }

    public void BtnPlay()
    {
        int bpm = songList[currentSong].bpm;

        GameManager._instance.GameStart(currentSong, bpm);
        gameObject.SetActive(false);
        controller.SetActive(true);
    }
}
