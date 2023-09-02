using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : MonoBehaviour
{
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    NoteManager _note;
    [SerializeField]
    Result _result;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        _result = FindObjectOfType<Result>();
        _note = FindObjectOfType<NoteManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audio.Play();
            PlayerController.s_canPressKey = false;
            _note.RemoveNote();
            _result.ShowResult();
        }
    }
}
