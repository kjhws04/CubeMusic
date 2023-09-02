using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;

    [SerializeField]
    Transform tfNoteAppear = null;

    [SerializeField]
    EffectManager _effect;
    TimingManager _timing;
    [SerializeField]
    ComboManager _combo;

    private void Start()
    {
        _timing = GetComponent<TimingManager>();
    }

    private void Update()
    {
        if (!GameManager._instance.isStartGame)
            return;

        currentTime += Time.deltaTime;

        if (currentTime >= 60d / bpm)
        { //1beat = 0.5s, 60s/BPM = 1beat
            GameObject t_note = PoolManager._instance.noteQueue.Dequeue(); //Ǯ�޴��� ���
            t_note.transform.position = tfNoteAppear.position; //��ġ
            t_note.SetActive(true); //Ȱ��ȭ
            _timing.boxNoteList.Add(t_note);
            currentTime -= 60d / bpm; //�ణ�� ���� ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                _timing.MissRecord();
                _effect.JudgementEffect(4); //miss Rect
                _combo.ResetCombo();
            }

            _timing.boxNoteList.Remove(collision.gameObject);

            PoolManager._instance.noteQueue.Enqueue(collision.gameObject); //�ı� ����
            collision.gameObject.SetActive(false); //��Ȱ��ȭ
        }
    }

    public void RemoveNote()
    {
        GameManager._instance.isStartGame = false;
        for (int i = 0; i < _timing.boxNoteList.Count; i++)
        {
            _timing.boxNoteList[i].SetActive(false);
            PoolManager._instance.noteQueue.Enqueue(_timing.boxNoteList[i]);
        }

        _timing.boxNoteList.Clear();
    }
}
