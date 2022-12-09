using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputProvider _inputProvider = null;
    private Spiner _spiner = null;

    [SerializeField]
    private int _playerNo = 0;                  // �v���C���[�ԍ�
    private Vector3 _direction = Vector3.zero;  // ���͂��ꂽ����
    private bool _beforeShoot = true;           // ���ˑO�t���O
    private int _possibleAttackCount = 1;       // �U���\��
    private int _attackCount = 0;               // �U����
    private int _possibleAvoidanceCount = 1;    // ����\��
    private int _avoidanceCount = 0;            // �����

    public void MyInit(Spiner spiner)
    {
        _inputProvider = gameObject.AddComponent<InputProvider>();
        _spiner = spiner;
    }

    public void MyUpdate(float delta, bool OnCounting)
    {
        // InputProvider�𒇉�A���͂��Ƃ�
        // gameStatus��counting�̎��́A�����̓��͂̂�
        if (OnCounting)
        {
            GetDirection();
        }
        else
        {
            // gameStatus��fighting�ɂȂ�����A�R�}�𔭎�
            if (_beforeShoot)
            {
                Shoot();
            }
            else
            {
                // �R�}�𔭎ˌ�͍U���Ɖ���̏���
                GetDirection();
                if (_attackCount > 0 && _direction != Vector3.zero)
                {
                    Attack();
                }
                if (_avoidanceCount > 0 && _direction != Vector3.zero)
                {
                    Avoidance();
                }
            }
        }
    }

    // ���͕����擾
    private void GetDirection()
    {
        switch (_playerNo)
        {
            case 1:
                _direction = _inputProvider.GetLeftStick1P() * -1;
                break;
            case 2:
                _direction = _inputProvider.GetLeftStick2P() * -1;
                break;
        }
        // InputProvide�Ń}�E�X�ƃR���g���[���Ŏ擾�ł���x�N�g����
        // �������t�ɂȂ��Ă���
        // �@�}�E�X�̓h���b�O���������Ƌt����
        // �@�R���g���[���̓X�e�B�b�N��|��������
        // �R���g���[���g�p��z�肵�āA�t�����i-1�������Ă���j�ɕύX
        // ���Ă��邽�߁A�}�E�X����̏ꍇ�̓h���b�O���������ɓ���
    }

    // �R�}����
    private void Shoot()
    {
        _spiner.SetDirection(_direction);
        _beforeShoot = false;
        _attackCount = _possibleAttackCount;
        _avoidanceCount = _possibleAvoidanceCount;
    }

    // �U��
    private void Attack()
    {
        if ((_playerNo == 1 && _inputProvider.GetFireDown1P(InputProvider.FireType.Circle) ||
             _playerNo == 2 && _inputProvider.GetFireDown2P(InputProvider.FireType.Circle)))
        {
            _spiner.AttackStart(_direction.normalized);
            _attackCount--;
        }
    }

    // ���
    private void Avoidance()
    {
        if ((_playerNo == 1 && _inputProvider.GetFireDown1P(InputProvider.FireType.Cross) ||
             _playerNo == 2 && _inputProvider.GetFireDown2P(InputProvider.FireType.Cross)))
        {
            _spiner.SetDirection(_direction);
            _avoidanceCount--;
        }
    }
}
