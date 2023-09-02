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
            Debug.Log($"�ڳ� ���� ���� ���� : {bro}");
            AutoLogin();
            _data.LoadScore();
        }
        else
        {
            Debug.Log($"�ڳ� ���� ���� ���� : {bro}");
        }
    }

    public void BtnRegist()
    {
        string t_id = id.text;
        string t_pw = password.text;

        BackendReturnObject bro = Backend.BMember.CustomSignUp(t_id, t_pw,"Test");

        if (bro.IsSuccess())
        {
            Debug.Log("ȸ������ �Ϸ�");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("ȸ������ ����");
        }
    }

    public void BtnLogin()
    {
        string t_id = id.text;
        string t_pw = password.text;

        BackendReturnObject bro = Backend.BMember.CustomLogin(t_id, t_pw);

        if (bro.IsSuccess())
        {
            Debug.Log("�α��� �Ϸ�");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("�α��� ����");
        }
    }

    // <summary>
    // �ڵ� �α���
    // </summary>
    public void AutoLogin()
    {
        var bro = Backend.BMember.LoginWithTheBackendToken();
        if (bro.IsSuccess())
        {
            Debug.Log("�ڵ� �α��� ����");
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log(bro.GetErrorCode() + " : " + bro.GetMessage());
        }
    }
}
