using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ResultScnenMnager;

public class ResultScnenMnager : MonoBehaviour
{
    //�X�s�[�J�[�̖���
    [SerializeField] AudioSource bgmAudioSource = default;
    [SerializeField] AudioSource seAudioSource = default;

    //��(BGM)������
    [SerializeField] AudioClip[] bgmList = default;
    public enum BGM
    {
        TitleBGM,
        GaneBGM,
        ClearBGM,
        GaneOverBGM,
    };

    //���ʉ�(se)������
    [SerializeField] AudioClip[] seList = default;
    public enum SE
    {
        TitleSE,
        GaneSE,
        ClearSE,
        GaneOverSE,
    };

    //�ǂ�����ł������ł���悤�ɂ��違�V���O���g��
    public static ResultScnenMnager instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//�V�[�����ύX���Ă��j�󂳂�Ȃ��Ȃ�
        }
        else
        {
            Destroy(gameObject);//��������(ResultScnenMnager)�����ɂ���ꍇ�͔j�󂷂�
        }
    }

    public void PlayBGM(BGM bgm)
    {
        int nunber = (int)bgm;//�񌝌^��BGM�𐮐��ɕϊ�
        bgmAudioSource.clip = bgmList[nunber];//BGM���Z�b�g����
        bgmAudioSource.Play();//�Đ�����
    }

    public void PlaySE(BGM se)
    {
        int nunber = (int)se;//�񌝌^��SE�𐮐��ɕϊ�
        AudioClip clip = seList[nunber];//���ʉ����Z�b�g����
        seAudioSource.PlayOneShot(clip);//1�x�Đ�����
    }
}
