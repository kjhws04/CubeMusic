using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField]
    Transform Center = null;
    [SerializeField]
    RectTransform[] timingRect = null;
    Vector2[] timingBoxs = null;

    [SerializeField]
    EffectManager _effect;
    [SerializeField]
    ScoreManager _score;
    [SerializeField]
    ComboManager _combo;
    [SerializeField]
    StageManager _stage;
    [SerializeField]
    PlayerController _player;
    [SerializeField]
    StatusManager _status;
    AudioManager _audio;

    #region Recode Score
    int[] judgeRecord = new int[5];
    #endregion

    private void Start()
    {
        _audio = AudioManager._instance;
        //타이밍 박스 설정
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    public void Init()
    {
        for (int i = 0; i < judgeRecord.Length; i++)
        {
            judgeRecord[i] = 0;
        }
    }

    public bool CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for (int x = 0; x < timingBoxs.Length; x++)
            {
                if (timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    HitEffect(x);
                    HitNote(i);

                    if (CheckcanNextPlate())
                    {
                        _score.IncreaseScore(x); //점수 증가
                        _stage.ShowNextPlate(); //발판 생성
                        _effect.JudgementEffect(x);
                        judgeRecord[x]++;
                        _status.CheckShield();
                    }
                    else
                    {
                        _effect.JudgementEffect(5);
                    }
                    _audio.PlaySFX("Clap");
                    return true;
                }
            }
        }

        _combo.ResetCombo();
        _effect.JudgementEffect(timingBoxs.Length);
        MissRecord();
        return false;
    }

    void HitNote(int count)
    {
        boxNoteList[count].GetComponent<Note>().HideNote();
        boxNoteList.RemoveAt(count);
    }

    void HitEffect(int x)
    {
        if (x < timingBoxs.Length - 1)
            _effect.NoteHitEffect();
    }

    bool CheckcanNextPlate()
    {
        if (Physics.Raycast(_player.destPos, Vector3.down, out RaycastHit t_hitInfo, 1.1f))
        {
            if (t_hitInfo.transform.CompareTag("BasicPlate"))
            {
                BasicPlate t_plate = t_hitInfo.transform.GetComponent<BasicPlate>();
                if (t_plate.flag)
                {
                    t_plate.flag = false;
                    return true;
                }
            }
        }
        return false;
    }

    public int[] GetJudgeRecord()
    {
        return judgeRecord;
    }

    public void MissRecord()
    {
        judgeRecord[4]++;
        _status.ResetShieldCombo();
    }
}
