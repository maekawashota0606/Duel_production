using UnityEngine;

public class Spiner : MonoBehaviour
{
    [SerializeField]
    private float _defaultSpeed = 1;
    [SerializeField]
    private int _power = 0;
    [SerializeField]
    private float _currentSpeed = 0;
    /// <summary>
    /// コマの進行方向
    /// </summary>
    private Vector3 _direction = Vector3.zero;

    [SerializeField]
    private float decelerationMoveRatio = 0.05f;    // 移動時減速率
    [SerializeField]
    private float decelerationWallRatio = 0.1f;     // 壁衝突時減速率
    [SerializeField]
    private float decelerationHitRatio = 0.2f;      // 攻撃被弾時減速率
    [SerializeField]
    private GameObject attackSpearPrefab;           // 攻撃（槍）プレハブ
    private float attackPrefabLength = 0;           // 攻撃プレハブの長さ
    private GameObject attackObj = null;            // 攻撃オブジェクト
    private float attackRad = 0;                    // 攻撃方向の角度
    private float possibleAttackTime = 0.5f;        // 攻撃時間
    private float attackTime = 0;                   // 攻撃タイマー

    /// <summary>
    /// 何かしらの初期化処理
    /// </summary>
    public void MyInit()
    {
        _currentSpeed = _defaultSpeed;
        attackPrefabLength = attackSpearPrefab.transform.localScale.x;
    }

    /// <summary>
    /// 基本的に毎フレーム実行する処理
    /// </summary>
    /// <param name="delta"></param>
    public void MyUpdate(float delta)
    {
        // 一定以下のスピードなら停止とみなす
        if (_currentSpeed < GameDirector._minSpeed)
        {
            OnStopped();
            return;
        }
        else
        {
            Move(delta);

            if (attackObj is not null)
            {
                Attacking(delta);
            }
        }
    }

    #region getter, setter
    public void SetDirection(Vector3 dir)
    {
        _direction = dir;
    }
    #endregion

    #region コマの挙動
    /// <summary>
    /// コマの移動
    /// </summary>
    public void Move(float delta)
    {
        // コマの動かし方について

        // ゲーム的な動きを追求  →  自前で計算
        // コマの移動、加減速、壁やコマ同士の接触判定、接触時の反射や移動距離、壁貫通対策を自前で計算しなければならない

        // 楽に動かす方法        →  Unity標準の物理エンジン
        // ゲーム的な動きにはならない(物理 ＋ 非物理で動かすのは相性が良くない)

        // 折衷案                →  RigidbodyのVelocityだけ操作
        // 速度を直接いじりつつ、反射などは物理エンジンに任せる

        transform.Translate(_direction * _currentSpeed * delta);
        DecelerationRatio(decelerationMoveRatio, delta);
    }

    /// <summary>
    /// コマ停止時の処理
    /// </summary>
    public void OnStopped()
    {
        _currentSpeed = 0;
    }

    /// <summary>
    /// 壁に反射したときの処理
    /// </summary>
    public void ReflectWall(Vector3 normal)
    {
        // 法線を参照して反射
        SetDirection(Vector3.Reflect(_direction, normal));
        // 減速
        Deceleration(decelerationWallRatio);
    }

    /// <summary>
    /// 他のコマに接触したときの処理
    /// </summary>
    public void ReflectSpiner(Vector3 normal)
    {
        // とりあえず反転
        SetDirection(_direction * -1);

        // 法線を参照して反射
        //SetDirection(Vector3.Reflect(_direction, normal).normalized);
        // ↑接触したタイミングでくっついて止まってしまう時がある
    }

    /// <summary>
    /// 割合での減速
    /// </summary>
    private void DecelerationRatio(float ratio, float delta)
    {
        _currentSpeed -= _currentSpeed * ratio * delta;
    }

    /// <summary>
    /// 定数での減速
    /// </summary>
    private void Deceleration(float num)
    {

    }
    #endregion

    // 攻撃開始
    public void AttackStart(Vector3 dir)
    {
        // 攻撃方向の角度を計算
        attackRad = Atan2(dir.x, dir.y);
        // 攻撃オブジェクトを生成
        attackObj = Instantiate(attackSpearPrefab, GetAttackPos(), Quaternion.Euler(0, 0, attackRad));
        attackObj.GetComponent<Attack>().MyInit(gameObject);
        attackTime = possibleAttackTime;
    }

    // 攻撃中
    private void Attacking(float delta)
    {
        attackTime -= delta;
        // 攻撃時間内ならば攻撃オブジェクトをコマに合わせて移動
        if (attackTime > 0)
        {
            attackObj.transform.position = GetAttackPos();

        }
        // 攻撃終了
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
        objPos.x += attackPrefabLength / 2 * Cos(attackRad);
        objPos.y += attackPrefabLength / 2 * Sin(attackRad);

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
        Deceleration(decelerationHitRatio);
        Destroy(attackObj);
        attackObj = null;
    }

    private static float Sin(float rad)
    {
        return Mathf.Sin(rad * Mathf.Deg2Rad);
    }

    private static float Cos(float rad)
    {
        return Mathf.Cos(rad * Mathf.Deg2Rad);
    }

    private static float Atan2(float x, float y)
    {
        return Mathf.Repeat(Mathf.Atan2(y, x) * Mathf.Rad2Deg, 360f);
    }
}
