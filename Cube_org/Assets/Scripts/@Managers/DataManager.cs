using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;

public class DataManager : MonoBehaviour
{
    public int[] score;

    Param param = new Param(); // 파라미터, 데이터를 송수신할 때, 사용하는 class

    // <summary>
    // 저장된 데이터를 로드하는 함수
    // </summary>
    public void LoadScore()
    {
        if (!Backend.IsInitialized)
        {
            Debug.LogError("현재 서버와 연결이 끊겼습니다.");
            return;
        }

        Where where = new Where();
        var bro = Backend.GameData.GetMyData("Score", where);
        if (bro.IsSuccess())
        {
            Debug.Log("데이터 로드 성공!");
            Debug.Log(bro.GetReturnValue());
            ParsingData(bro.GetReturnValuetoJSON()["rows"][0]);
        }
        else
        {
            Debug.Log(bro.GetErrorCode() + " : " + bro.GetMessage());
        }
    }

    // <summary>
    // 데이터 테이블에 데이터를 추가하는 함수
    // </summary>
    public void SaveScore()
    {
        param.Add("Score", score);
        var bro = Backend.GameData.Insert("Score", param);

        if (bro.IsSuccess())
        {
            Debug.Log("데이터 추가 성공");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("데이터 추가 실패");
        }
    }

    // <summary>
    // Json => int 로 변환하고, 변수에 값 넣기
    // </summary>
    void ParsingData(JsonData json)
    {
        for (int i = 0; i < score.Length; i++)
        {
            score[i] = int.Parse(json["Score"]["L"][i][0].ToString());
        }
    }
}
