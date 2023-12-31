using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

}

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;

    [SerializeField]
    Sound[] sfx;
    [SerializeField]
    Sound[] bgm;

    [SerializeField]
    AudioSource bgmPlayer;
    [SerializeField]
    AudioSource[] sfxPlayer;

    private void Start()
    {
        _instance = this;
    }

    public void PlayBGM(string Name)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (Name == bgm[i].name)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string Name)
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if (Name == sfx[i].name)
            {
                for (int x = 0; x < sfxPlayer.Length; x++)
                {
                    if (!sfxPlayer[x].isPlaying)
                    {
                        sfxPlayer[x].clip = sfx[i].clip;
                        sfxPlayer[x].Play();
                        return;
                    }
                }
                Debug.Log("모든 오디오 플레이어가 재생중입니다.");
                return;
            }
        }
        Debug.Log($"{Name} + 이름의 효과음이 없습니다.");
    }
}
