using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;

public class DataManager : MonoBehaviour
{
    public int[] score;

    Param param = new Param(); // �Ķ����, �����͸� �ۼ����� ��, ����ϴ� class

    // <summary>
    // ����� �����͸� �ε��ϴ� �Լ�
    // </summary>
    public void LoadScore()
    {
        if (!Backend.IsInitialized)
        {
            Debug.LogError("���� ������ ������ ������ϴ�.");
            return;
        }

        Where where = new Where();
        var bro = Backend.GameData.GetMyData("Score", where);
        if (bro.IsSuccess())
        {
            Debug.Log("������ �ε� ����!");
            Debug.Log(bro.GetReturnValue());
            ParsingData(bro.GetReturnValuetoJSON()["rows"][0]);
        }
        else
        {
            Debug.Log(bro.GetErrorCode() + " : " + bro.GetMessage());
        }
    }

    // <summary>
    // ������ ���̺� �����͸� �߰��ϴ� �Լ�
    // </summary>
    public void SaveScore()
    {
        param.Add("Score", score);
        var bro = Backend.GameData.Insert("Score", param);

        if (bro.IsSuccess())
        {
            Debug.Log("������ �߰� ����");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("������ �߰� ����");
        }
    }

    // <summary>
    // Json => int �� ��ȯ�ϰ�, ������ �� �ֱ�
    // </summary>
    void ParsingData(JsonData json)
    {
        for (int i = 0; i < score.Length; i++)
        {
            score[i] = int.Parse(json["Score"]["L"][i][0].ToString());
        }
    }
}
