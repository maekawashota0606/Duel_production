using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBetweenSpiner : SingletonMonoBehaviour<CollisionBetweenSpiner>
{
    [SerializeField]
    private GameObject _spiner1 = null;     // コマ1ゲームオブジェクト
    [SerializeField]
    private GameObject _spiner2 = null;     // コマ2ゲームオブジェクト
    private float radius1 = 0f;             // コマ1半径
    private float radius2 = 0f;             // コマ2半径
    private bool inCollision = false;


    // Start is called before the first frame update
    void Start()
    {
        radius1 = _spiner1.transform.localScale.x / 2f;
        radius2 = _spiner2.transform.localScale.x / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollisionBetweenKoma();
    }

    // コマ同士の衝突処理
    private void CheckCollisionBetweenKoma()
    {
        if (IsCollisionCircle(_spiner1.transform.position, _spiner2.transform.position, radius1, radius2) && !inCollision)
        {
            // コマ1反射
            _spiner1.GetComponent<Spiner>().ReflectSpiner(_spiner2.transform.position - _spiner1.transform.position);
            // コマ2反射
            _spiner2.GetComponent<Spiner>().ReflectSpiner(_spiner1.transform.position - _spiner2.transform.position);
            inCollision = true;
        }
        else
        {
            inCollision = false;
        }
    }

    // 円同士の衝突判定
    public bool IsCollisionCircle(Vector2 v1, Vector2 v2, float r1, float r2)
    {
        float a = v1.x - v2.x;
        float b = v1.y - v2.y;
        float c = Mathf.Sqrt(a * a + b * b);
        float d = r1 + r2;

        if (d >= c)
        {
            return true;
        }
        return false;
    }
}
