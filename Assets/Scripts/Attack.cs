using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private GameObject _spiner;
    [SerializeField]
    private float _possibleAttackTime = 0.5f;   // 攻撃時間
    private float _attackTime = 0;              // 攻撃タイマー

    // Update is called once per frame
    void Update()
    {
        // 攻撃時間終了
        _attackTime -= Time.deltaTime;
        if (gameObject.activeSelf && _attackTime <= 0)
        {
            // 非表示
            SetActive(false);
            // 元の位置に戻す
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = new Vector3(transform.localScale.x / 2, 0, 0);
        }
    }

    // 攻撃開始
    public void ActiveStart(Vector3 dir)
    {
        // 攻撃オブジェクト回転
        transform.RotateAround(_spiner.transform.position, Vector3.forward, Atan2(dir.x, dir.y));
        // 攻撃オブジェクト表示
        SetActive(true);
        _attackTime = _possibleAttackTime;
    }

    // 攻撃オブジェクトの表示／非表示
    public void SetActive(bool on)
    {
        gameObject.SetActive(on);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 攻撃ヒット
        if (collision.gameObject.CompareTag("Spiner"))
        {
            if (collision.gameObject != _spiner)
            {
                collision.gameObject.GetComponent<Spiner>().HitAttack();
            }
        }
        //攻撃の相殺
        else if (collision.gameObject.CompareTag("Attack"))
        {
            _spiner.GetComponent<Spiner>().DecelerationOffset();
        }
    }

    private static float Atan2(float x, float y)
    {
        return Mathf.Repeat(Mathf.Atan2(y, x) * Mathf.Rad2Deg, 360);
    }
}
