using UnityEngine;

public class Spiner : MonoBehaviour
{
    [SerializeField]
    private float _defaultSpeed = 1;
    [SerializeField]
    private int _power = 0;
    private float _currentSpeed = 0;
    /// <summary>
    /// コマの進行方向
    /// </summary>
    private Vector3 _direction = Vector3.zero;


    /// <summary>
    /// 何かしらの初期化処理
    /// </summary>
    public void MyInit()
    {

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
    public void ReflectWall()
    {

    }

    /// <summary>
    /// 他のコマに接触したときの処理
    /// </summary>
    public void ReflectSpiner()
    {
        
    }
    
    /// <summary>
    /// 割合での減速
    /// </summary>
    private void DecelerationRatio(float ratio)
    {

    }

    /// <summary>
    /// 定数での減速
    /// </summary>
    private void Deceleration(float num)
    {

    }
    #endregion
}
