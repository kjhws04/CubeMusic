using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static bool s_canPressKey = true;
    Rigidbody rigid;

    [SerializeField]
    float moveSpeed = 3;
    Vector3 dir = new Vector3();
    public Vector3 destPos = new Vector3();
    Vector3 orgPos = new Vector3();

    [SerializeField]
    float spinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();

    [SerializeField]
    float recoilPosY = 0.25f;
    [SerializeField]
    float recoilSpeed = 1.5f;

    bool canMove = true;
    bool isFalling = false;
    bool isMove = false;

    [SerializeField]
    Transform fakeCube = null;
    [SerializeField]
    Transform realCube = null;

    [SerializeField]
    TimingManager timing;
    CameraController _mainCam;
    [SerializeField]
    StatusManager _status;

    private void Start()
    {
        _mainCam = Camera.main.GetComponent<CameraController>();
        rigid = GetComponentInChildren<Rigidbody>();
        orgPos = transform.position;
    }

    public void Init()
    {
        transform.position = Vector3.zero;
        destPos = Vector3.zero;
        realCube.localPosition = Vector3.zero;
        canMove = true;
        s_canPressKey = true;
        isFalling = false;
        rigid.useGravity = false;
        rigid.isKinematic = true;
    }

    private void Update()
    {
        if (!GameManager._instance.isStartGame)
            return;

        CheckFalling();

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.W))
        {
            if (canMove && s_canPressKey && !isFalling)
            {
                Calc();
                if (timing.CheckTiming())
                {
                    StartAction();
                }
            }
        }
    }

    #region InputSystem (test)
    public void OnUpTap()
    {
        if (isMove == true)
            return;
        isMove = true;
        if (!GameManager._instance.isStartGame)
            return;
        CheckFalling();
        CheckCanMove();
        dir.Set(1, 0, 0);
        InputCalc();
    }

    public void OnDownTap()
    {
        if (isMove == true)
            return;
        isMove = true;
        if (!GameManager._instance.isStartGame)
            return;
        CheckFalling();
        CheckCanMove();
        dir.Set(-1, 0, 0);
        InputCalc();
    }

    public void OnLeftTap()
    {
        if (isMove == true)
            return;
        isMove = true;
        if (!GameManager._instance.isStartGame)
            return;
        CheckFalling();
        CheckCanMove();
        dir.Set(0, 0, -1);
        InputCalc();
    }

    public void OnRightTap()
    {
        if (isMove == true)
            return;
        isMove = true;
        if (!GameManager._instance.isStartGame)
            return;
        CheckFalling();
        CheckCanMove();
        dir.Set(0, 0, 1);
        InputCalc();
    }

    void CheckCanMove()
    {
        if (!canMove || !s_canPressKey || isFalling)
            return;
    }

    void InputCalc()
    {
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z); //이동 목표값 계산
        rotDir = new Vector3(-dir.z, 0, -dir.x); //회전 목표값
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        destRot = fakeCube.rotation;

        if (timing.CheckTiming())
        {
            StartAction();
        }
        isMove = false;
    }
    #endregion


    void Calc()
    {
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal")); //방향계산
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z); //이동 목표값 계산
        rotDir = new Vector3(-dir.z, 0, -dir.x); //회전 목표값
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        destRot = fakeCube.rotation;
    }

    void StartAction()
    {
        StartCoroutine(MoveCoroutine());
        StartCoroutine(SpinCoroutine());
        StartCoroutine(RecoilCoroutine());
        StartCoroutine(_mainCam.ZommCamCoroutine());
    }

    void CheckFalling()
    {
        if (!isFalling && canMove)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, 1.1f))
            {
                Falling();
            }
        }
    }

    void Falling()
    {
        isFalling = true;
        rigid.useGravity = true;
        rigid.isKinematic = false;
    }

    public void ResetFalling()
    {
        _status.DecreaseHp(1);
        AudioManager._instance.PlaySFX("Falling");

        if (!_status.IsDead())
        {
            isFalling = false;
            rigid.useGravity = false;
            rigid.isKinematic = true;
            transform.position = orgPos;
            realCube.localPosition = new Vector3(0, 0, 0);
        }
    }

    IEnumerator MoveCoroutine()
    {
        canMove = false;
        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destPos;
        canMove = true;
    }

    IEnumerator SpinCoroutine()
    {
        while (Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator RecoilCoroutine()
    {
        while (realCube.position.y < recoilPosY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        while(realCube.position.y > 0)
        {

            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        realCube.localPosition = new Vector3(0, 0, 0);
    }

}
