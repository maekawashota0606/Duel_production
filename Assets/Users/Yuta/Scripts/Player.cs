using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // PS4�R���g���[���[AxisName
    private const string STICK_H = "Horizontal_Test";
    private const string STICK_V = "Vertical_Test";
    private const string BUTTON_BATSU = "Avoidance_Test";
    private const string BUTTON_MARU = "Attack_Test";

    [SerializeField]
    private Koma useKoma = null;                // �g�p����R�}

    private Vector3 direction = Vector3.zero;   // ���͂��ꂽ����
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

    // �R�}����
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

    // �R�}���
    private void Avoidance()
    {
        if (GetAction() == BUTTON_BATSU && direction != Vector3.zero)
        {
            useKoma.ChangeDirection(direction);
            beforeAvoidance = false;
        }
    }

    // �U��
    private void Attack()
    {
        if (GetAction() == BUTTON_MARU && direction != Vector3.zero)
        {
            useKoma.AttackStart(direction);
            beforeAttack = false;
        }
    }

    // �R�}�̃A�N�V���������擾
    private void GetDirection()
    {
#if UNITY_EDITOR
        if (gameObject.name == "Player2")
        {
            // �}�E�X�h���b�O���擾
            direction = InputMouseDrag();
            return;
        }
#endif
        direction = InputControllerStick();
    }

    // �R�}�̃A�N�V�������擾
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

    // �R���g���[���[�X�e�B�b�N���擾
    private Vector3 InputControllerStick()
    {
        //�W���C�X�e�B�b�N�̍��E
        float h = Input.GetAxis(STICK_H);
        //�W���C�X�e�B�b�N�̏㉺
        float v = Input.GetAxis(STICK_V);

        if (h != 0f || v != 0f)
        {
            return new Vector3(-h, -v, 0f);
        }

        return Vector3.zero;
    }

    // �R���g���[���[�{�^�����擾
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
    private Vector3 startPos = Vector3.zero;    // �}�E�X�h���b�O�J�n�ʒu

    // �}�E�X�h���b�O���擾
    private Vector3 InputMouseDrag()
    {
        // �h���b�O�J�n�ʒu���擾
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

    // �L�[�{�[�h���͎擾
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
