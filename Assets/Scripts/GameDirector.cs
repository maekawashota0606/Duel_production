using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    [SerializeField]
    private GameObject _playersRoot = null;
    [SerializeField]
    private GameObject _spinersRoot = null;
    private Player _player_1 = null;
    private Player _player_2 = null;
    private Spiner _spiner_1 = null;
    private Spiner _spiner_2 = null;
    [SerializeField]
    private CollisionBetweenSpiner _collisionBetweenSpiner = null;
    //
    private gameState _gameState = gameState.waiting;
    private delegate void OnUpdate(float delta);
    private OnUpdate _onUpdate = null;
    /// <summary>
    /// コマが停止とみなす速度
    /// </summary>
    public const float _minSpeed = 8;

    private float startCount = 3;
    private float count = 0;

    private enum gameState
    {
        waiting,
        counting,
        fighting,
        ended,
    }

    private void Start()
    {
        Init();
        _player_1.MyInit(_spiner_1);
        _player_2.MyInit(_spiner_2);
        _spiner_1.MyInit();
        _spiner_2.MyInit();
        _collisionBetweenSpiner.MyInit();
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        _onUpdate(delta);
    }


    private void Init()
    {
        // プレイヤーのインスタンスを取得
        _player_1 = _playersRoot.transform.GetChild(0).GetComponent<Player>();
        _player_2 = _playersRoot.transform.GetChild(1).GetComponent<Player>();
        // コマのインスタンスを取得
        _spiner_1 = _spinersRoot.transform.GetChild(0).GetComponent<Spiner>();
        _spiner_2 = _spinersRoot.transform.GetChild(1).GetComponent<Spiner>();

        //
        _onUpdate = OnWaiting;
    }

    private void OnWaiting(float delta)
    {
        // 他のセットアップを待つ
        count = startCount;

        // セットアップが完了したら
        _gameState = gameState.counting;
        _onUpdate = OnCounting;
    }

    private void OnCounting(float delta)
    {
        // 3秒?数える
        count -= delta;
        if (count > 0)
        {
            _player_1.MyUpdate(delta, true);
            _player_2.MyUpdate(delta, true);
            return;
        }

        //_gameState = gameState.fighting;

        // カウントに達したら
        _gameState = gameState.fighting;
        _onUpdate = Onfighting;
    }

    private void Onfighting(float delta)
    {
        _player_1.MyUpdate(delta, false);
        _player_2.MyUpdate(delta, false);
        _collisionBetweenSpiner.MyUpdate();
        _spiner_1.MyUpdate(delta);
        _spiner_2.MyUpdate(delta);

        // 勝敗がついたら
        if(_spiner_1.hp <= 0)
        {
            Destroy(_spiner_1.gameObject);
            _gameState = gameState.ended;
            _onUpdate = OnEnded;
        }

        if(_spiner_2.hp <= 0)
        {
            Destroy(_spiner_2);
            _gameState = gameState.ended;
            _onUpdate = OnEnded;
        }
    }

    private void OnEnded(float delta)
    {
        if (InputProvider.Instance.GetFire1P(InputProvider.FireType.Cross) || InputProvider.Instance.GetFire2P(InputProvider.FireType.Cross))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }
    }
}
