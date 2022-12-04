using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // PS4コントローラーAxisName
    private const string STICK_H = "Horizontal_Test";
    private const string STICK_V = "Vertical_Test";
    private const string BUTTON_BATSU = "Avoidance_Test";
    private const string BUTTON_MARU = "Attack_Test";

    [SerializeField]
    private Koma useKoma = null;                // 使用するコマ

    private Vector3 direction = Vector3.zero;   // 入力された方向
    private bool beforeShoot = true;
    private bool beforeAvoidance = true;
    private bool beforeAttack = true;

    // Update is called once per frame
    void Update()
    {
        if (StatusManager_Test.gameStatus == StatusManager_Test.GameStatus.waiting)
        {
            GetDirection();
            beforeShoot = true;
        }
        else if (StatusManager_Test.gameStatus == StatusManager_Test.GameStatus.fighting)
        {
            if (beforeShoot)
            {
                Shoot();
            }
            else if (beforeAvoidance || beforeAttack)
            {
                GetDirection();
                if (beforeAvoidance)
                {
                    Avoidance();
                }
                if (beforeAttack)
                {
                    Attack();
                }
            }
        }
    }

    // コマ発射
    private void Shoot()
    {
        if (direction == Vector3.zero)
        {
            GetDirection();
        }
        else
        {
            useKoma.ChangeDirection(direction);

            beforeShoot = false;
            beforeAvoidance = true;
            beforeAttack = true;
        }
    }

    // コマ回避
    private void Avoidance()
    {
        if (GetAction() == BUTTON_BATSU && direction != Vector3.zero)
        {
            useKoma.ChangeDirection(direction);
            beforeAvoidance = false;
        }
    }

    // 攻撃
    private void Attack()
    {
        if (GetAction() == BUTTON_MARU && direction != Vector3.zero)
        {
            useKoma.AttackStart(direction);
            beforeAttack = false;
        }
    }

    // コマのアクション方向取得
    private void GetDirection()
    {
#if UNITY_EDITOR
        if (gameObject.name == "Player2")
        {
            // マウスドラッグ情報取得
            direction = InputMouseDrag();
            return;
        }
#endif
        direction = InputControllerStick();
    }

    // コマのアクション情報取得
    private string GetAction()
    {
#if UNITY_EDITOR
        if (gameObject.name == "Player2")
        {
            return InputKeyboard();
        }
#endif
        return InputControllerButton();
    }

    // コントローラースティック情報取得
    private Vector3 InputControllerStick()
    {
        //ジョイスティックの左右
        float h = Input.GetAxis(STICK_H);
        //ジョイスティックの上下
        float v = Input.GetAxis(STICK_V);

        if (h != 0f || v != 0f)
        {
            return new Vector3(-h, -v, 0f);
        }

        return Vector3.zero;
    }

    // コントローラーボタン情報取得
    private string InputControllerButton()
    {
        if (Input.GetButtonDown(BUTTON_BATSU))
        {
            return BUTTON_BATSU;
        }
        else if (Input.GetButtonDown(BUTTON_MARU))
        {
            return BUTTON_MARU;
        }
        return null;
    }

#if UNITY_EDITOR
    private Vector3 startPos = Vector3.zero;    // マウスドラッグ開始位置

    // マウスドラッグ情報取得
    private Vector3 InputMouseDrag()
    {
        // ドラッグ開始位置を取得
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
        {
            Vector3 endPos = Input.mousePosition;
            return startPos - endPos;
        }

        return Vector3.zero;
    }

    // キーボード入力取得
    private string InputKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return BUTTON_BATSU;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            return BUTTON_MARU;
        }
        return null;
    }
#endif
}
