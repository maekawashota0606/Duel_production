using UnityEngine;

public class Spiner : MonoBehaviour
{
    [SerializeField]
    private float _defaultSpeed = 1;
    [SerializeField]
    private int _power = 0;
    [SerializeField]
    private float _currentSpeed = 0;
    /// <summary>
    /// �R�}�̐i�s����
    /// </summary>
    private Vector3 _direction = Vector3.zero;

    [SerializeField]
    private float decelerationMoveRatio = 0.05f;    // �ړ���������
    [SerializeField]
    private float decelerationWallRatio = 0.1f;     // �ǏՓˎ�������
    [SerializeField]
    private float decelerationHitRatio = 0.2f;      // �U����e��������
    [SerializeField]
    private GameObject attackSpearPrefab;           // �U���i���j�v���n�u
    private float attackPrefabLength = 0;           // �U���v���n�u�̒���
    private GameObject attackObj = null;            // �U���I�u�W�F�N�g
    private float attackRad = 0;                    // �U�������̊p�x
    private float possibleAttackTime = 0.5f;        // �U������
    private float attackTime = 0;                   // �U���^�C�}�[

    /// <summary>
    /// ��������̏���������
    /// </summary>
    public void MyInit()
    {
        _currentSpeed = _defaultSpeed;
        attackPrefabLength = attackSpearPrefab.transform.localScale.x;
    }

    /// <summary>
    /// ��{�I�ɖ��t���[�����s���鏈��
    /// </summary>
    /// <param name="delta"></param>
    public void MyUpdate(float delta)
    {
        // ���ȉ��̃X�s�[�h�Ȃ��~�Ƃ݂Ȃ�
        if (_currentSpeed < GameDirector._minSpeed)
        {
            OnStopped();
            return;
        }
        else
        {
            Move(delta);

            if (attackObj is not null)
            {
                Attacking(delta);
            }
        }
    }

    #region getter, setter
    public void SetDirection(Vector3 dir)
    {
        _direction = dir;
    }
    #endregion

    #region �R�}�̋���
    /// <summary>
    /// �R�}�̈ړ�
    /// </summary>
    public void Move(float delta)
    {
        // �R�}�̓��������ɂ���

        // �Q�[���I�ȓ�����ǋ�  ��  ���O�Ōv�Z
        // �R�}�̈ړ��A�������A�ǂ�R�}���m�̐ڐG����A�ڐG���̔��˂�ړ������A�Ǌђʑ΍�����O�Ōv�Z���Ȃ���΂Ȃ�Ȃ�

        // �y�ɓ��������@        ��  Unity�W���̕����G���W��
        // �Q�[���I�ȓ����ɂ͂Ȃ�Ȃ�(���� �{ �񕨗��œ������̂͑������ǂ��Ȃ�)

        // �ܒ���                ��  Rigidbody��Velocity��������
        // ���x�𒼐ڂ�����A���˂Ȃǂ͕����G���W���ɔC����

        transform.Translate(_direction * _currentSpeed * delta);
        DecelerationRatio(decelerationMoveRatio, delta);
    }

    /// <summary>
    /// �R�}��~���̏���
    /// </summary>
    public void OnStopped()
    {
        _currentSpeed = 0;
    }

    /// <summary>
    /// �ǂɔ��˂����Ƃ��̏���
    /// </summary>
    public void ReflectWall(Vector3 normal)
    {
        // �@�����Q�Ƃ��Ĕ���
        SetDirection(Vector3.Reflect(_direction, normal));
        // ����
        Deceleration(decelerationWallRatio);
    }

    /// <summary>
    /// ���̃R�}�ɐڐG�����Ƃ��̏���
    /// </summary>
    public void ReflectSpiner(Vector3 normal)
    {
        // �Ƃ肠�������]
        SetDirection(_direction * -1);

        // �@�����Q�Ƃ��Ĕ���
        //SetDirection(Vector3.Reflect(_direction, normal).normalized);
        // ���ڐG�����^�C�~���O�ł������Ď~�܂��Ă��܂���������
    }

    /// <summary>
    /// �����ł̌���
    /// </summary>
    private void DecelerationRatio(float ratio, float delta)
    {
        _currentSpeed -= _currentSpeed * ratio * delta;
    }

    /// <summary>
    /// �萔�ł̌���
    /// </summary>
    private void Deceleration(float num)
    {

    }
    #endregion

    // �U���J�n
    public void AttackStart(Vector3 dir)
    {
        // �U�������̊p�x���v�Z
        attackRad = Atan2(dir.x, dir.y);
        // �U���I�u�W�F�N�g�𐶐�
        attackObj = Instantiate(attackSpearPrefab, GetAttackPos(), Quaternion.Euler(0, 0, attackRad));
        attackObj.GetComponent<Attack>().MyInit(gameObject);
        attackTime = possibleAttackTime;
    }

    // �U����
    private void Attacking(float delta)
    {
        attackTime -= delta;
        // �U�����ԓ��Ȃ�΍U���I�u�W�F�N�g���R�}�ɍ��킹�Ĉړ�
        if (attackTime > 0)
        {
            attackObj.transform.position = GetAttackPos();

        }
        // �U���I��
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
        objPos.x += attackPrefabLength / 2 * Cos(attackRad);
        objPos.y += attackPrefabLength / 2 * Sin(attackRad);

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
        Deceleration(decelerationHitRatio);
        Destroy(attackObj);
        attackObj = null;
    }

    private static float Sin(float rad)
    {
        return Mathf.Sin(rad * Mathf.Deg2Rad);
    }

    private static float Cos(float rad)
    {
        return Mathf.Cos(rad * Mathf.Deg2Rad);
    }

    private static float Atan2(float x, float y)
    {
        return Mathf.Repeat(Mathf.Atan2(y, x) * Mathf.Rad2Deg, 360f);
    }
}
