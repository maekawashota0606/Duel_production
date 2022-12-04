using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager_Test : MonoBehaviour
{
    [SerializeField]
    private Koma koma1 = null;
    [SerializeField]
    private Koma koma2 = null;

    public static GameStatus gameStatus = GameStatus.start; // ゲームステータス    
    private float waitTime = 3f;                            // 戦闘開始待ち時間
    private float waitElapsedTime = 0f;                     // 戦闘開始待ち経過時間

    public enum GameStatus
    {
        start,          // ゲーム開始
        waiting,        // 戦闘開始待ち中
        fighting        // 戦闘中
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

    // スタート処理
    private void StartProc()
    {
        // 初期設定
        waitElapsedTime = 0f;
        gameStatus = GameStatus.waiting;
    }

    // 戦闘開始待機中処理
    private void WaitingProc()
    {
        // 戦闘開始待ち時間が経過したらゲームステータスを戦闘中に変更
        waitElapsedTime += Time.deltaTime;
        if (waitElapsedTime >= waitTime)
        {
            gameStatus = GameStatus.fighting;
        }
    }

    // 戦闘中処理
    private void FightingProc()
    {
        // 両方のコマが停止したら再スタート
        if (koma1.CheckStoped() && koma2.CheckStoped())
        {
            Debug.Log("RESTART");
            gameStatus = GameStatus.start;
            koma1.SetRestartSpeed();
            koma2.SetRestartSpeed();
        }
    }
}
