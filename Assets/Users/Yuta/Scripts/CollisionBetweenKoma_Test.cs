using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBetweenKoma_Test : SingletonMonoBehaviour<CollisionBetweenKoma_Test>
{
    [SerializeField]
    private GameObject koma1 = null;    // �R�}1�Q�[���I�u�W�F�N�g
    [SerializeField]
    private GameObject koma2 = null;    // �R�}2�Q�[���I�u�W�F�N�g

    private float radius1 = 0f;         // �R�}1���a
    private float radius2 = 0f;         // �R�}2���a

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

    // �R�}���m�̏Փˏ���
    private void CheckCollisionBetweenKoma()
    {
        if (IsCollisionCircle(koma1.transform.position, koma2.transform.position, radius1, radius2))
        {
            // �R�}1����
            koma1.GetComponent<Koma>().ReflectKoma(koma2.transform.position - koma1.transform.position);
            // �R�}2����
            koma2.GetComponent<Koma>().ReflectKoma(koma1.transform.position - koma2.transform.position);
        }
    }

    // �~���m�̏Փ˔���
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
