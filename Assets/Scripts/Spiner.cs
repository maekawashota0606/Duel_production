using UnityEngine;

public class Spiner : MonoBehaviour
{
    [SerializeField]
    private float _defaultSpeed = 1;
    [SerializeField]
    private int _power = 0;
    [SerializeField]
    public int hp = 1;
    [SerializeField]
    public float _currentSpeed = 0;

    Player _player;
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
    private Attack attackComponent = null;          // 攻撃オブジェクトのコンポーネント

    /// <summary>
    /// 何かしらの初期化処理
    /// </summary>
    public void MyInit()
    {
        _currentSpeed = _defaultSpeed;
        attackComponent = transform.GetChild(0).gameObject.GetComponent<Attack>();
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
        // 法線を参照して反射
        //SetDirection(Vector3.Reflect(_direction, normal).normalized);
        // ↑接触したタイミングでくっついて止まってしまう時があるため、
        // とりあえず反転
        SetDirection(_direction * -1);
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

    // 攻撃
    public void Attack(Vector3 dir)
    {
        attackComponent.ActiveStart(dir);
    }

    // 攻撃被弾時
    public void HitAttack(int damage = 3)
    {
        hp -= damage;
        Deceleration(decelerationHitRatio);
    }

    // 攻撃相殺時
    public void DecelerationOffset()
    {
        Deceleration(decelerationHitRatio);
        attackComponent.SetActive(false);
    }
}
