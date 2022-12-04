using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koma : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction = Vector3.zero;       // ����
    [SerializeField]
    private float speed = 0f;                       // ���x
    [SerializeField]
    private float decelerationMoveRatio = 0.02f;    // �ړ���������
    [SerializeField]
    private float decelerationWallRatio = 0.05f;    // �ǏՓˎ�������
    [SerializeField]
    private float decelerationHitRatio = 0.1f;      // �U����e��������
    [SerializeField]
    private float decelerationOffsetRatio = 0.1f;   // �U�����E��������
    [SerializeField]
    private float stopThreshold = 0f;               // ��~���x臒l   
    [SerializeField]
    private GameObject attackPrefab;                // �U���I�u�W�F�N�g�̃v���n�u

    private GameObject attackObj = null;            // �U���I�u�W�F�N�g
    private float attackRad = 0f;                   // �U�������̊p�x
    private float possibleAttackTime = 0.5f;        // �U���\����
    private float attackTime = 0f;                  // �U��������

    // Update is called once per frame
    void Update()
    {
        if (StatusManager_Test.gameStatus == StatusManager_Test.GameStatus.fighting && CheckSpeed())
        {
            Move();

            if (attackObj is not null)
            {
                Attacking();
            }
        }
    }

    // ���x�`�F�b�N
    private bool CheckSpeed()
    {
        // ���x����~���x臒l����������瑬�x��0�ɂ���
        if (speed < stopThreshold)
        {
            speed = 0f;
            return false;
        }

        return true;
    }

    // ��~�`�F�b�N
    public bool CheckStoped()
    {
        if (speed == 0f)
        {
            return true;
        }
        return false;
    }

    // �i�s�����̕ύX
    public void ChangeDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    // �ړ�
    private void Move()
    {
        // �R�}�̈ړ�
        transform.Translate(direction * speed * Time.deltaTime);
        // ����
        Deceleration(decelerationMoveRatio);
    }

    // ����
    private void Deceleration(float decelerationRatio)
    {
        speed -= speed * decelerationRatio * Time.deltaTime;
    }

    // �ǏՓˎ��̔���
    public void ReflectWall(Vector3 normal)
    {
        // �@�����Q�Ƃ��Ĕ���
        ChangeDirection(Vector3.Reflect(direction, normal));
        // ����
        Deceleration(decelerationWallRatio);
    }

    // �R�}�Փˎ��̔���
    public void ReflectKoma(Vector3 normal)
    {
        // �Ƃ肠�������]
        ChangeDirection(direction * -1);

        // �@�����Q�Ƃ��Ĕ���
        //ChangeDirection(Vector3.Reflect(direction, normal.normalized));
        // ���Փ˂���^�C�~���O�ł������Ă��邮�����Ă��܂���������
    }

    // �U���J�n
    public void AttackStart(Vector3 dir)
    {
        // �U�������̊p�x���v�Z
        attackRad = Mathf.Repeat(Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, 360f);
        // �U���I�u�W�F�N�g�𐶐�
        attackObj = Instantiate(attackPrefab, GetAttackPos(), Quaternion.Euler(0f, 0f, attackRad));
        attackTime = 0f;
    }

    // �U����
    private void Attacking()
    {
        attackTime += Time.deltaTime;
        // �U���\���ԓ��Ȃ�΍U���I�u�W�F�N�g���R�}�ɍ��킹�Ĉړ�
        if (attackTime < possibleAttackTime)
        {
            attackObj.transform.position = GetAttackPos();

        }
        // �U���\���Ԃ��߂�����U���I�u�W�F�N�g�j��
        else
        {
            Destroy(attackObj);
            attackObj = null;
        }
    }

    // �U���I�u�W�F�N�g�̈ʒu�v�Z
    private Vector3 GetAttackPos()
    {
        Vector3 objPos = gameObject.transform.position;
        objPos.x += attackPrefab.transform.localScale.x / 2f * Mathf.Cos(attackRad * Mathf.Deg2Rad);
        objPos.y += attackPrefab.transform.localScale.x / 2f * Mathf.Sin(attackRad * Mathf.Deg2Rad);

        return objPos;
    }

    // �U����e��
    public void DecelerationHit()
    {
        Deceleration(decelerationHitRatio);
    }

    // �U�����E��
    public void DecelerationOffset()
    {
        Deceleration(decelerationOffsetRatio);
        Destroy(attackObj);
        attackObj = null;
    }

    // �ăX�^�[�g�����x�ݒ�
    public void SetRestartSpeed()
    {
        speed = 1000f;
    }
}
