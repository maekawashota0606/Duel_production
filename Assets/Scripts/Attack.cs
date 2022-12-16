using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private GameObject _spiner;
    [SerializeField]
    private float _possibleAttackTime = 0.5f;   // �U������
    private float _attackTime = 0;              // �U���^�C�}�[

    // Update is called once per frame
    void Update()
    {
        // �U�����ԏI��
        _attackTime -= Time.deltaTime;
        if (gameObject.activeSelf && _attackTime <= 0)
        {
            // ��\��
            SetActive(false);
            // ���̈ʒu�ɖ߂�
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = new Vector3(transform.localScale.x / 2, 0, 0);
        }
    }

    // �U���J�n
    public void ActiveStart(Vector3 dir)
    {
        // �U���I�u�W�F�N�g��]
        transform.RotateAround(_spiner.transform.position, Vector3.forward, Atan2(dir.x, dir.y));
        // �U���I�u�W�F�N�g�\��
        SetActive(true);
        _attackTime = _possibleAttackTime;
    }

    // �U���I�u�W�F�N�g�̕\���^��\��
    public void SetActive(bool on)
    {
        gameObject.SetActive(on);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // �U���q�b�g
        if (collision.gameObject.CompareTag("Spiner"))
        {
            if (collision.gameObject != _spiner)
            {
                collision.gameObject.GetComponent<Spiner>().HitAttack();
            }
        }
        //�U���̑��E
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
