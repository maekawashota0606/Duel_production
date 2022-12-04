using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBetweenKoma_Test : SingletonMonoBehaviour<CollisionBetweenKoma_Test>
{
    [SerializeField]
    private GameObject koma1 = null;    // コマ1ゲームオブジェクト
    [SerializeField]
    private GameObject koma2 = null;    // コマ2ゲームオブジェクト

    private float radius1 = 0f;         // コマ1半径
    private float radius2 = 0f;         // コマ2半径

    // Start is called before the first frame update
    void Start()
    {
        radius1 = koma1.transform.localScale.x / 2f;
        radius2 = koma2.transform.localScale.x / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCollisionBetweenKoma();
    }

    // コマ同士の衝突処理
    private void CheckCollisionBetweenKoma()
    {
        if (IsCollisionCircle(koma1.transform.position, koma2.transform.position, radius1, radius2))
        {
            // コマ1反射
            koma1.GetComponent<Koma>().ReflectKoma(koma2.transform.position - koma1.transform.position);
            // コマ2反射
            koma2.GetComponent<Koma>().ReflectKoma(koma1.transform.position - koma2.transform.position);
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
