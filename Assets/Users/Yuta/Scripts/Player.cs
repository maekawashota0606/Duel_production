using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputProvider _inputProvider = null;
    private Spiner _spiner = null;

    [SerializeField]
    private int _playerNo = 0;                  // プレイヤー番号
    private Vector3 _direction = Vector3.zero;  // 入力された方向
    private bool _beforeShoot = true;           // 発射前フラグ
    private int _possibleAttackCount = 1;       // 攻撃可能回数
    private int _attackCount = 0;               // 攻撃回数
    private int _possibleAvoidanceCount = 1;    // 回避可能回数
    private int _avoidanceCount = 0;            // 回避回数

    public void MyInit(Spiner spiner)
    {
        _inputProvider = gameObject.AddComponent<InputProvider>();
        _spiner = spiner;
    }

    public void MyUpdate(float delta, bool OnCounting)
    {
        // InputProviderを仲介し、入力をとる
        // gameStatusがcountingの時は、方向の入力のみ
        if (OnCounting)
        {
            GetDirection();
        }
        else
        {
            // gameStatusがfightingになったら、コマを発射
            if (_beforeShoot)
            {
                Shoot();
            }
            else
            {
                // コマを発射後は攻撃と回避の処理
                GetDirection();
                if (_attackCount > 0 && _direction != Vector3.zero)
                {
                    Attack();
                }
                if (_avoidanceCount > 0 && _direction != Vector3.zero)
                {
                    Avoidance();
                }
            }
        }
    }

    // 入力方向取得
    private void GetDirection()
    {
        switch (_playerNo)
        {
            case 1:
                _direction = _inputProvider.GetLeftStick1P() * -1;
                break;
            case 2:
                _direction = _inputProvider.GetLeftStick2P() * -1;
                break;
        }
        // InputProvideでマウスとコントローラで取得できるベクトルの
        // 向きが逆になっている
        // 　マウスはドラッグした方向と逆向き
        // 　コントローラはスティックを倒した方向
        // コントローラ使用を想定して、逆向き（-1をかけている）に変更
        // しているため、マウス操作の場合はドラッグした方向に動く
    }

    // コマ発射
    private void Shoot()
    {
        _spiner.SetDirection(_direction);
        _beforeShoot = false;
        _attackCount = _possibleAttackCount;
        _avoidanceCount = _possibleAvoidanceCount;
    }

    // 攻撃
    private void Attack()
    {
        if ((_playerNo == 1 && _inputProvider.GetFireDown1P(InputProvider.FireType.Circle) ||
             _playerNo == 2 && _inputProvider.GetFireDown2P(InputProvider.FireType.Circle)))
        {
            _spiner.AttackStart(_direction.normalized);
            _attackCount--;
        }
    }

    // 回避
    private void Avoidance()
    {
        if ((_playerNo == 1 && _inputProvider.GetFireDown1P(InputProvider.FireType.Cross) ||
             _playerNo == 2 && _inputProvider.GetFireDown2P(InputProvider.FireType.Cross)))
        {
            _spiner.SetDirection(_direction);
            _avoidanceCount--;
        }
    }
}
