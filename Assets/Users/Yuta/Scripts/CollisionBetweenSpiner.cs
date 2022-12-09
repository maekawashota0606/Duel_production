using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBetweenSpiner : SingletonMonoBehaviour<CollisionBetweenSpiner>
{
    [SerializeField]
    private GameObject _spiner1 = null;     // �R�}1�Q�[���I�u�W�F�N�g
    [SerializeField]
    private GameObject _spiner2 = null;     // �R�}2�Q�[���I�u�W�F�N�g
    private float radius1 = 0f;             // �R�}1���a
    private float radius2 = 0f;             // �R�}2���a
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

    // �R�}���m�̏Փˏ���
    private void CheckCollisionBetweenKoma()
    {
        if (IsCollisionCircle(_spiner1.transform.position, _spiner2.transform.position, radius1, radius2) && !inCollision)
        {
            // �R�}1����
            _spiner1.GetComponent<Spiner>().ReflectSpiner(_spiner2.transform.position - _spiner1.transform.position);
            // �R�}2����
            _spiner2.GetComponent<Spiner>().ReflectSpiner(_spiner1.transform.position - _spiner2.transform.position);
            inCollision = true;
        }
        else
        {
            inCollision = false;
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
