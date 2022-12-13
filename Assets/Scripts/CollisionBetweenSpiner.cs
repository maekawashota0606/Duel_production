using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class CollisionBetweenSpiner : SingletonMonoBehaviour<CollisionBetweenSpiner>
public class CollisionBetweenSpiner : MonoBehaviour
{
    [SerializeField]
    private GameObject _spiner1 = null;     // �R�}1�Q�[���I�u�W�F�N�g
    [SerializeField]
    private GameObject _spiner2 = null;     // �R�}2�Q�[���I�u�W�F�N�g
    private float _radius1 = 0;             // �R�}1���a
    private float _radius2 = 0;             // �R�}2���a
    
    // Start is called before the first frame update
    //void Start()
    public void MyInit()
    {
        _radius1 = _spiner1.transform.localScale.x / 2;
        _radius2 = _spiner2.transform.localScale.x / 2;
    }

    // Update is called once per frame
    public void MyUpdate()
    {
        CheckCollisionBetweenKoma();
    }

    // �R�}���m�̏Փˏ���
    private void CheckCollisionBetweenKoma()
    {
        Vector3 pos1 = _spiner1.transform.position;
        Vector3 pos2 = _spiner2.transform.position;
        if (IsCollisionCircle(pos1, pos2, _radius1, _radius2))
        {
            // �R�}1����
            _spiner1.GetComponent<Spiner>().ReflectSpiner(pos2 - pos1);
            // �R�}2����
            _spiner2.GetComponent<Spiner>().ReflectSpiner(pos1 - pos2);
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
