using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koma : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction = Vector3.zero;       // 方向
    [SerializeField]
    private float speed = 0f;                       // 速度
    [SerializeField]
    private float decelerationMoveRatio = 0.02f;    // 移動時減速率
    [SerializeField]
    private float decelerationWallRatio = 0.05f;    // 壁衝突時減速率
    [SerializeField]
    private float decelerationHitRatio = 0.1f;      // 攻撃被弾時減速率
    [SerializeField]
    private float decelerationOffsetRatio = 0.1f;   // 攻撃相殺時減速率
    [SerializeField]
    private float stopThreshold = 0f;               // 停止速度閾値   
    [SerializeField]
    private GameObject attackPrefab;                // 攻撃オブジェクトのプレハブ

    private GameObject attackObj = null;            // 攻撃オブジェクト
    private float attackRad = 0f;                   // 攻撃方向の角度
    private float possibleAttackTime = 0.5f;        // 攻撃可能時間
    private float attackTime = 0f;                  // 攻撃中時間

    // Update is called once per frame
    void Update()
    {
        if (StatusManager_Test.gameStatus == StatusManager_Test.GameStatus.fighting && CheckSpeed())
        {
            Move();

            if (attackObj is not null)
            {
                Attacking();
            }
        }
    }

    // 速度チェック
    private bool CheckSpeed()
    {
        // 速度が停止速度閾値を下回ったら速度を0にする
        if (speed < stopThreshold)
        {
            speed = 0f;
            return false;
        }

        return true;
    }

    // 停止チェック
    public bool CheckStoped()
    {
        if (speed == 0f)
        {
            return true;
        }
        return false;
    }

    // 進行方向の変更
    public void ChangeDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    // 移動
    private void Move()
    {
        // コマの移動
        transform.Translate(direction * speed * Time.deltaTime);
        // 減速
        Deceleration(decelerationMoveRatio);
    }

    // 減速
    private void Deceleration(float decelerationRatio)
    {
        speed -= speed * decelerationRatio * Time.deltaTime;
    }

    // 壁衝突時の反射
    public void ReflectWall(Vector3 normal)
    {
        // 法線を参照して反射
        ChangeDirection(Vector3.Reflect(direction, normal));
        // 減速
        Deceleration(decelerationWallRatio);
    }

    // コマ衝突時の反射
    public void ReflectKoma(Vector3 normal)
    {
        // とりあえず反転
        ChangeDirection(direction * -1);

        // 法線を参照して反射
        //ChangeDirection(Vector3.Reflect(direction, normal.normalized));
        // ↑衝突するタイミングでくっついてぐるぐる回ってしまう時がある
    }

    // 攻撃開始
    public void AttackStart(Vector3 dir)
    {
        // 攻撃方向の角度を計算
        attackRad = Mathf.Repeat(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, 360f);
        // 攻撃オブジェクトを生成
        attackObj = Instantiate(attackPrefab, GetAttackPos(), Quaternion.Euler(0f, 0f, attackRad));
        attackTime = 0f;
    }

    // 攻撃中
    private void Attacking()
    {
        attackTime += Time.deltaTime;
        // 攻撃可能時間内ならば攻撃オブジェクトをコマに合わせて移動
        if (attackTime < possibleAttackTime)
        {
            attackObj.transform.position = GetAttackPos();

        }
        // 攻撃可能時間を過ぎたら攻撃オブジェクト破壊
        else
        {
            Destroy(attackObj);
            attackObj = null;
        }
    }

    // 攻撃オブジェクトの位置計算
    private Vector3 GetAttackPos()
    {
        Vector3 objPos = gameObject.transform.position;
        objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(attackRad * Mathf.Deg2Rad);
        objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(attackRad * Mathf.Deg2Rad);

        return objPos;
    }

    // 攻撃被弾時
    public void DecelerationHit()
    {
        Deceleration(decelerationHitRatio);
    }

    // 攻撃相殺時
    public void DecelerationOffset()
    {
        Deceleration(decelerationOffsetRatio);
        Destroy(attackObj);
        attackObj = null;
    }

    // 再スタート時速度設定
    public void SetRestartSpeed()
    {
        speed = 1000f;
    }
}
