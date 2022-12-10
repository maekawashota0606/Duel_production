using UnityEngine;

public class Spiner : MonoBehaviour
{
    [SerializeField]
    private float _defaultSpeed = 1;
    [SerializeField]
    private int _power = 0;
    private float _currentSpeed = 0;
    /// <summary>
    /// �R�}�̐i�s����
    /// </summary>
    private Vector3 _direction = Vector3.zero;


    /// <summary>
    /// ��������̏���������
    /// </summary>
    public void MyInit()
    {

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
    public void ReflectWall()
    {

    }

    /// <summary>
    /// ���̃R�}�ɐڐG�����Ƃ��̏���
    /// </summary>
    public void ReflectSpiner()
    {
        
    }
    
    /// <summary>
    /// �����ł̌���
    /// </summary>
    private void DecelerationRatio(float ratio)
    {

    }

    /// <summary>
    /// �萔�ł̌���
    /// </summary>
    private void Deceleration(float num)
    {

    }
    #endregion
}
