using UnityEngine;

public class Spiner : MonoBehaviour
{
    [SerializeField]
    private float _defaultSpeed = 1;
    [SerializeField]
    private int _power = 0;
    [SerializeField]
    public int hp = 1;
    [SerializeField]
    public float _currentSpeed = 0;

    Player _player;
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
    private Attack attackComponent = null;          // �U���I�u�W�F�N�g�̃R���|�[�l���g

    /// <summary>
    /// ��������̏���������
    /// </summary>
    public void MyInit()
    {
        _currentSpeed = _defaultSpeed;
        attackComponent = transform.GetChild(0).gameObject.GetComponent<Attack>();
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
        // �@�����Q�Ƃ��Ĕ���
        //SetDirection(Vector3.Reflect(_direction, normal).normalized);
        // ���ڐG�����^�C�~���O�ł������Ď~�܂��Ă��܂��������邽�߁A
        // �Ƃ肠�������]
        SetDirection(_direction * -1);
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

    // �U��
    public void Attack(Vector3 dir)
    {
        attackComponent.ActiveStart(dir);
    }

    // �U����e��
    public void HitAttack(int damage = 3)
    {
        hp -= damage;
        Deceleration(decelerationHitRatio);
    }

    // �U�����E��
    public void DecelerationOffset()
    {
        Deceleration(decelerationHitRatio);
        attackComponent.SetActive(false);
    }
}
