using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager_Test : MonoBehaviour
{
    [SerializeField]
    private Koma koma1 = null;
    [SerializeField]
    private Koma koma2 = null;

    public static GameStatus gameStatus = GameStatus.start; // �Q�[���X�e�[�^�X    
    private float waitTime = 3f;                            // �퓬�J�n�҂�����
    private float waitElapsedTime = 0f;                     // �퓬�J�n�҂��o�ߎ���

    public enum GameStatus
    {
        start,          // �Q�[���J�n
        waiting,        // �퓬�J�n�҂���
        fighting        // �퓬��
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStatus == GameStatus.start)
        {
            StartProc();
        }
        else if (gameStatus == StatusManager_Test.GameStatus.waiting)
        {
            WaitingProc();
        }
        else if (gameStatus == StatusManager_Test.GameStatus.fighting)
        {
            FightingProc();
        }
    }

    // �X�^�[�g����
    private void StartProc()
    {
        // �����ݒ�
        waitElapsedTime = 0f;
        gameStatus = GameStatus.waiting;
    }

    // �퓬�J�n�ҋ@������
    private void WaitingProc()
    {
        // �퓬�J�n�҂����Ԃ��o�߂�����Q�[���X�e�[�^�X��퓬���ɕύX
        waitElapsedTime += Time.deltaTime;
        if (waitElapsedTime >= waitTime)
        {
            gameStatus = GameStatus.fighting;
        }
    }

    // �퓬������
    private void FightingProc()
    {
        // �����̃R�}����~������ăX�^�[�g
        if (koma1.CheckStoped() && koma2.CheckStoped())
        {
            Debug.Log("RESTART");
            gameStatus = GameStatus.start;
            koma1.SetRestartSpeed();
            koma2.SetRestartSpeed();
        }
    }
}
