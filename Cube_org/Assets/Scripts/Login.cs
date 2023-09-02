using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class Login : MonoBehaviour
{
    [SerializeField]
    InputField id;
    [SerializeField]
    InputField password;

    [SerializeField]
    DataManager _data;

    void Awake()
    {
        BackendSetUp();
    }

    void BackendSetUp()
    {
        var bro = Backend.Initialize(true);
        if (bro.IsSuccess())
        {
            Debug.Log($"뒤끝 서버 연동 성공 : {bro}");
            AutoLogin();
            _data.LoadScore();
        }
        else
        {
            Debug.Log($"뒤끝 서버 연동 실패 : {bro}");
        }
    }

    public void BtnRegist()
    {
        string t_id = id.text;
        string t_pw = password.text;

        BackendReturnObject bro = Backend.BMember.CustomSignUp(t_id, t_pw,"Test");

        if (bro.IsSuccess())
        {
            Debug.Log("회원가입 완료");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("회원가입 실패");
        }
    }

    public void BtnLogin()
    {
        string t_id = id.text;
        string t_pw = password.text;

        BackendReturnObject bro = Backend.BMember.CustomLogin(t_id, t_pw);

        if (bro.IsSuccess())
        {
            Debug.Log("로그인 완료");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("로그인 실패");
        }
    }

    // <summary>
    // 자동 로그인
    // </summary>
    public void AutoLogin()
    {
        var bro = Backend.BMember.LoginWithTheBackendToken();
        if (bro.IsSuccess())
        {
            Debug.Log("자동 로그인 성공");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log(bro.GetErrorCode() + " : " + bro.GetMessage());
        }
    }
}
